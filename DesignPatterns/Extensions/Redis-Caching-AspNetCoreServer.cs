using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public static class Redis_Caching_AspNetCoreServer
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
            string key,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            {
                options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
                options.SlidingExpiration = unusedExpireTime;

                var jsonData = JsonConvert.SerializeObject(data);
                await cache.SetStringAsync(key, jsonData, options);
            };
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string key)
        {
            var jsonData = await cache.GetStringAsync(key);
            if (jsonData == null)
            {
                return default(T);
            }
            return JSonConvert.DeserializeObject<T>(key);
        }
    }
}

//redis how-to:
//install docker
//in a docker prompt: docker run --name {name} -p {port}:6379 -d redis
//in a docker prompt: docker exec -it {name} sh // launches a shell in the redis container
//in a docker prompt: select 0 //selects the first cache database
//in a docker prompt: scan 0 //lists out the contents of the database

//in ASP.net Core Web Server Project:
//install Nuget package: Microsoft.Extensions.Caching.StackExchangeRedis
//install Nuget package: StackExchange.Redis
//in ConfigureServices in Startup.cs: 
//services.AddStackExchangeRedisCache(options => {
// options.Configuration = Configuration.GetConnectionString("Redis");
// options.InstanceName = "{prepended to keys in cache}";
// });

//in AppSettings.json after adding a comma after AllowedHosts:
// "ConnectionStrings": {
// "Redis": "localhost:{port number}" }

// an example in a cshtml page:
// bring in the Idistributedcache dependency
//private async Task LoadForecast()
//{
// forecasts = null;
// loadLocation = null;
// string key = "WeatherForecast_" + DateTime.Now.ToString("yyyyMMdd_hhmm");

// forecasts = await cache.GetRecordAsync<WeatherForecast[]>(key);
// if (forecasts == null)
// {
// forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
// loadLocation = $"Loaded from API at { DateTime.Now }";
// isCacheData = "";
// await cache.SetRecordAsync(key, forecasts);
// }
// else
// { loadLocation = $"Loaded from the cache at { DateTime.Now }";
//}