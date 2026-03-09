using OrderAPi.Models;

namespace OrderAPi.DTO
{
    public class CreateOrderDTO
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public List<OrderItem>? Items { get; set; } 
        public decimal DiscountPercent { get; set; }
        public bool IsVip { get; set; }
    }
}
