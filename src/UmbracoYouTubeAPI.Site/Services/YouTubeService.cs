using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.PublishedModels;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UmbracoYouTubeAPI.Site.Entities;

namespace UmbracoYouTubeAPI.Site.Services
{
    public class YouTubeService : IYouTubeService
    {
        public Playlist GetVideosByPlaylistId(string playlistId)
        {
            var parameters = new Dictionary<string, string>
            {
                ["key"] = ConfigurationManager.AppSettings.Get("YouTube.APIKey"),
                ["playlistId"] = playlistId,
                ["part"] = "snippet",
                ["fields"] = "pageInfo, items/snippet(title, description, thumbnails, resourceId)",
                ["maxResults"] = "50"
            };

            var baseUrl = "https://www.googleapis.com/youtube/v3/playlistItems?";
            var fullUrl = GetUrlWithQuery(baseUrl, parameters);

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(fullUrl).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Playlist>(result);
                }
            }

            return null;
        }

        private string GetUrlWithQuery(string baseUrl, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                return "";

            if (parameters == null || parameters.Count() == 0)
                return baseUrl;

            string url = parameters.Aggregate(baseUrl, (urlConcat, kvp) => string.Format($"{urlConcat}{kvp.Key}={kvp.Value}&"));

            return url;
        }
    }
}