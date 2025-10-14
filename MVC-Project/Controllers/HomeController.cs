using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Areas.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var Categories = context.Categories.ToList();
            ViewBag.Categories = context.Categories.ToList();
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
