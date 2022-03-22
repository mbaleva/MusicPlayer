namespace MusicPlayer.Services.Songs
{
    using MusicPlayer.Data;
    using System.Threading.Tasks;
    using MusicPlayer.Web.Shared.Songs;
    using MusicPlayer.Services.DataProviders;
    using System;
    using MusicPlayer.Data.Models;
    public class SongsService : ISongsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IYouTubeDataProviderService youtubeProvider;
        public SongsService(ApplicationDbContext dbContext, 
            IYouTubeDataProviderService youtubeProvider)
        {
            this.dbContext = dbContext;
            this.youtubeProvider = youtubeProvider;

        }
        public async Task<int> AddSongAsync(AddSongInputModel model)
        {
            Song song = new Song();
            var url = youtubeProvider.SearchVideo(model.ArtistName, model.SongName);
            Console.WriteLine($"This video ID is: {url}");
            song.Name = model.SongName;
            song.PlayableUrl = url;

            await this.dbContext.Songs.AddAsync(song);
            await this.dbContext.SaveChangesAsync();
            
            return song.Id;
        }
    }
}
