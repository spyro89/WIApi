using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories.DbImplementations
{
    public class DbProductRepository : IProductRepository
    {
        private readonly IWIApiContext _context;

        public DbProductRepository(IWIApiContext context)
        {
            _context=context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products
                .SingleOrDefault(x => x.Id == id && x.IsDeleted == false);

            if (product is null) return;

            product.IsDeleted = true;

            _context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _context.Products
                .Where(x => x.IsDeleted == false)
                .ToList();
        }

        public List<Product> GetAllByIds(List<int> ids)
        {
            return _context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }

        public Product GetOne(int id)
        {
            return _context.Products
                .SingleOrDefault(x => x.Id == id && x.IsDeleted == false);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}