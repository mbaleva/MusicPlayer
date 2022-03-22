namespace MusicPlayer.Services.DataProviders
{
    using Google.Apis.Services;
    using Google.Apis.YouTube.v3;
    using MusicPlayer.Common;
    using Microsoft.Extensions.Options;

    public class YouTubeDataProviderService : IYouTubeDataProviderService
    {
        private readonly YouTubeService youTubeService; 
        public YouTubeDataProviderService(IOptions<ApplicationSettings> options)
        {
            this.YouTubeApiKey = options.Value.YouTubeApiKey;
            this.youTubeService = new YouTubeService(
                new BaseClientService.Initializer
                {
                    ApiKey = this.YouTubeApiKey,
                    ApplicationName = "ListenUp",
                    GZipEnabled = true,
                });
        }
        public string YouTubeApiKey { get; set; }
        public string SearchVideo(string artist, string song) 
        {
            var listRequest = this.youTubeService.Search.List("snippet");
            listRequest.Q = $"{artist} {song}";
            listRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            listRequest.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.None;

            var searchResponse = listRequest.Execute();

            foreach (var searchResult in searchResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    return searchResult.Id.VideoId;
                }
            }

            return null;
        }
    }
}
