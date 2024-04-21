using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Net.WebSockets;

namespace Choreganizer_webapp.Controllers
{
    public class ProjectsController(IConfiguration configuration) : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public IActionResult Index()
        {
            //get projects from database
            List<Projects> projectsList = new List<Projects>();

            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Projects", s);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var project = new Projects();
                        project.Id = (int)rdr["Id"];
                        project.ProjectName = (string)rdr["ProjectName"];
                        //project.CreationDate = (DateTime)rdr["CreationDate"];
                        //project.LastEditDate = (DateTime)rdr["LastEditDate"];
                        projectsList.Add(project);
                    }
                }
            }
            return View(projectsList);
        }

        [HttpPost]
        public ActionResult Remove(int Id)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Chores] WHERE Id = @Id", s);
                cmd.Parameters.AddWithValue("@Id", Id.ToString());
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Add(string Chore)
        {
            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                s.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Chores] ([Chore], [DateTime], [Completed]) VALUES (@Chore, @DateTime, 0)\r\n", s);
                cmd.Parameters.AddWithValue("@Chore", Chore);
                cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
