using Choreganizer_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Net.WebSockets;
using DAL;

namespace Choreganizer_webapp.Controllers
{
    public class ChoreTableController(IConfiguration configuration) : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public int ProjectId;

        public IActionResult Index(int projectId)
        { 
            ProjectId = projectId;
            List<Chore> choreList = new List<Chore>();
            List<ChoreDTO> choresFromDB = new ChoreRepository().GetChores(projectId);

            foreach (var chore in choresFromDB)
            {
                Chore c = new Chore();
                c.ChoreName = chore.ChoreName;
                c.Id = chore.Id;
                choreList.Add(c);
            }
            
            return View(choreList);
        }

        public ActionResult Remove(int Id)
        {
            new ChoreRepository().RemoveChore(Id);
            return RedirectToAction("Index", "ChoreTable", new { projectId = ProjectId });
        }

        public ActionResult Add(string Chore)
        {
            new ChoreRepository().AddChore(Chore, ProjectId);
            return RedirectToAction("Index", new { projectId = ProjectId });
        }
    }
}
