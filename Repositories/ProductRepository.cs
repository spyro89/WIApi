using Database;
using Repositories.Interfaces;

namespace Repositories
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

        public async Task<Product> GetOneAsync(int id)
        {
            return db.Where(x => x.Id == id && x.IsDeleted == false).SingleOrDefault();
        }

        public async Task<IEnumerable<Product>> GetAllByIdsAsync(List<int> ids)
        {
            return db.Where(x => x.IsDeleted == false && ids.Contains(x.Id)).ToList();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return db.Where(x => x.IsDeleted == false).ToList();
        }

        public async Task UpdateAsync(Product product)
        {

        }

        public async Task AddAsync(Product product)
        {
            product.Id = db.Max(x => (int?)x.Id).GetValueOrDefault(0) + 1;
            db.Add(product);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await GetOneAsync(id);
            if (itemToDelete != null)
            {
                itemToDelete.IsDeleted = true;
            }
        }
    }
}
