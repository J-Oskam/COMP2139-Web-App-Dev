using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models {
    public class Project {
        [Key] //states that this is the primary key
        public int ProjectID { get; set; }

        [Required]
        public /*required*/ string Name { get; set; }

        public string? Description { get; set; }

        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        //[DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? Status { get; set; }

    }
}