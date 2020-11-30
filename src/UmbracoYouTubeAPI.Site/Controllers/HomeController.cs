using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.Models;
using UmbracoYouTubeAPI.Site.Models;
using UmbracoYouTubeAPI.Site.Services;

namespace UmbracoYouTubeAPI.Site.Controller
{
    public class HomeController : RenderMvcController
    {
        private readonly IVideoService _videoService;

        public HomeController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public override ActionResult Index(ContentModel model)
        {
            HomeViewModel viewModel = new HomeViewModel(model.Content);
            viewModel.Videos = _videoService.GetAllVideos(Umbraco);

            return View("Home", viewModel);
        }
    }
}