using Microsoft.AspNetCore.Mvc;

namespace Build_Market_Management_System.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
