using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IProductRepository
    {
        X GetAllByIdsAsync(X ids);
        X GetAllAsync();
        X GetOneAsync(X id);
        X UpdateAsync(X product);
        X AddAsync(X product);
        X DeleteAsync(X id);
    }
}
