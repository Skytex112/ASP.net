using OrderAPi.Models;
namespace OrderAPi.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
    }
}
