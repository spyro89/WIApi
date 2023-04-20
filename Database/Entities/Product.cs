namespace DB.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
