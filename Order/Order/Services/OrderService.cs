using OrderAPi.DTO;
using OrderAPi.Interfaces;
using OrderAPi.Models;

namespace OrderAPI.Services
{
    public class OrderService
    {
        public readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {

            if (string.IsNullOrEmpty(createOrderDTO.CustomerName) || string.IsNullOrEmpty(createOrderDTO.CustomerEmail))
                throw new ArgumentException("Customer name and email are required");

            if (createOrderDTO.Items == null || createOrderDTO.Items.Count == 0)
                throw new ArgumentException("At least one order item is required");

            if (createOrderDTO.DiscountPercent < 0 || createOrderDTO.DiscountPercent > 100)
                throw new ArgumentException("Discount percent must be between 0 and 100");

            decimal TotalPrice = 0;
            foreach (var item in createOrderDTO.Items)
            {
                if (string.IsNullOrEmpty(item.ProductName))
                    throw new ArgumentException("Product name is required for each item");
                if (item.Quantity <= 0)
                    throw new ArgumentException("Item quantity must be greater than zero");
                if (item.Price <= 0)
                    throw new ArgumentException("Item price cannot be negative");
                TotalPrice += item.Price * item.Quantity;
                if (TotalPrice < 100)
                    throw new ArgumentException("Minimum order amount 100.");
                if (TotalPrice > 100000)
                    throw new ArgumentException("The order cannot be more expensive than 100,000.");

            }



            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = createOrderDTO.CustomerName,
                CustomerEmail = createOrderDTO.CustomerEmail,
                Items = createOrderDTO.Items,
                DiscountPercent = createOrderDTO.DiscountPercent,
                CreatedAt = DateTime.UtcNow
            };
            order.TotalAmount = order.Items.Sum(i => i.Price * i.Quantity);
            order.TotalPrice = order.TotalAmount * (1 - order.DiscountPercent / 100);
            if (createOrderDTO.IsVip)
            {
                order.TotalPrice = order.TotalAmount * 0.9m;
            }
            else
            {
                order.TotalPrice = order.TotalAmount * (1 - order.DiscountPercent / 100);
            }

            await _orderRepository.SaveAsync(order);
            return order;
        }
    }
}
