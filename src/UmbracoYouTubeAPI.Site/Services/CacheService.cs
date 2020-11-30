using System;
using System.Web;
using System.Web.Caching;

namespace UmbracoYouTubeAPI.Site.Services
{
    public class CacheService : ICacheService
    {
        private Cache _cache;

        public CacheService()
        {
            _cache = HttpContext.Current.Cache;
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Insert(string key, object data, CacheDependency cacheDependency, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            _cache.Insert(key, data, cacheDependency, absoluteExpiration, slidingExpiration);
        }
    }
}
