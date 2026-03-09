using Moq;
using OrderAPi.DTO;
using OrderAPi.Interfaces;
using OrderAPi.Models;
using OrderAPI.Services;
namespace OrderUnitTests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrderAsync_Should_Create_Order_When_Request_Is_Valid()
        {
            var RepositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderAPI.Services.OrderService(RepositoryMock.Object);

            var request = new CreateOrderDTO
            {
                CustomerName = "Test",
                CustomerEmail = "test@test.com",
                DiscountPercent = 10,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductName = "Product 1",
                        Quantity = 2,
                        Price = 100
                    },
                    new OrderItem
                    {
                        ProductName = "Product 2",
                        Quantity = 1,
                        Price = 200
                    }
                }
            };

            var result = orderService.CreateOrderAsync(request);
            Assert.NotNull(result);
            Assert.Equal("test@test.com", result.Result.CustomerEmail);
            Assert.Equal(400, result.Result.TotalAmount);
            Assert.Equal(2, result.Result.Items.Count);
            Assert.Equal(10, result.Result.DiscountPercent);
            RepositoryMock.Verify(r => r.SaveAsync(It.IsAny<Order>()), Times.Once);
        }
        [Fact]
        public async void CreateOrderAsync_Should_Throw_Exception_When_Item_Quantity_Is_Missing()
        {
            var RepositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(RepositoryMock.Object);
            var request = new CreateOrderDTO
            {
                CustomerEmail = "Test@Test.com",
                CustomerName = "Test",
                DiscountPercent = 10,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductName = "Product 1",
                        Price = 100,
                        Quantity = 0
                    }
                }
            };
            var exeption = await Assert.ThrowsAsync<ArgumentException>(() => orderService.CreateOrderAsync(request));
            Assert.Equal("Item quantity must be greater than zero", exeption.Message);

        }
        [Fact]
        public async void CreateOrderAsync_Should_Throw_Exception_When_Discount_Is_Invalid()
        {
            var RepositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(RepositoryMock.Object);

            var request = new CreateOrderDTO
            {
                CustomerName = "TESt",
                CustomerEmail = "TESt@TESt.com",
                DiscountPercent = 0,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    { 
                        ProductName = "Cheap Product",
                        Quantity = 1,
                        Price = 50 
                    }
                }
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => orderService.CreateOrderAsync(request));
            Assert.Equal("Minimum order amount 100.", exception.Message);

        }
        [Fact]
        public async void CreateOrderAsync_Should_Throw_Exception_When_Total_Price_Exceeds_Limit()
        {
            var RepositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(RepositoryMock.Object);

            var request = new CreateOrderDTO
            {
                CustomerName = "TEst",
                CustomerEmail = "TEst@TEst.com",
                DiscountPercent = 0,
                Items = new List<OrderItem>
                {
                    new OrderItem 
                    { 
                        ProductName = "Expensive Product",
                        Quantity = 3,
                        Price = 50000 
                    }
                }
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => orderService.CreateOrderAsync(request));
            Assert.Equal("The order cannot be more expensive than 100,000.", exception.Message);

        }
        [Fact]
        public async void CreateOrderAsync_Should_Throw_Exception_When_Customer_Email_Is_Missing()
        {
            var RepositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(RepositoryMock.Object);

            var request = new CreateOrderDTO
            {
                CustomerName = "VIP",
                CustomerEmail = "vip@test.com",
                DiscountPercent = 10,
                Items = new List<OrderItem>
                {
                    new OrderItem 
                    { 
                        ProductName = "Product",
                        Quantity = 1,
                        Price = 1000 
                    }
                }
            };

            var result = await orderService.CreateOrderAsync(request);

            Assert.NotNull(result);
            Assert.Equal("vip@test.com", result.CustomerEmail);
            Assert.Equal(1000, result.TotalAmount);
            Assert.Equal(900, result.TotalPrice);  
            RepositoryMock.Verify(r => r.SaveAsync(It.IsAny<Order>()), Times.Once);

        }
    }
}