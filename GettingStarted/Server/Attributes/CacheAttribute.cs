using GettingStarted.Server.BUS;
using GettingStarted.Server.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace GettingStarted.Server.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveMinutes;
        private const int DEFAULT_EXPIRE_MINUTES = 20;
        public CacheAttribute(int timeToLiveMinutes = DEFAULT_EXPIRE_MINUTES)
        {
            _timeToLiveMinutes = timeToLiveMinutes;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisConfiguration>();
            // cache có chưa? Nếu không thì chạy vào xử lí controller, nếu đã có thì không cần vào các hàm controller và lấy cache
            if (!cacheConfiguration.Enabled)
            {
                await next(); // vào controller
                return;
            }
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var excutedContext = await next();
            if (excutedContext.Result is OkObjectResult objectResult && objectResult.Value != null)
                await cacheService.SetCacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromMinutes(_timeToLiveMinutes));
            else if (excutedContext.Result is ObjectResult okObjectResult && okObjectResult.Value != null)
                await cacheService.SetCacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromMinutes(_timeToLiveMinutes));
        }
        // lấy các parameter của controller
        private static string GenerateCacheKeyFromRequest(HttpRequest request) 
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path }");
            foreach(var (key, value) in request.Query.OrderBy(x => x.Key)){
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
