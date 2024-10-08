﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using store.Services.CacheServices;
using Store.Service;
using System.Text;

namespace store.Web
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CacheAttribute(int timeToLiveInSeconds)

        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
       
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();

            var cacheKey = GetCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponse = await cacheService.GetCacheResponseAsyc(cacheKey);

            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var excutedContext = await next();

            if (excutedContext.Result is OkObjectResult response)

                await cacheService.SetCacheResponseAsyc(cacheKey, response.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));

        }

        private string GetCacheKeyFromRequest(HttpRequest request)

        {
            var cacheKey = new StringBuilder();

            cacheKey.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                cacheKey.Append($"|{key}-{value}");

            }
            return cacheKey.ToString();
        }
    }

}
