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

        public IActionResult Edit(int? id)
        {
            var category = CategoriesRepository.GetCategoryById(id ?? 0);

            return View(category);
        }

        [HttpPost] //defaultowo jest HttpGet wiec musimy zaznaczyc ze to jest HttpPost
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoriesRepository.UpdateCategory(category.ID, category);
                return RedirectToAction("Index"); // Dzieki temu wracamy do listy kategorii po edycji
            }

            return View(category); // Jesli model jest nieprawidlowy, ponownie wyswietlamy formularz z bledami
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid) 
            {
                CategoriesRepository.AddCategory(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }

        }
    }
}
