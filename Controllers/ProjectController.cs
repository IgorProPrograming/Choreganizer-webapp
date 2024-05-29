using Choreganizer_webapp.Models;
using LOGIC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Net.WebSockets;

namespace Choreganizer_webapp.Controllers
{
    public class ProjectController(IConfiguration configuration, IProjectRepository projectRepository) : Controller
    {
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Index", "LoginPage");
            } else 
            {                 
                int ownerId = int.Parse(HttpContext.Session.GetString("UserId"));
                List<LOGIC.Project> projectsList = new List<LOGIC.Project>();
            
                projectsList = new ProjectService(projectRepository).GetProjects(ownerId);
                return View(projectsList);
            }
        }

        public IActionResult OpenProject(int projectId) 
        {
            HttpContext.Session.SetString("CurrentProjectId", projectId.ToString());
            string test = HttpContext.Session.GetString("CurrentProjectId");
            return RedirectToAction("Index", "ChoreTable");
        }

        public IActionResult Remove(int projectId)
        {
            new ProjectService().RemoveProject(projectId);
            return RedirectToAction("Index");
        }

        public ActionResult Add(string projectName)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            new ProjectService().AddProject(projectName, userId);
            return RedirectToAction("Index");
        }
    }
}
