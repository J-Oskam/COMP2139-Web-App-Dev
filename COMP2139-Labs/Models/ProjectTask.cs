namespace COMP2139_Labs.Models {
    public class ProjectTask {
        public int ProjectTaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; } //this is the foreign key for the project model
        public Project? Project { get; set; } //navigation property to delineate relation. Gives access to the other side of the relationshop, the attributes for project
    }
}
