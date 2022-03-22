namespace MusicPlayer.Web.Server.Controllers
{
    using MusicPlayer.Services.Users;
    using MusicPlayer.Web.Shared;
    using MusicPlayer.Web.Shared.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UsersController : ApiController
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
            => this.usersService = usersService;

        [HttpPost]
        public async Task<ApiResponse<LoginResponseModel>> Login([FromBody]LoginInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ModelStateErrors<LoginResponseModel>();
            }
            var responseModel = await this.usersService.Login(model);
            return responseModel.ToApiResponse();
        }
        [HttpPost]
        public async Task<ApiResponse<RegisterResponseModel>> Register([FromBody]RegisterInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ModelStateErrors<RegisterResponseModel>();
            }
            var id = await this.usersService.Register(model);
            return new RegisterResponseModel { Id = id }.ToApiResponse();
        }
    }
}
