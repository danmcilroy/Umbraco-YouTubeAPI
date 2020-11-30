using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using UmbracoYouTubeAPI.Site.Entities;

namespace UmbracoYouTubeAPI.Site.Services
{
    public class YouTubeService : IYouTubeService
    {
        private const string baseYouTubePlaylistItemsApiUrl = "https://www.googleapis.com/youtube/v3/playlistItems?";

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

            var fullYouTubePlaylistItemsApiUrl = GetUrlWithQuery(baseYouTubePlaylistItemsApiUrl, parameters);
            var youTubePlaylist = GetYouTubePlaylist(fullYouTubePlaylistItemsApiUrl);

            return youTubePlaylist;
        }

        private Playlist GetYouTubePlaylist(string fullUrl)
        {
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