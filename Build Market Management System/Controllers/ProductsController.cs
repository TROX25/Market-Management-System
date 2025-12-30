using System.Security.Cryptography.X509Certificates;
using Build_Market_Management_System.Models;
using Build_Market_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Build_Market_Management_System.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductsRepository.GetProducts(loadCategory: true);
            return View(products);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var productViewModel = new ViewModels.ProductViewModel
            {
                Categories = CategoriesRepository.GetCategories()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.AddProduct(productViewModel.Product);
                return RedirectToAction("Index");
            }

            productViewModel.Categories = CategoriesRepository.GetCategories();
            return View(productViewModel);


        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ProductsRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var productViewModel = new ProductViewModel
            {
                // Dzieki temu pobieramy dane kategorii i produktu ktore potem przekazuje do widoku i wyswietlam
                Product = ProductsRepository.GetProductById(id ?? 0),
                Categories = CategoriesRepository.GetCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
                return RedirectToAction("Index");
            }
            productViewModel.Categories = CategoriesRepository.GetCategories();
            return View(productViewModel);
        }

        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            var products = ProductsRepository.GetProductsByCategoryId(categoryId);

            // Dzieki partial view zamisast odswiezac cala strone odswiezamy tylko liste produktow
            return PartialView("_Products", products);
        }
    }
}
