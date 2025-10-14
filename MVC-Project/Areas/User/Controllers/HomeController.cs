using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var Categories = context.Categories.ToList();
            ViewBag.Categories = context.Categories.ToList();
            return View();
        }
    }
}
