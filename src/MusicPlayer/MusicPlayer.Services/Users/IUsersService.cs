namespace MusicPlayer.Services.Users
{
    using MusicPlayer.Web.Shared.Authentication;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    public interface IUsersService
    {
        Task<LoginResponseModel> Login(LoginInputModel model);

        Task<string> Register(RegisterInputModel model);
    }
}
