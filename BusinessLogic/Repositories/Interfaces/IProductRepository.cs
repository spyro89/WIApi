using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllByIds(List<int> ids);
        List<Product> GetAll();
        Product GetOne(int id);
        void Update(Product product);
        void Add(Product product);
        void Delete(int id);
    }
}
