using Build_Market_Management_System.ViewModels;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using UseCases.Interfaces;

namespace Build_Market_Management_System.Controllers
{
    [Authorize(Policy = "Cashiers")]
    public class SalesController : Controller
    {
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
        private readonly IRecordTransactionUseCase recordTransactionUseCase;
        private readonly IDecreaseProductQuantityUseCase decreaseProductQuantityUseCase;

        public SalesController(IViewCategoriesUseCase viewCategoriesUseCase, 
            IViewSelectedProductUseCase viewSelectedProductUseCase,
            IRecordTransactionUseCase recordTransactionUseCase,
            IDecreaseProductQuantityUseCase decreaseProductQuantityUseCase)
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
            this.recordTransactionUseCase = recordTransactionUseCase;
            this.decreaseProductQuantityUseCase = decreaseProductQuantityUseCase;
        }
        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = viewCategoriesUseCase.Execute(),
            };
            return View(salesViewModel);
        }

        public IActionResult ProductDetailsPartial(int productId)
        {
            var product = viewSelectedProductUseCase.Execute(productId);
            return PartialView("_SellProduct", product);
        }

        [HttpPost]
        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                // sell product
                var prod = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
                if (prod != null && prod.Quantity.HasValue && prod.Quantity.Value >= salesViewModel.QuantityToSell)
                {
                    // Decrease prod quantity
                    decreaseProductQuantityUseCase.Execute(prod.ProductId, salesViewModel.QuantityToSell);
                    // Log transaction
                    var transaction = new Transaction
                    {
                        //TimeStamp = DateTime.Now, -> by default in AddTransaction method
                        ProductId = salesViewModel.SelectedProductId,
                        ProductName = prod.Name,
                        Price = prod.Price ?? 0,
                        BeforeQuantity = prod.Quantity.Value,
                        SoldQuantity = salesViewModel.QuantityToSell,
                        CashierName = "Default Cashier" // This could be dynamic based on logged-in user
                    };
                    recordTransactionUseCase.Execute(transaction);
                    TempData["SuccessMessage"] = $"Successfully sold {salesViewModel.QuantityToSell} unit(s) of {prod.Name}.";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Insufficient product quantity available.");
                }
            }

            var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
            salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
            // Potrzebuje jeszcze raz pobrac kategorie, zeby wypelnic dropdown w widoku
            // poniewaz przy postowaniu modelu nie sa one przesylane
            salesViewModel.Categories = viewCategoriesUseCase.Execute();
            return View("Index", salesViewModel);
        }
    }
}
