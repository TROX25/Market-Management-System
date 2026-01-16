using CoreBusiness;

namespace UseCases.Interfaces
{
    public interface IViewProductsUseCase
    {
        IEnumerable<Product> Execute(bool loadCategory = false);
    }
}