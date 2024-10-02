namespace Store.Service

{
   public interface ICasheService
   {
        Task SetCacheResponseAsyc(string cacheKey, Object response, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsyc(string cacheKey);
   }
}
