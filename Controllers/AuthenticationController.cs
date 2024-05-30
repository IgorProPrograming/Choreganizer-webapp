using Microsoft.AspNetCore.Mvc;
using LOGIC;

namespace Choreganizer_webapp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly string _connectionString;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(IConfiguration configuration, IAuthenticationRepository authenticationRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _authenticationRepository = authenticationRepository;
            _authenticationService = new AuthenticationService(_authenticationRepository);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            UserProfile profile = new UserProfile();
            profile.Username = Username;
            profile.Password = Password;

            string result = _authenticationService.Login(profile);
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

        public ActionResult Register(string username, string password, string passwordConfirmation)
        {
            string result = _authenticationService.Register(username, password, passwordConfirmation);
            if (result == "Registered successfully")
            {
                HttpContext.Session.SetString("UserId", result);
                ViewBag.Message = "Login successful";
                return RedirectToAction("Index", "Project");
            }
            else
            {
                ViewBag.Message = result;
                return RedirectToAction("Index");
            }
        }
    }
}

