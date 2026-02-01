using System.Security.Cryptography.X509Certificates;
using Build_Market_Management_System.ViewModels;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using UseCases.Interfaces;
using UseCases.ProductsUseCases;


namespace Build_Market_Management_System.Controllers
{
    [Authorize(Policy = "Inventory")]
    public class ProductsController : Controller
    {
        private readonly IAddProductUseCase addProductUseCase;
        private readonly IEditProductUseCase editProductUseCase;
        private readonly IDeleteProductUseCase deleteProductUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
        private readonly IViewProductsUseCase viewProductsUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewProductsInCategoryUseCase viewProductsInCategoryUseCase;

        public ProductsController(IAddProductUseCase addProductUseCase,
            IEditProductUseCase editProductUseCase,
            IDeleteProductUseCase deleteProductUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase,
            IViewProductsUseCase viewProductsUseCase,
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewProductsInCategoryUseCase viewProductsInCategoryUseCase)
        {
            this.addProductUseCase = addProductUseCase;
            this.editProductUseCase = editProductUseCase;
            this.deleteProductUseCase = deleteProductUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
            this.viewProductsUseCase = viewProductsUseCase;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewProductsInCategoryUseCase = viewProductsInCategoryUseCase;
        }
        public IActionResult Index()
        {
            var products = viewProductsUseCase.Execute(loadCategory: true);
            return View(products);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var productViewModel = new ViewModels.ProductViewModel
            {
                Categories = viewCategoriesUseCase.Execute()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                addProductUseCase.Execute(productViewModel.Product);
                return RedirectToAction("Index");
            }

            productViewModel.Categories = viewCategoriesUseCase.Execute();
            return View(productViewModel);


        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            deleteProductUseCase.Execute(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var productViewModel = new ProductViewModel
            {
                // Dzieki temu pobieramy dane kategorii i produktu ktore potem przekazuje do widoku i wyswietlam
                Product = viewSelectedProductUseCase.Execute(id ?? 0),
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                editProductUseCase.Execute(productViewModel.Product.ProductId, productViewModel.Product);
                return RedirectToAction("Index");
            }
            productViewModel.Categories = viewCategoriesUseCase.Execute();
            return View(productViewModel);
        }

        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            var products = viewProductsInCategoryUseCase.Execute(categoryId);

            // Dzieki partial view zamisast odswiezac cala strone odswiezamy tylko liste produktow
            return PartialView("_Products", products);
        }
    }
}
