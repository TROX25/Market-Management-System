using System.ComponentModel.DataAnnotations;
using Build_Market_Management_System.Models;

namespace Build_Market_Management_System.ViewModels
{
    public class SalesViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        public int SelectedProductId { get; set; }

        [Display(Name = "Quantity")]
        public int QuantityToSell { get; set; }
    }
}
