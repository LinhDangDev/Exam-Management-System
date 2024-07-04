namespace GettingStarted.Server.BUS
{
    public interface IResponseCacheService
    {
        Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
        // vì khi lấy ra là dạng document, dùng serialize, deserialize
        Task<string> GetCacheResponseAsync(string cacheKey);

        Task RemoveCacheResponseAsync(string pattern);
    }
}
