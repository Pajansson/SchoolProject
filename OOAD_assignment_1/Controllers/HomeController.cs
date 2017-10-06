using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOAD_assignment_1.Models;
using OOAD_assignment_1.Services;

namespace OOAD_assignment_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITimeProvider _timeProvider;

        public HomeController(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Timewarp()
        {
            _timeProvider.TimeWarp();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Reset()
        {
           _timeProvider.ResetTime();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
