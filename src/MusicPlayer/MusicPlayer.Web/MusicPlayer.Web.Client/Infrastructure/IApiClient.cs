namespace MusicPlayer.Web.Client.Infrastructure
{
    using MusicPlayer.Web.Shared;
    using MusicPlayer.Web.Shared.Songs;
    using System.Threading.Tasks;
    public interface IApiClient
    {
        Task<ApiResponse<AddSongResponseModel>> AddSong(AddSongInputModel request);
    }
}
