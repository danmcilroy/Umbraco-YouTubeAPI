using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using UmbracoYouTubeAPI.Site.Entities;
using System.Web.Caching;

namespace UmbracoYouTubeAPI.Site.Services
{
    public class YouTubeService : IYouTubeService
    {
        private const string baseYouTubePlaylistItemsApiUrl = "https://www.googleapis.com/youtube/v3/playlistItems?";
        private readonly ICacheService _cacheService;

        public YouTubeService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Playlist GetVideosByPlaylistId(string playlistId)
        {
            var cachePlaylistKey = $"YouTubeApiPlaylistItems-{playlistId}";

            var cachedYouTubePlaylist = _cacheService.Get(cachePlaylistKey);
            if (cachedYouTubePlaylist != null)
            {
                return cachedYouTubePlaylist as Playlist;
            }

            var youTubePlaylist = GetYouTubePlaylist(playlistId);

            if (youTubePlaylist != null)
            {
                _cacheService.Insert(cachePlaylistKey, youTubePlaylist, null, DateTime.UtcNow.AddMinutes(10), Cache.NoSlidingExpiration);
            }

            return youTubePlaylist;
        }

        private Playlist GetYouTubePlaylist(string playlistId)
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
            var youTubePlaylistJson = GetYouTubePlaylistFromApi(fullYouTubePlaylistItemsApiUrl);
            var youTubePlaylist = JsonConvert.DeserializeObject<Playlist>(youTubePlaylistJson);

            return youTubePlaylist;
        }

        private string GetYouTubePlaylistFromApi(string fullUrl)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(fullUrl).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return result;
                }
            }

            return "";
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