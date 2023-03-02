using FootballLeagueApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FootballLeagueApplication.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(ILogger<TeamsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TeamsRank()
        {
            return View();
        }

        public IActionResult PlayedMatches()
        {
            return View();
        }

        public IActionResult PlayedMatchesDetail(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public IActionResult Teams()
        {
            return View();
        }
        public IActionResult TeamsDetail(int id)
        {
            ViewBag.Id = id;
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}