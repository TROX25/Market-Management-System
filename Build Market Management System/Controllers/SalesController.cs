using Build_Market_Management_System.Models;
using Build_Market_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Build_Market_Management_System.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = CategoriesRepository.GetCategories()
            };
            return View(salesViewModel);
        }
    }
}
