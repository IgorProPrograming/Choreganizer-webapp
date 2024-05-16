using Choreganizer_webapp.Models;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Net.WebSockets;

namespace Choreganizer_webapp.Controllers
{
    public class ProjectController(IConfiguration configuration) : Controller
    {
        //private readonly ILogger<HomeController> _logger;
       // private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public IActionResult Index(int Id)
        {
            Id = 1;
            List<Project> projectsList = new List<Project>();

            List<ProjectDTO> projectjesUitDeDatabase = new ProjectRepository().GetProjects(Id);

            foreach (var project in projectjesUitDeDatabase)
            {
                Project p = new Project();
               // p.CreationDate = project.CreationDate;
                p.ProjectName = project.ProjectName;
                p.Id = project.Id;
                projectsList.Add(p);
            }

            return View(projectsList);
        }

        public IActionResult OpenProject(int ProjectId) 
        {
            return RedirectToAction("Index", "ChoreTable", new { projectId = ProjectId });
        }

        [HttpPost]
        public ActionResult Remove(int projectId)
        {
            new ProjectRepository().RemoveProject(projectId);
            return RedirectToAction("Index");
        }

        public ActionResult Add(string projectName)
        {
            new ProjectRepository().AddProject(projectName);
            return RedirectToAction("Index");
        }
    }
}
