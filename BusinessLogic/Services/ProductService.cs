
using BusinessLogic.Dto.Product;
using BusinessLogic.Enums;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        //private readonly IOrderRepository orderRepository;

        public ProductService(X)
        {
            X
        }

        public async Task<IEnumerable<ProductDto>> GetListAsync()
        {
            var products = await productRepository.GetAllAsync();
            var result = new List<ProductDto>();
            foreach (var product in products)
            {
                result.Add(X);
            }
            return result;
        }

        public async Task<ProductDeleteStatus> DeleteAsync(int id)
        {
            bool canDeleteProduct = await CheckCanDeleteProductAsync(id);
            if (!canDeleteProduct)
            {
                return ProductDeleteStatus.ProductCannotBeDeleted;
            }
            await productRepository.DeleteAsync(id);
            return ProductDeleteStatus.Ok;
        }

        public async Task<int> AddAsync(ProductAddEditDto product)
        {
            var newProduct = X;
            await productRepository.AddAsync(newProduct);
            return newProduct.Id;
        }

        public async Task<bool> UpdateAsync(int id, ProductAddEditDto product)
        {
            var existingProduct = await productRepository.GetOneAsync(id);
            if (existingProduct == null)
            {
                return false;
            }
            X
            await productRepository.UpdateAsync(existingProduct);
            return true;
        }

        private async Task<bool> CheckCanDeleteProductAsync(int id)
        {
            //var existsAnyActiveOrderWithSelectedProduct = await orderRepository.ExistsAnyActiveOrderWithSelectedProductAsync(id);
            //return !existsAnyActiveOrderWithSelectedProduct;
            return true;
        }
    }
}
