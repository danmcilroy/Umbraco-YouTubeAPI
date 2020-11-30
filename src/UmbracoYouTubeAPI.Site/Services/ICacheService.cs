using System;
using System.Web.Caching;

namespace UmbracoYouTubeAPI.Site.Services
{
    public interface ICacheService
    {
        object Get(string key);
        void Insert(string key, object data, CacheDependency cacheDependency, DateTime absoluteExpiration, TimeSpan slidingExpiration);
    }
}
