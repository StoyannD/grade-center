namespace GradeCenter.Server.Web.ViewModels.Subject
{
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;

    public class SubjectViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}