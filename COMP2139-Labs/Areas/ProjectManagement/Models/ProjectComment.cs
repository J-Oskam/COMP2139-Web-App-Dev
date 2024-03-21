using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace COMP2139_Labs.Areas.ProjectManagement.Models {
    public class ProjectComment {
        [Key]
        public int ProjectCommentID { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string? Content { get; set; }

        [Display(Name = "Posted Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        //foreign key for project
        public int ProjectID { get; set; }

        //navigation property for project
        public Project? project { get; set; }
    }
}