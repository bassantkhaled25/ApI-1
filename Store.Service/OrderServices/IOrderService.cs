using store.Services;
using Store.Data.Entities;

namespace store.Services
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderDto orderDto);
        Task<IReadOnlyList<OrderDetailsDto>> GetAllOrderForUserAsync(string buyerEmail);
        Task<OrderDetailsDto> GetOrderByIdAsync(Guid id , string buyerEmail );
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync();
    }
}
