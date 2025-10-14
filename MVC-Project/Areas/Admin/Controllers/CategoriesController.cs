using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var category = context.Categories.ToList();
            return View(category);
        }

        public IActionResult Delete(int id) {
            var cat = context.Categories.Find(id);
            if (cat != null) 
                context.Categories.Remove(cat);
            context.SaveChanges();
            return  RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            return View(new Category());
        }

        public IActionResult Store (Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Add",category);
        }
       public IActionResult Edit(int id)
            {
                var category = context.Categories.Find(id);
                return View(category);
            }
        public IActionResult Update(Category request)
        {
            if (ModelState.IsValid)
            {
                var category = context.Categories.Find(request.Id);
                category.Name = request.Name;
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
                
            }
            return View("Edit", request);
        }

     
    }
}
