
using Database;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetOneAsync(int id);
        Task UpdateAsync(Order order);
        Task AddAsync(Order order);
        Task<bool> ExistsAnyActiveOrderWithSelectedProductAsync(int productId);
    }
}
