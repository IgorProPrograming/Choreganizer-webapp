using Choreganizer_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Net.WebSockets;
using LOGIC;

namespace Choreganizer_webapp.Controllers
{
    public class ChoreTableController(IConfiguration configuration) : Controller
    {

        public IActionResult Index()
        { 
            int projectId = int.Parse(HttpContext.Session.GetString("CurrentProjectId"));

            List<Chore> choreList = new ChoreService().GetChores(projectId);
            
            return View(choreList);
        }

        public ActionResult Remove(int choreId)
        {
            new ChoreService().RemoveChore(choreId);
            return RedirectToAction("Index", "ChoreTable");
        }

        public ActionResult Add(string choreName)
        {
            int projectId = int.Parse(HttpContext.Session.GetString("CurrentProjectId"));
            new ChoreService().AddChore(choreName, projectId);
            return RedirectToAction("Index");
        }
    }
}
