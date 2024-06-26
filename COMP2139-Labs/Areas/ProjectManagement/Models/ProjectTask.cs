using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class ProjectTask
    {
        public int ProjectTaskID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public int ProjectID { get; set; } //this is the foreign key for the project model
        public Project? Project { get; set; } //navigation property to delineate relation. Gives access to the other side of the relationship, the attributes for project
    }
}
