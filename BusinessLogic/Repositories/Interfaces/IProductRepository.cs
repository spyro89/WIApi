using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllByIds(List<int> ids);
        IEnumerable<Product> GetAll();
        Product GetOne(int id);
        void Update(Product product);
        void Add(Product product);
        void Delete(int id);
    }
}
