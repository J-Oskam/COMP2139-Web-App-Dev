using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using COMP2139_Labs.Areas.ProjectManagement.Models;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class TasksController : Controller
    {
        private readonly AppDbContext _db;

        public TasksController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("Index/{projectID:int}")]
        public async Task<IActionResult> Index(int? projectId)
        {
            var tasksQuery = _db.ProjectTasks.AsQueryable();

            if (projectId.HasValue) {
                tasksQuery = tasksQuery.Where(t => t.ProjectID == projectId.Value);
            }

            var tasks = await tasksQuery.ToListAsync();
            ViewBag.ProjectId = projectId; // Store projectId in viewbag
            return View(tasks);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var tasksQuery = _db.ProjectTasks.AsQueryable();
            var task = await _db.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.ProjectTaskID == id);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpGet("Create/{projectID:int}")]
        public async Task<IActionResult> Create(int projectId)
        {
            var project = await _db.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var task = new ProjectTask()
            {
                ProjectID = projectId
            };
            return View(task);
        }

        [HttpPost("Create/{projectID:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title", "Description", "ProjectID")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _db.ProjectTasks.Add(task);
                _db.SaveChanges();
                return RedirectToAction("Index", new { projectID = task.ProjectID });
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskID == id);

            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskID", "Title", "Description", "ProjectID")] ProjectTask task)
        {
            if (id != task.ProjectTaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(task);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { projectID = task.ProjectID });
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        [HttpGet("Delete/{projectID:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskID == id);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProjectTaskID)
        {

            var task = await _db.ProjectTasks.FindAsync(ProjectTaskID);
            if (task != null)
            {
                _db.ProjectTasks.Remove(task);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { projectID = task.ProjectID });
            }
            return NotFound();
        }

        [HttpGet("Search/{projectID:int}/{searchString?}")]
        public async Task<IActionResult> Search(int projectID, string searchString)
        {
            //var tasksQuery = _db.ProjectTasks.Where(t => t.ProjectID == projectID);
            var tasksQuery = _db.ProjectTasks.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                tasksQuery = tasksQuery.Where(t => t.Title.Contains(searchString)
                                            || t.Description.Contains(searchString));
            }

            var tasks = await tasksQuery.ToListAsync();
            ViewBag.ProjectID = projectID; // keeps track of current project
            return View("Index", tasks); // reuse the index view to display results
        }
    }
}
