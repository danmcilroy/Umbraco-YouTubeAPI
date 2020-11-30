using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoYouTubeAPI.Site.Models;
using UmbracoYouTubeAPI.Site.Services;

namespace UmbracoYouTubeAPI.Site.Controller
{
    public class YouTubeController : SurfaceController
    {
        private readonly IVideoService _videoService;
        private readonly IYouTubeService _youTubeService;

        public YouTubeController(IVideoService videoService, IYouTubeService youTubeService)
        {
            _videoService = videoService;
            _youTubeService = youTubeService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            // Get YouTube videos from TED playlist https://www.youtube.com/playlist?list=PLOGi5-fAu8bEyLwr2Ddjq9lDM2Fd0SpCH
            var playlist = _youTubeService.GetVideosByPlaylistId("PLOGi5-fAu8bEyLwr2Ddjq9lDM2Fd0SpCH");
            _videoService.UpdateVideosWithYouTubePlaylist(playlist, Umbraco);

            HomeViewModel viewModel = new HomeViewModel(CurrentPage);
            viewModel.Videos = _videoService.GetAllVideos(Umbraco);

            return View("Home", viewModel);
        }
    }
}