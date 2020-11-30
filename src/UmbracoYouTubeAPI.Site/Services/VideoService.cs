using System.Collections.Generic;
using System.Linq;
using Umbraco.Web.PublishedModels;
using Umbraco.Web;
using UmbracoYouTubeAPI.Site.Entities;
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
            var videoListPage = GetVideoListPage(umbracoHelper);
            var videos = videoListPage.Descendants<Video>();

            return videos;
        }

        public VideoList GetVideoListPage(UmbracoHelper umbracoHelper)
        {
            var siteRoot = umbracoHelper.ContentAtRoot().Where(x => x.Name == "Home").FirstOrDefault();
            var videoListPage = siteRoot.DescendantsOrSelf<VideoList>().FirstOrDefault();

            return videoListPage;
        }

        public bool UpdateVideosWithYouTubePlaylist(Playlist playlist, UmbracoHelper umbracoHelper)
        {
            if (playlist != null)
            {
                var videoListPage = GetVideoListPage(umbracoHelper);
                var videos = videoListPage.Descendants<Video>();

                DeleteAllVideos(videos);
                AddVideos(playlist, videoListPage.Id);

                return true;
            }

            return false;
        }

        private void DeleteAllVideos(IEnumerable<Video> videos)
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
                CreateVideo(parentUdi, youTubeVideo.Snippet.Title, youTubeVideo.Snippet.Thumbnails.Default.Url, youTubeVideo.Snippet.ResourceId.VideoUrl);
            }
        }

        private void CreateVideo(GuidUdi parentUdi, string title, string thumbnailUrl, string videoUrl)
        {
            var video = _contentService.CreateContent(title, parentUdi, "video");

            video.SetValue("title", title);
            video.SetValue("videoThumbnailImageURL", thumbnailUrl);
            video.SetValue("videoLinkURL", videoUrl);

            _contentService.SaveAndPublish(video);
        }
    }
}