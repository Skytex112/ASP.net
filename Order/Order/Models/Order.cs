namespace OrderAPi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public List<OrderItem>? Items { get; set; }
        public decimal TotalAmount {  get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
