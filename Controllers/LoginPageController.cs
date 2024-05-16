using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace Choreganizer_webapp.Controllers
{
    public class LoginPageController(IConfiguration configuration) : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[UserProfile] WHERE UserName = @Name AND Password = @Password", s);
                cmd.Parameters.AddWithValue("@Name", Username);
                cmd.Parameters.AddWithValue("@Password", Password);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return RedirectToAction("Index", "Project", new { Id = (int)rdr["Id"] });
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}

