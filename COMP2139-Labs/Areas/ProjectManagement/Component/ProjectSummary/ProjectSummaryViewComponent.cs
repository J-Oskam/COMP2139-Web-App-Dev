using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Data;

namespace COMP2139_Labs.Areas.ProjectManagement.Component.ProjectSummary {
    public class ProjectSummaryViewComponent : ViewComponent {
        private readonly AppDbContext _context;

        public ProjectSummaryViewComponent(AppDbContext context) {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int projectId) {
            var project = await _context.Projects
                                        .Include(p => p.Tasks)
                                        .FirstOrDefaultAsync(p => p.ProjectID == projectId);

            if(project == null) {
                return Content("Project not found");
            }
            return View(project);
        }
    }
}
