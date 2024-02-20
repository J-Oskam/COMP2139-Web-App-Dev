using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP2139_Labs.Controllers {
    public class TasksController : Controller {
        private readonly AppDbContext _db;

        public TasksController(AppDbContext db) {
            _db = db;
        }
        public IActionResult Index(int projectId) {
            var tasks = _db.ProjectTasks.Where(t => t.ProjectID == projectId).ToList();
            ViewBag.ProjectId = projectId; // Store projectId in viewbag
            return View(tasks);
        }

        public IActionResult Details(int id) {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(task => task.ProjectID == id);

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
                ProjectID = projectId
            };
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title", "Description", "ProjectID")] ProjectTask task) {
            if (ModelState.IsValid) {
                _db.ProjectTasks.Add(task);
                _db.SaveChanges();
                return RedirectToAction("Index", new { projectID = task.ProjectID });
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        public IActionResult Edit(int id) {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskID == id);
            
            if(task == null) {
                return NotFound();
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectTaskID", "Title", "Description", "ProjectID")] ProjectTask task) {
            if(id != task.ProjectTaskID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                _db.ProjectTasks.Update(task);
                _db.SaveChanges();
                return RedirectToAction("Index", new { ProjectID = task.ProjectID });
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        public IActionResult Delete(int id) {
            var task = _db.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskID == id);

            if (task == null) {
                return NotFound();
            }

            ViewBag.Projects = new SelectList(_db.Projects, "ProjectID", "Name", task.ProjectID);
            return View(task);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int ProjectTaskID) {

            var task = _db.ProjectTasks.Find(ProjectTaskID);
            if (task != null) {
                _db.ProjectTasks.Remove(task);
                _db.SaveChanges();
                return RedirectToAction("Index", new { ProjectID = task.ProjectID });
            }
            return NotFound();
        }
    }
}
