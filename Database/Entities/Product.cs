namespace DB.Entities
{
    public class Product
    {
        public int Id { get; set; }
        /// <summary>
        /// Product's Name
        /// </summary>
        /// <example>Best product evah</example>
        public string Name { get; set; }
        public short Quantity { get; set; }
        /// <summary>
        /// Product's Price
        /// </summary>
        /// <example>85.5</example>
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
