using Choreganizer_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace Choreganizer_webapp.Controllers
{
	public class HomeController(IConfiguration configuration, ILogger<HomeController> logger) : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

		public IActionResult Index()
		{

			SqlConnection s = new SqlConnection(_connectionString);
			s.Open();

            return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
