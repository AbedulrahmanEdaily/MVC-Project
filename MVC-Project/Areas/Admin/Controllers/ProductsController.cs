using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View(context.Products.Include(p => p.Category).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }

        public IActionResult Store(Product product , IFormFile file) {
            if(file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\imges",fileName);
                using(var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.Image = fileName;
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("create", product);
        }

        public IActionResult Remove(int id)
        {
            var product = context.Products.Find(id);
            context.Products.Remove(product);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\imges", product.Image);
            System.IO.File.Delete(filePath);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            ViewBag.Categories = context.Categories.ToList();
            return View(product);
        }
        public IActionResult Update(Product request,IFormFile file)
        {
            var product = context.Products.Find(request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity ;
            product.CategoryId = request.CategoryId;
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
