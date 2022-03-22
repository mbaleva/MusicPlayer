namespace MusicPlayer.Services.Songs
{
    using System.Threading.Tasks;
    using MusicPlayer.Web.Shared.Songs;
    public interface ISongsService
    {
        Task<int> AddSongAsync(AddSongInputModel model);
    }
}
