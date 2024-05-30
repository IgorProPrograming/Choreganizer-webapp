using Microsoft.AspNetCore.Mvc;
using LOGIC;

namespace Choreganizer_webapp.Controllers
{
    public class ChoreTableController : Controller
    {
        private readonly string _connectionString;
        private readonly IChoreRepository _choreRepository;
        private readonly ChoreService _choreService;

        public ChoreTableController(IConfiguration configuration, IChoreRepository choreRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _choreRepository = choreRepository;
            _choreService = new ChoreService(_choreRepository, _connectionString);
        }

        public IActionResult Index()
        {
            int projectId = int.Parse(HttpContext.Session.GetString("CurrentProjectId"));
            List<Chore> choreList = _choreRepository.GetChores(projectId, _connectionString);

            return View(choreList);
        }

        public ActionResult Remove(int choreId)
        {
            _choreService.RemoveChore(choreId, _connectionString);
            return RedirectToAction("Index", "ChoreTable");
        }

        public ActionResult Add(string choreName)
        {
            int projectId = int.Parse(HttpContext.Session.GetString("CurrentProjectId") ?? "0");
            _choreService.AddChore(choreName, projectId, _connectionString);
            return RedirectToAction("Index");
        }

        public ActionResult ToggleState(int choreId, bool lastState)
        {
            _choreService.ToggleChoreStatus(choreId, lastState, _connectionString);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int choreId)
        {
            Chore chore = _choreService.GetChore(choreId, _connectionString);
            return View(chore);
        }


        public ActionResult BackToList()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int choreId, string choreName, string choreDescription, DateTime deadlineDate)
        {
            Chore chore = new Chore()
            {
                Id = choreId,
                ChoreName = choreName,
                Deadline = deadlineDate
            };

            _choreService.UpdateChore(chore, _connectionString);
            return RedirectToAction("Index");
        }

    }
}
