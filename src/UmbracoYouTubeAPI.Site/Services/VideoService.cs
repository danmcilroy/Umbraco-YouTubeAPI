using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web.PublishedModels;
using Umbraco.Web;
using UmbracoYouTubeAPI.Site.Entities;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Core;

namespace UmbracoYouTubeAPI.Site.Services
{
    public class VideoService : IVideoService
    {
        public readonly IContentService _contentService;

        public VideoService(IContentService contentService)
        {
            _contentService = contentService;
        }

        public IEnumerable<Video> GetAllVideos(UmbracoHelper umbracoHelper)
        {
            var siteRoot = umbracoHelper.ContentAtRoot().Where(x => x.Name == "Home").FirstOrDefault();
            var videoListPage = siteRoot.DescendantsOrSelf<VideoList>().FirstOrDefault();
            var videos = videoListPage.Descendants<Video>();

            return videos;
        }

        public bool UpdateVideosWithYouTubePlaylist(Playlist playlist, UmbracoHelper umbracoHelper)
        {
            if (playlist != null)
            {
                var siteRoot = umbracoHelper.ContentAtRoot().Where(x => x.Name == "Home").FirstOrDefault();
                var videoListPage = siteRoot.DescendantsOrSelf<VideoList>().FirstOrDefault();
                var videos = videoListPage.Descendants();

                DeleteAllVideos(videos);
                AddVideos(playlist, videoListPage.Id);

                return true;
            }

            return false;
        }

        private void DeleteAllVideos(IEnumerable<IPublishedContent> videos)
        {
            foreach (var video in videos)
            {
                var videoContent = _contentService.GetById(video.Id);
                _contentService.Unpublish(videoContent);
                _contentService.Delete(videoContent);
            }
        }

        private void AddVideos(Playlist playlist, int videoListPageId)
        {
            var parent = _contentService.GetById(videoListPageId);
            var parentUdi = parent.GetUdi();

            foreach (var youTubeVideo in playlist.Items)
            {
                var video = _contentService.CreateContent(youTubeVideo.Snippet.Title, parentUdi, "video");

                video.SetValue("title", youTubeVideo.Snippet.Title);
                video.SetValue("videoThumbnailImageURL", youTubeVideo.Snippet.Thumbnails.Default.Url);
                video.SetValue("videoLinkURL", youTubeVideo.Snippet.ResourceId.VideoUrl);

                _contentService.SaveAndPublish(video);
            }
        }
    }
}