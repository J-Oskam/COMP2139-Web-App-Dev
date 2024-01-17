using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models {
    public class Project {
        public int ProjectID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
