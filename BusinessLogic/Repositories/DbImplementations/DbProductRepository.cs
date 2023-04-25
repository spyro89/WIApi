using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories.DbImplementations
{
    public class DbProductRepository : IProductRepository
    {
        private readonly IWIApiContext _context;

        public DbProductRepository(IWIApiContext context)
        {
            _context=context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products
                .SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (product is null) return;

            product.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByIdsAsync(List<int> ids)
        {
            return await _context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<Product> GetOneAsync(int id)
        {
            return await _context.Products
                .SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}