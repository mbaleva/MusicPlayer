namespace MusicPlayer.Services.Users
{
    using MusicPlayer.Data.Models;
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
