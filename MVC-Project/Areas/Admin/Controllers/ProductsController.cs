using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models;
using MVC_Project.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
            
        {
            var product = context.Products.Include(p => p.Category).ToList();
            var productVm = new List<ProductViewModel>();
            foreach (var item in product)
            {
                var vm = new ProductViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/imges/{item.Image}",
                    CategoryName = item.Category.Name
                };
                productVm.Add(vm);
            }
            return View(productVm);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }
        [ValidateAntiForgeryToken]
        public IActionResult Store(Product product , IFormFile Image) {
            ModelState.Remove("Image");
            ModelState.Remove("Rate");
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    fileName += Path.GetExtension(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\imges", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        Image.CopyTo(stream);
                    }
                    product.Image = fileName;

                    context.Products.Add(product);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Image", "The File Is Required");
            }
            ViewBag.Categories = context.Categories.ToList();
            return View("create", product);
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
        public IActionResult Update(Product request,IFormFile Image)
        {
            ModelState.Remove("Image");
            ModelState.Remove("Rate");
            var product = context.Products.Find(request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity ;
            product.CategoryId = request.CategoryId;
            if (Image != null && Image.Length > 0)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\imges", product.Image);
                System.IO.File.Delete(oldFilePath);
                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\imges", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);
                }
                product.Image = fileName;

            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
