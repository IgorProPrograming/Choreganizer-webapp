using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using LOGIC;
using MySqlX.XDevAPI;



namespace Choreganizer_webapp.Controllers
{
    public class LoginPageController(IConfiguration configuration) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RedirectToRegisterPage()
        {
            return RedirectToAction("Index", "RegisterPage");
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            UserProfile profile = new UserProfile();
            profile.Username = Username;
            profile.Password = Password;

            string result = new LoginService().login(profile);
            if (result == "User not found")
            {
                ViewBag.Message = "User not found";
                return View("Index");
            }
            else if (result == "Incorrect password")
            {
                ViewBag.Message = "Incorrect password";
                return View("Index");
            }
            else
            {

                HttpContext.Session.SetString("UserId", result);
                ViewBag.Message = "Login successful";
                return RedirectToAction("Index", "Project");
            }
        }
    }
}

