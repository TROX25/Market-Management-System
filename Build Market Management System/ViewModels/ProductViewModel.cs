using CoreBusiness;

namespace Build_Market_Management_System.ViewModels
{
    // ViewModel to klasa pośrednia między Model a View, której jedynym celem jest dostarczenie danych do widoku.
    public class ProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        // W teorii zamiast IEnumerable można użyć List<Category>, ale IEnumerable jest bardziej ogólne i uniemożliwia zrobienie na przyklad .Add() czy .Remove()
        public Product Product { get; set; } = new Product();
    }
}
