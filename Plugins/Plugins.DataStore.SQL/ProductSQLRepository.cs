using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class ProductSQLRepository : IProductRepository
    {
        private readonly MarketContext db;

        public ProductSQLRepository(MarketContext db)
        {
            this.db = db;
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void DecreaseProductQuantity(int productId, int quantity)
        {
            if (productId <= 0 || quantity <= 0) return;

            var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) return;
            if (product.Quantity < quantity) return;
            product.Quantity -= quantity;
            db.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            if (productId <= 0) return;

            var productToDelete = db.Products.FirstOrDefault(c => c.ProductId == productId);
            if (productToDelete != null)
            {
                // Remove nie jest moim kodem tylko kodem EF
                db.Products.Remove(productToDelete);
                db.SaveChanges();
            }
        }

        public Product? GetProductById(int productId, bool loadCategory = false)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) return null;

            if (loadCategory)
                return db.Products
                    .Include(x => x.Category)
                    .FirstOrDefault(x => x.ProductId == productId);
            else
                return db.Products
                    .FirstOrDefault(x => x.ProductId == productId);
        }

        public IEnumerable<Product> GetProducts(bool loadCategory = false)
        {
            if (loadCategory)
                return db.Products.Include(x => x.Category).OrderBy(x => x.CategoryId).ToList();
            else
                return db.Products.OrderBy(x => x.CategoryId).ToList();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return db.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void UpdateProduct(int productId, Product product)
        {
            if (productId != product.ProductId) return;

            var productToUpdate = db.Products.FirstOrDefault(c => c.ProductId == productId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.CategoryId = product.CategoryId;
                db.SaveChanges();
            }
        }
    }
}
