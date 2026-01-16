using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void DecreaseProductQuantity(int productId, int quantity);
        void DeleteProduct(int productId);
        Product GetProductById(int productId, bool loadCategory = false);
        IEnumerable<Product> GetProducts(bool loadCategory = false);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
        void UpdateProduct(int productId, Product product);
    }
}