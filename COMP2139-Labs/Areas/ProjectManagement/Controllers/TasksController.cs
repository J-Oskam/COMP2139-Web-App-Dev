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
        public async Task<IActionResult> Create([Bind("Title", "Description", "ProjectID")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _db.ProjectTasks.AddAsync(task);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectID = task.ProjectID });
            }

            //async call to retrieve the projects for SelectList
            var projects = await _db.Projects.ToListAsync();

            //repopulate the projects SelectList if returning to the form
            ViewBag.Projects = new SelectList(projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _db.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.ProjectTaskID == id);

            if (task == null)
            {
                return NotFound();
            }

            var projects = await _db.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(projects, "ProjectID", "Name", task.ProjectID);
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

            var projects = await _db.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        [HttpGet("Delete/{projectID:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _db.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.ProjectTaskID == id);

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

        //[HttpGet("Search/{projectID:int}/{searchString?}")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(int? projectID, string searchString)
        {
            //var tasksQuery = _db.ProjectTasks.Where(t => t.ProjectID == projectID);
            var taskQuery = _db.ProjectTasks.AsQueryable();
            bool searchPerformed = !String.IsNullOrEmpty(searchString);

            //if projectID is provided then apply is at a filter
            if (projectID.HasValue) {
                taskQuery = taskQuery.Where(t => t.ProjectID == projectID.Value);
            }

            //apply search string as a filter if it's present
            if (!searchPerformed)
            {
                taskQuery = taskQuery.Where(t => t.Title.Contains(searchString)
                                            || t.Description.Contains(searchString));
            }

            //execute query
            var tasks = await taskQuery.ToListAsync();

            ViewBag.ProjectID = projectID; // keeps track of current project
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", tasks); // reuse the index view to display results
        }
    }
}