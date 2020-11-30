using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;
using UmbracoYouTubeAPI.Site.Entities;

namespace UmbracoYouTubeAPI.Site.Models
{
    public class HomeViewModel : Home
    {
        public HomeViewModel(IPublishedContent content) : base(content)
        {
        }

        public IEnumerable<Video> Videos { get; set; }

        //public Playlist Playlist { get; set; }
    }
}