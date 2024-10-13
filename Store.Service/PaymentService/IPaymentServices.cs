using store.Services;
using Store.Service.BasketService.Dtos;

namespace store.Services

{
    public interface IPaymentServices
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto basket);
        Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
