namespace MusicPlayer.Services.Users
{
    using MusicPlayer.Data;
    using MusicPlayer.Data.Models;
    using MusicPlayer.Web.Shared.Authentication;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    public class UsersService : IUsersService
    {
        private const string InvalidCredentials = "Invalid username or password";
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtService jwtService;
        private readonly ApplicationDbContext db;

        public UsersService(UserManager<ApplicationUser> userManager,
            IJwtService jwtService, 
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
            this.db = db;
        }
        public async Task<LoginResponseModel> Login(LoginInputModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);


            var password = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && password)
            {
                var token = this.jwtService.GenerateToken(user);
                return new LoginResponseModel { AuthToken = token, UserId = user.Id};
            }
            return null;
        }

        public async Task<string> Register(RegisterInputModel model)
        {
            var user = new ApplicationUser()
            { 
                Email = model.Email,
            };
            var result = await this.userManager.CreateAsync(user, model.Password);
            await this.db.SaveChangesAsync();
            return user.Id;
        }
    }
}
