using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace COMP2139_Labs.Data
{
    public class AppDbContext : IdentityDbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }
    }
}