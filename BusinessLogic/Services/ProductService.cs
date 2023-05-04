
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
        private readonly IOrderRepository orderRepository;

        public ProductService(IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public IEnumerable<ProductDto> GetList()
        {
            var products = productRepository.GetAll();
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

        public ProductDeleteStatus Delete(int id)
        {
            bool canDeleteProduct = CheckCanDeleteProduct(id);
            if (!canDeleteProduct)
            {
                return ProductDeleteStatus.ProductCannotBeDeleted;
            }
            productRepository.Delete(id);
            return ProductDeleteStatus.Ok;
        }

        public int Add(ProductAddEditDto product)
        {
            var newProduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
            productRepository.Add(newProduct);
            return newProduct.Id;
        }

        public bool Update(int id, ProductAddEditDto product)
        {
            var existingProduct = productRepository.GetOne(id);
            if (existingProduct == null)
            {
                return false;
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            productRepository.Update(existingProduct);
            return true;
        }

        private bool CheckCanDeleteProduct(int id)
        {
            var existsAnyActiveOrderWithSelectedProduct = orderRepository.ExistsAnyActiveOrderWithSelectedProduct(id);
            return !existsAnyActiveOrderWithSelectedProduct;
        }
    }
}
