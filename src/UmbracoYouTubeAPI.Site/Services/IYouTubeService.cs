using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using UmbracoYouTubeAPI.Site.Entities;

namespace UmbracoYouTubeAPI.Site.Services
{
    public interface IYouTubeService
    {
        Playlist GetVideosByPlaylistId(string playlistId);
    }
}
