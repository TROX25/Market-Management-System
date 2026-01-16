using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces;

namespace UseCases.ProductsUseCases
{
    public class ViewProductsInCategoryUseCase : IViewProductsInCategoryUseCase
    {
        private readonly IProductRepository productRepository;

        public ViewProductsInCategoryUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<Product> Execute(int categoryId)
        {
            return productRepository.GetProductsByCategoryId(categoryId);
        }
    }
}
