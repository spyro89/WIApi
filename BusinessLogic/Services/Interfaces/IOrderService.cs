
using BusinessLogic.Dto.Order;
using BusinessLogic.Enums;

namespace BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetList();
        OrderAddResult Add(OrderAddDto order);
        OrderChangeStatusResult ChangeStatus(int id, OrderChangeStatusDto orderChangeStatus);
    }
}
