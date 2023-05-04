
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

        public Product GetOne(int id)
        {
            return db.Where(x => x.Id == id && x.IsDeleted == false).SingleOrDefault();
        }

        public List<Product> GetAllByIds(List<int> ids)
        {
            return db.Where(x => x.IsDeleted == false && ids.Contains(x.Id)).ToList();
        }

        public List<Product> GetAll()
        {
            return db.Where(x => x.IsDeleted == false).ToList();
        }

        public void  Update(Product product)
        {

        }

        public void Add(Product product)
        {
            product.Id = db.Max(x => (int?)x.Id).GetValueOrDefault(0) + 1;
            db.Add(product);
        }

        public void Delete(int id)
        {
            var itemToDelete = GetOne(id);
            if (itemToDelete != null)
            {
                itemToDelete.IsDeleted = true;
            }
        }
    }
}
