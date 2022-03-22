namespace MusicPlayer.Services.DataProviders
{
    public interface IYouTubeDataProviderService
    {
        public string SearchVideo(string artist, string song);
    }
}
