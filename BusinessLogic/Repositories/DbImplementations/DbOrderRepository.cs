using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories.DbImplementations
{
    public class DbOrderRepository : IOrderRepository
    {
        private readonly IWIApiContext _context;

        public DbOrderRepository(IWIApiContext context)
        {
            _context=context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAnyActiveOrderWithSelectedProductAsync(int productId)
        {
            return await _context.OrderItems
                .Where(x => x.ProductId == productId 
                    && x.Order.Status == DB.Enums.OrderStatus.Created)
                .AnyAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(x => x.Items)
                .ToListAsync();
        }

        public async Task<Order> GetOneAsync(int id)
        {
            return await _context.Orders
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}