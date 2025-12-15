using Microsoft.AspNetCore.Mvc;
using Build_Market_Management_System.Models;

namespace Build_Market_Management_System.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            var categories = CategoriesRepository.GetCategories();
            return View(categories);
        }

        public IActionResult Edit(int id)
        {
            var category = new Category
            {
                ID = id,
                Name = "Sample Category",
                Description = "This is a sample category description."
            };
            return View(category);
        }
    }
}
