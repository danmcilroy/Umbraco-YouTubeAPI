using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace UmbracoYouTubeAPI.Site.Models
{
    public class HomeViewModel : Home
    {
        public HomeViewModel(IPublishedContent content) : base(content)
        {
        }

        public IEnumerable<Video> Videos { get; set; }
    }
}