using System.Security.Cryptography.X509Certificates;
using Build_Market_Management_System.Models;
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
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.AddProduct(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }

        }
        public IActionResult Delete(int id)
        {
            ProductsRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
