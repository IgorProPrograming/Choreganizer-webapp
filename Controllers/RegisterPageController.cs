using Microsoft.AspNetCore.Mvc;
using LOGIC;
using MySqlX.XDevAPI.Common;

namespace Choreganizer_webapp.Controllers
{
    public class RegisterPageController : Controller
    {
        public IActionResult Index()
        {
                return View();
        }

        [HttpPost]

        public ActionResult Register(string username, string password, string passwordConfirmation)
        {
            string result = new RegisterService().Register(username, password, passwordConfirmation);
            if (result == "Registered succesfully")
            {
                return RedirectToAction("Index", "LoginPage");
            } else
            {
                ViewBag.Message = result;
                return RedirectToAction("index");
            }
        }


    }
}
