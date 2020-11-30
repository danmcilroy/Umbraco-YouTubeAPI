using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace UmbracoYouTubeAPI.Site.Entities
{
    public class Playlist
    {
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        public Snippet Snippet { get; set; }
    }

    public class Snippet
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Thumbnails Thumbnails { get; set; }
        public Resource ResourceId { get; set; }
    }

    public class Thumbnails
    {
        public Image Default { get; set; }
        public Image Medium { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }

    public class Resource
    {
        public string Kind { get; set; }
        public string VideoId { get; set; }
        [JsonIgnore]
        public string VideoUrl {
            get { return "https://www.youtube.com/watch?v=" + VideoId; }
        }
    }
}