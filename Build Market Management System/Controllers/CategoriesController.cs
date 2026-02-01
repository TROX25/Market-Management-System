using Microsoft.AspNetCore.Mvc;
using UseCases.Interfaces;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;

namespace Build_Market_Management_System.Controllers
{
    [Authorize(Policy = "Inventory")]
    public class CategoriesController : Controller
    {
        private readonly IAddCategoryUseCase addCategoryUseCase;
        private readonly IDeleteCategoryUseCase deleteCategoryUseCase;
        private readonly IEditCategoryUseCase editCategoryUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCase;

        public CategoriesController(IViewCategoriesUseCase viewCategoriesUseCase,
            IAddCategoryUseCase addCategoryUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase,
            IEditCategoryUseCase editCategoryUseCase,
            IViewSelectedCategoryUseCase viewSelectedCategoryUseCase)
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.addCategoryUseCase = addCategoryUseCase;
            this.deleteCategoryUseCase = deleteCategoryUseCase;
            this.editCategoryUseCase = editCategoryUseCase;
            this.viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
        }
        public IActionResult Index()
        {
            var categories = viewCategoriesUseCase.Execute();
            return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var category = viewSelectedCategoryUseCase.Execute(id ?? 0);

            return View(category);
        }

        [HttpPost] //defaultowo jest HttpGet wiec musimy zaznaczyc ze to jest HttpPost
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                editCategoryUseCase.Execute(category.ID, category);
                return RedirectToAction("Index"); // Dzieki temu wracamy do listy kategorii po edycji
            }
            //ViewBag.Action = "edit";
            return View(category); // Jesli model jest nieprawidlowy, ponownie wyswietlamy formularz z bledami
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            //ViewBag.Action = "add";
            if (ModelState.IsValid)
            {
                addCategoryUseCase.Execute(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }

        }

        // Delete nie musi byc post ale tak jest bezpieczniej, bo unika sie przypadkowego usuniecia przez link
        [HttpPost]
        public IActionResult Delete(int id)
        {
            deleteCategoryUseCase.execute(id);
            return RedirectToAction("Index");
        }
    }
}
