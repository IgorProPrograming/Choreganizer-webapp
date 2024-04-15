using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Net.WebSockets;

namespace Choreganizer_webapp.Controllers
{
    public class ChoreTableController(IConfiguration configuration) : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public IActionResult Index()
        {
            List<Chores> choresList = new List<Chores>();

            using (SqlConnection s = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Chores", s);
                cmd.CommandType = System.Data.CommandType.Text;
                s.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var chore = new Chores();
                        chore.Id = (int)rdr["Id"];
                        chore.Chore = (string)rdr["Chore"];
                       // chore.Date = (DateTime)rdr["DateTime"];
                        chore.Finished = (byte)rdr["Completed"];
                        choresList.Add(chore);
                    }
                }
            }
            return View(choresList);
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
