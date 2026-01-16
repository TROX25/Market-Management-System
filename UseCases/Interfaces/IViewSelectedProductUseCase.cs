using CoreBusiness;

namespace UseCases.Interfaces
{
    public interface IViewSelectedProductUseCase
    {
        Product Execute(int productId);
    }
}