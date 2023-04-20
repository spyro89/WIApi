
using Common.Enums;
using Dto.Order;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetListAsync();
        Task<OrderAddResult> AddAsync(OrderAddDto order);
        Task<OrderChangeStatusResult> ChangeStatusAsync(int id, OrderChangeStatusDto orderChangeStatus);
    }
}
