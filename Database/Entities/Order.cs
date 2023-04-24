//using DB.Enums;

//namespace DB.Entities
//{
//    public class Order
//    {
//        public X Id { get; set; }
//        public DateTime CreateDate { get; set; }
//        public X TotalPrice { get; set; }
//        public string CustomerEmail { get; set; }
//        public X Status { get; set; }

//        public X Items { get; set; } = X;

// 	    public bool CanChangeOrderStatus(OrderStatus currentStatus, OrderStatus newStatus)
//        {
//            if (currentStatus == OrderStatus.Created)
//            {
//                return newStatus == OrderStatus.Canceled || newStatus == OrderStatus.Finished;
//            }
//            return false;
//        }
//    }
//}
