using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP2139_Labs.Controllers {
    public class TasksController : Controller {
        private readonly AppDbContext _db;

        public TasksController(AppDbContext context) {
            _db = context;
        }
        public IActionResult Index(int projectId) {
            var tasks = _db.ProjectTasks.Where(t => t.ProjectId == projectId).ToList();
            ViewBag.ProjectId = projectId; // Store projectId in viewbag
            return View(tasks);
        }

        public IActionResult Details(int id) {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(task => task.ProjectId == id);

            if(task == null) {
                return NotFound();
            }
            return View(task);
        }

        public IActionResult Create(int projectId) {
            var project = _db.Projects.Find(projectId);
            if(project == null) {
                return NotFound();
            }

            var task = new ProjectTask() {
                ProjectId = projectId
            };
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title", "Description", "ProjectID")] ProjectTask task) {
            if (ModelState.IsValid) {
                _db.ProjectTasks.Add(task);
                _db.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        public IActionResult Edit(int id) {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskId == id);
            
            if(task == null) {
                return NotFound();
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task) {
            if(id != task.ProjectTaskId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                _db.ProjectTasks.Update(task);
                _db.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        public IActionResult Delete(int id) {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskId == id);

            if (task == null) {
                return NotFound();
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int projectTaskId) {

            var task = _db.ProjectTasks.Find(projectTaskId);
            if (task != null) {
                _db.ProjectTasks.Remove(task);
                _db.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }
            return NotFound();
        }
    }
}
