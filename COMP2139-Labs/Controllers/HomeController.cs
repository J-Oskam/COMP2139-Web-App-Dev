using COMP2139_Labs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace COMP2139_Labs.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult About() {
            return View();
        }

        [HttpGet]
        public IActionResult GeneralSearch(string searchType, string searchString) {
            if(searchType == "Projects") {
                return RedirectToAction("search", "Projects", new { area = "ProjectManagement", searchString }); //Redirect to projects search
            } else if(searchType=="Tasks"){
                int defaultProjectID = 1;
                return RedirectToAction("Search", "Tasks", new { projectID = defaultProjectID, searchString }); //Redirect to tasks search assuming default projectID
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotFound(int statusCode) {
            if(statusCode == 404) {
                return View("NotFound");
            }
            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}