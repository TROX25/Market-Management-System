using CoreBusiness;

namespace UseCases.Interfaces
{
    public interface IViewProductsInCategoryUseCase
    {
        IEnumerable<Product> Execute(int categoryId);
    }
}