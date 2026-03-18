namespace Middleware.Models
{
    public class Order
    {
        public Guid Id { get; set; }          
        public DateTime Date { get; set; }    
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
