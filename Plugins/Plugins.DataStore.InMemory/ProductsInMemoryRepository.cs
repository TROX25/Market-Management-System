using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class ProductsInMemoryRepository : IProductRepository
    {
        
        // _products wynika z konwencji nazewnictwa dla prywatnych pól statycznych. Oznacza to ze NIE jest przeznaczone do użytku poza tą klasą !!!
        private List<Product> _products = new()
        {
            new Product { ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 100, Price = 1.99 },
            new Product { ProductId = 2, CategoryId = 1, Name = "Canada Dry", Quantity = 200, Price = 1.99 },
            new Product { ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 300, Price = 1.50 },
            new Product { ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 300, Price = 1.50 }
        };

        private readonly ICategoryRepository categoryRepository;

        public ProductsInMemoryRepository(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void AddProduct(Product product)
        {
            if (product != null && _products.Count > 0)
            {
                var maxId = _products.Max(x => x.ProductId);
                product.ProductId = maxId + 1;
            }
            else
            {
                product.ProductId = 1;
            }
            if (_products.Count == 0)
            {
                new List<Product>();
            }
            _products.Add(product);
        }

        public IEnumerable<Product> GetProducts(bool loadCategory = false)
        {
            if (!loadCategory)
            {
                return _products;
            }
            else
            {
                if (_products != null && _products.Count > 0)
                {
                    _products.ForEach(p =>
                    {
                        if (p.CategoryId.HasValue)
                            p.Category = categoryRepository.GetCategoryById(p.CategoryId ?? 0);
                    });
                }
                return _products ?? new List<Product>();
            }

        }

        public Product? GetProductById(int productId, bool loadCategory = false)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                var prod = new Product
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    CategoryId = product.CategoryId
                };
                if (loadCategory && prod.CategoryId.HasValue)
                {
                    prod.Category = categoryRepository.GetCategoryById(prod.CategoryId ?? 0);
                }
                return prod;
            }

            return null;
        }

        public void UpdateProduct(int productId, Product product)
        {
            if (productId != product.ProductId) return;

            var productToUpdate = _products.FirstOrDefault(x => x.ProductId == productId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;
                productToUpdate.CategoryId = product.CategoryId;
            }
        }

        public void DeleteProduct(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _products.Where(x => x.CategoryId == categoryId);
            if (products != null)
            {
                return products.ToList();
            }
            else
            {
                return new List<Product>();
            }
        }

        public void DecreaseProductQuantity(int productId, int quantity)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                product.Quantity -= quantity;
            }
        }
    }
}

