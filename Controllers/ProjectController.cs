using LOGIC;
using Microsoft.AspNetCore.Mvc;

namespace Choreganizer_webapp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly string _connectionString;
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectService _projectService;

        public ProjectController(IConfiguration configuration, IProjectRepository projectRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _projectRepository = projectRepository;
            _projectService = new ProjectService(_projectRepository, _connectionString);
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            int ownerId = int.Parse(HttpContext.Session.GetString("UserId"));
            List<Project> projectsList = _projectService.GetProjects(ownerId, _connectionString);
            return View(projectsList);
        }

        public IActionResult OpenProject(int projectId)
        {
            HttpContext.Session.SetString("CurrentProjectId", projectId.ToString());
            return RedirectToAction("Index", "ChoreTable");
        }

        public IActionResult Remove(int projectId)
        {
            _projectService.RemoveProject(projectId, _connectionString);
            return RedirectToAction("Index");
        }

        public IActionResult Add(string projectName)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            _projectService.AddProject(projectName, userId, _connectionString);
            return RedirectToAction("Index");
        }
    }
}
