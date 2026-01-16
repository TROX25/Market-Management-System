using CoreBusiness;

namespace UseCases.Interfaces
{
    public interface IEditProductUseCase
    {
        void Execute(int productId, Product product);
    }
}