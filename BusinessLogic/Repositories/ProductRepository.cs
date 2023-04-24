
using BusinessLogic.Repositories.Interfaces;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static ICollection<Product> db = new List<Product>() {
            new Product()
            {
                Id= 1,
                Name = "Product 1",
                Price = 1.99M,
                Quantity = 10
            } ,
            new Product()
            {
                Id= 2,
                Name = "Product 2",
                Price = 123.50M,
                Quantity = 100
            }
        };

        public X GetOneAsync(int id)
        {
            return db.X;
        }

        public X GetAllByIdsAsync(List<int> ids)
        {
            return db.X;
        }

        public X GetAllAsync()
        {
            return db.X);
        }

        public X UpdateAsync(Product product)
        {
            // dlaczego tutaj nic nie ma?
        }

        public X AddAsync(Product product)
        {
            product.Id = db.X
            db.X
        }

        public X DeleteAsync(int id)
        {
            var itemToDelete = X
            if (itemToDelete != null)
            {
                itemToDelete.X = true;
            }
        }
    }
}
