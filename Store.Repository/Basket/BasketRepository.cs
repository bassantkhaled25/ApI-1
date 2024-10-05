using StackExchange.Redis;
using Store.Repository.Basket.Models;
using System.Text.Json;

namespace Store.Repository.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)                               //inject IConnectionMultiplexer

        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)

           => await _database.KeyDeleteAsync(basketId);                                      //rediskey => string

        public async Task<CustomerBasket> GetBasketAsync(string basketId)

        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);      //redisvalue(string)=>object (Deserilize)
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)

        {
            var isCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));     //هيتخزن as a json

            if (!isCreated)
                return null;
            return await GetBasketAsync(basket.Id);                                 
        }

      

      
    }
}
