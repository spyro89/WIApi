using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetOne(int id);
        void Update(Order order);
        void Add(Order order);
        bool ExistsAnyActiveOrderWithSelectedProduct(int productId);
    }
}
