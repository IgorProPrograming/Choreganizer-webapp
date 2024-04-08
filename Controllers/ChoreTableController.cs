using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Choreganizer_webapp.Controllers
{
    public class ChoreTableController : Controller
    {
        public IActionResult Index(string Chore, int Amount)
        {
            ViewData["Chore"] = Chore;
            ViewData["Amount"] = Amount;
            return View();
        }
    }
}
