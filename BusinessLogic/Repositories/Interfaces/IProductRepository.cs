using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllByIdsAsync(List<int> ids);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetOneAsync(int id);
        Task UpdateAsync(Product product);
        Task AddAsync(Product product);
        Task DeleteAsync(int id);
    }
}
