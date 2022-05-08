namespace GradeCenter.Server.Web.ViewModels.Subject
{
    using System.ComponentModel.DataAnnotations;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Subject;

    public class UpdateSubjectInputModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}