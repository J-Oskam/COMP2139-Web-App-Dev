using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class Project : IValidatableObject
    {
        [Key] //states that this is the primary key
        public int ProjectID { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        [StringLength(100, ErrorMessage = "The project name cannot exceed 100 characters")]
        public required string Name { get; set; }

        [Display(Name = "Decription")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }

        public List<ProjectTask>? Tasks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                //Yield is used to iterate methods to produce sequence of values
                yield return new ValidationResult("End date must be greater than start date.", new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}