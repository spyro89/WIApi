using DB.Enums;

namespace DB.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerEmail { get; set; }
        public OrderStatus Status { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public bool CanChangeOrderStatus(OrderStatus currentStatus, OrderStatus newStatus)
        {
            if (currentStatus == OrderStatus.Created)
            {
                return newStatus == OrderStatus.Canceled || newStatus == OrderStatus.Finished;
            }
            return false;
        }
    }
}
