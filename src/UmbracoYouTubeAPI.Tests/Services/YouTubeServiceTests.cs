using System;
using System.Collections.Generic;
using NUnit.Framework;
using UmbracoYouTubeAPI.Site.Services;
using UmbracoYouTubeAPI.Site.Entities;
using System.Linq;
using Moq;

namespace Tests
{
    public class YouTubeServiceTests
    {
        private IYouTubeService _youTubeService;
        private Mock<ICacheService> _cacheService;

        [SetUp]
        public void Setup()
        {
            _cacheService = new Mock<ICacheService>();

            _youTubeService = new YouTubeService(_cacheService.Object);
        }

        [Test]
        public void GetVideosByPlaylistId_ReturnsValidFirstVideoFromApi()
        {
            _cacheService.Setup(x => x.Get(It.IsAny<string>())).Returns(null);

            Playlist youTubePlaylist = _youTubeService.GetVideosByPlaylistId("PLOGi5-fAu8bEyLwr2Ddjq9lDM2Fd0SpCH");

            Assert.Greater(youTubePlaylist.Items.Count, 0);

            var youTubeVideo = youTubePlaylist.Items.FirstOrDefault().Snippet;
            Assert.IsNotNull(youTubeVideo);

            Assert.IsNotEmpty(youTubeVideo.Title);
            Assert.IsNotEmpty(youTubeVideo.Description);
        }

        [Test]
        public void GetVideosByPlaylistId_VerifyCacheIsCheckedForPlaylist()
        {
            _cacheService.Setup(x => x.Get(It.IsAny<string>())).Returns(null);

            Playlist youTubePlaylist = _youTubeService.GetVideosByPlaylistId("PLOGi5-fAu8bEyLwr2Ddjq9lDM2Fd0SpCH");

            _cacheService.Verify(x => x.Get(It.IsAny<string>()));
        }

        [Test]
        public void GetVideosByPlaylistId_VerifyCacheInsertApiPlaylistIntoCache()
        {
            _cacheService.Setup(x => x.Get(It.IsAny<string>())).Returns(null);

            Playlist youTubePlaylist = _youTubeService.GetVideosByPlaylistId("PLOGi5-fAu8bEyLwr2Ddjq9lDM2Fd0SpCH");

            _cacheService.Verify(x => x.Insert(It.IsAny<string>(), It.IsAny<object>(), null, It.IsAny<DateTime>(), It.IsAny<TimeSpan>()));
        }

        [Test]
        public void GetVideosByPlaylistId_ReturnsValidVideoFromCache()
        {
            var playlist = new Playlist() {
                Items = new List<Items> {
                    new Items { Snippet = new Snippet() {
                        Title = "His Holiness Pope Francis",
                        Thumbnails = new Thumbnails() {
                            Default = new Image() { Url = "https://i.ytimg.com/vi/KHuwaKrHuR0/default.jpg" }
                        },
                        ResourceId = new Resource { VideoId = "KHuwaKrHuR0" } } }
                }
            };

            _cacheService.Setup(x => x.Get(It.IsAny<string>())).Returns(playlist);

            Playlist youTubePlaylist = _youTubeService.GetVideosByPlaylistId("PLOGi5-fAu8bEyLwr2Ddjq9lDM2Fd0SpCH");

            Assert.Greater(youTubePlaylist.Items.Count, 0);

            var youTubeVideo = youTubePlaylist.Items.FirstOrDefault().Snippet;
            Assert.IsNotNull(youTubeVideo);

            Assert.AreEqual("His Holiness Pope Francis", youTubeVideo.Title);
            Assert.AreEqual("https://i.ytimg.com/vi/KHuwaKrHuR0/default.jpg", youTubeVideo.Thumbnails.Default.Url);
            Assert.AreEqual("KHuwaKrHuR0", youTubeVideo.ResourceId.VideoId);
            Assert.AreEqual("https://www.youtube.com/watch?v=KHuwaKrHuR0", youTubeVideo.ResourceId.VideoUrl);
        }
    }
}