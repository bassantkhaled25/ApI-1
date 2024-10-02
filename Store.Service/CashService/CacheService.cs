using StackExchange.Redis;
using Store.Service;
using System.Text.Json;

namespace store.Services.CacheServices
{
    public class CacheService :ICasheService 
    {
        private readonly IDatabase _database;                            //هبعتله ال key - هخزن جواه ال db - connection between app and redis
        public CacheService(IConnectionMultiplexer redis)                      //constructor

        {
            _database = redis.GetDatabase();                                 //immemory database
        }

        public async Task<string> GetCacheResponseAsyc(string cacheKey)

        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);

            if (cachedResponse.IsNullOrEmpty)
                return null;

            return cachedResponse;


        }
        public async Task SetCacheResponseAsyc(string cacheKey, object response, TimeSpan timeToLive)

        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serlizedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey , serlizedResponse, timeToLive); //set at cash
        }
      

    }
}
