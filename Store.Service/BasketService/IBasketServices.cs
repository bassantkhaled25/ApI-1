using Store.Service.BasketService.Dtos;

namespace Store.Service.BasketService
{
    public interface IBasketServices
    {
        Task<CustomerBasketDto> GetBasketAsync(string basketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
