using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models {
    public class ProjectComment {
        public int ProjectCommentID { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot exceet 500 characters")]
        public string? Content { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        //foreign key for project
        public int ProjectID { get; set; }

        //navigation property for project
        public Project? project { get; set; }
    }
}
