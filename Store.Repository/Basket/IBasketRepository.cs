using Store.Repository.Basket.Models;

namespace Store.Repository.Basket
{
    public interface IBasketRepository

    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);  //هتاخد الباسكت كامله تعدل عليها وترجعهالي بعد التعديل
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
