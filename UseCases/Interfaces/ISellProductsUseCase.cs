namespace UseCases.Interfaces
{
    public interface ISellProductsUseCase
    {
        void Execute(int productId, int quantity);
    }
}