using System.Collections.Generic;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using UmbracoYouTubeAPI.Site.Entities;

namespace UmbracoYouTubeAPI.Site.Services
{
    public interface IVideoService
    {
        IEnumerable<Video> GetAllVideos(UmbracoHelper umbracoHelper);
        bool UpdateVideosWithYouTubePlaylist(Playlist playlist, UmbracoHelper umbracoHelper);
    }
}
