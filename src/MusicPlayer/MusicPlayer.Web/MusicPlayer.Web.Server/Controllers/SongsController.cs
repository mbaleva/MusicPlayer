namespace MusicPlayer.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MusicPlayer.Services.Songs;
    using MusicPlayer.Web.Shared;
    using MusicPlayer.Web.Shared.Songs;
    using System.Threading.Tasks;

    [Route("/api/[controller]/[action]")]
    public class SongsController : ApiController
    {
        private readonly ISongsService songsService;
        public SongsController(ISongsService songsService)
        {
            this.songsService = songsService;
        }
        public async Task<ApiResponse<AddSongResponseModel>> Add([FromBody] AddSongInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ModelStateErrors<AddSongResponseModel>();
            }
            var response = new AddSongResponseModel 
            {
                Id = await this.songsService.AddSongAsync(model)
            };
            return response.ToApiResponse<AddSongResponseModel>();
        }
    }
}
