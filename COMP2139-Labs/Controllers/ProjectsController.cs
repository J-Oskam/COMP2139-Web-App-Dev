using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;
using COMP2139_Labs.Data;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Controllers {
    public class ProjectsController : Controller {

        private readonly AppDbContext _db;
        public ProjectsController(AppDbContext db) {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index() {
            return View(_db.Projects.ToList());
        }

        [HttpGet]
        public IActionResult Details(int id) {
            var project = _db.Projects.FirstOrDefault(p => p.ProjectID == id);
            if (project == null) {
                return NotFound();
            }
            return View(project);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var project = _db.Projects.Find(id);
            if (project == null) {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")] Project project) {
            if(id != project.ProjectID) {
                return NotFound();
            }
            if(ModelState.IsValid) {
                try {
                    _db.Update(project);
                } catch (DbUpdateConcurrencyException) {
                    if (!ProjectExists(project.ProjectID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var project = _db.Projects.FirstOrDefault(p => p.ProjectID == id);
            if (project == null) {
                return NotFound();
            }
            return View(project);
        }

        [HttpGet]
        private bool ProjectExists(int id) {
            return _db.Projects.Any(e => e.ProjectID == id);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project) {
            if (ModelState.IsValid) {
                _db.Projects.Add(project);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }


        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int ProjectId) {
            var project = _db.Projects.Find(ProjectId);
            if (project != null) {
                _db.Projects.Remove(project);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //handle the case where the project might not be found
            return NotFound();
        }
    }
}