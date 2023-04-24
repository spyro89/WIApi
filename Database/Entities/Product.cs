namespace DB.Entities
{
    public class Product
    {
        public X Id { get; set; }
        public string Name { get; set; }
        public X Quantity { get; set; }
        public X Price { get; set; }
        public X IsDeleted { get; set; }
    }
}
