using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models {
    public class Project : IValidatableObject {
        [Key] //states that this is the primary key
        public int ProjectID { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }

        public List<ProjectTask>? Tasks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if(EndDate < StartDate) {
                //Yield is used to iterate methods to produce sequence of values
                yield return new ValidationResult("End date must be greater than start date.", new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}