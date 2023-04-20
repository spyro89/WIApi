
using Database;
using Dto.Product;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public ProductService(IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetListAsync()
        {
            var products = await productRepository.GetAllAsync();
            var result = new List<ProductDto>();
            foreach (var product in products)
            {
                result.Add(new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                });
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool canDeleteProduct = await CheckCanDeleteProductAsync(id);
            if (!canDeleteProduct)
            {
                return canDeleteProduct;
            }
            await productRepository.DeleteAsync(id);
            return true;
        }

        public async Task<int> AddAsync(ProductAddEditDto product)
        {
            var newProduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
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
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            await productRepository.UpdateAsync(existingProduct);
            return true;
        }

        private async Task<bool> CheckCanDeleteProductAsync(int id)
        {
            var existsAnyActiveOrderWithSelectedProduct = await orderRepository.ExistsAnyActiveOrderWithSelectedProductAsync(id);
            return existsAnyActiveOrderWithSelectedProduct;
        }
    }
}
