using Umbraco.Core;
using Umbraco.Core.Composing;
using UmbracoYouTubeAPI.Site.Services;

namespace UmbracoYouTubeAPI.Site.Composing
{
    public class RegisterServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IVideoService, VideoService>(Lifetime.Request);
            composition.Register<IYouTubeService, YouTubeService>(Lifetime.Request);
            composition.Register<ICacheService, CacheService>(Lifetime.Request);
        }
    }
}