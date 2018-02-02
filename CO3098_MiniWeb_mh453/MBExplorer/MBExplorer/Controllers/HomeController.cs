using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MBExplorer.Models;
using MBExplorer.Persistence;
using MBExplorer.Core;

namespace MBExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _db;
        public HomeController(IUnitOfWork db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TreeMapChart()
        {
            return View();
        }
        public IActionResult OrganisationChart()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
