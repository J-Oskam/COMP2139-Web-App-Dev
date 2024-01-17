using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;

namespace COMP2139_Labs.Controllers {
    public class ProjectsController : Controller {
        public IActionResult Index() {
            var projects = new List<Project>() { 
                new Project { ProjectID = 1, Name = "Project 1", Description = "First project" } 
            };
            return View(projects);
        }
        public IActionResult Details() {
            return View();
        }
        public IActionResult Create() {
            return View();
        }
    }
}