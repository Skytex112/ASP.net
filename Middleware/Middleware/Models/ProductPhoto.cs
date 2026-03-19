namespace Middleware.Models
{
    public class ProductPhoto
    {
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }  
        public string? Url { get; set; }        
        public string? Description { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
