namespace GradeCenter.Server.Web.Controllers
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Subject;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class SubjectsController : ApiController
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSubjectInputModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }


            var id = await this.subjectService.CreateAsync(model.Name);

            return this.Created(nameof(this.Create), id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SubjectViewModel>> Details(int id)
        {
            var result = await this.subjectService.GetByIdAsync<SubjectViewModel>(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSubjectInputModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.subjectService.UpdateAsync(id, model.Name);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            var isDeleted = await this.subjectService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            return this.Ok(await this.subjectService.GetAllAsync<SubjectViewModel>());
        }
    }
}