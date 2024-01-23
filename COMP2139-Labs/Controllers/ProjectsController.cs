using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;

namespace COMP2139_Labs.Controllers {
    public class ProjectsController : Controller {

        [HttpGet]
        public IActionResult Index() {
            var projects = new List<Project>() { 
                new Project { ProjectID = 1, Name = "Project 1", Description = "First project" } 
            };
            return View(projects);
        }

        [HttpGet]
        public IActionResult Details(int id) {
            var project = new Project { ProjectID = id, Name = "Project " + id, Description = "Details of the project " + id };
            return View(project);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project) {
            return RedirectToAction("Index");
        }
    }
}