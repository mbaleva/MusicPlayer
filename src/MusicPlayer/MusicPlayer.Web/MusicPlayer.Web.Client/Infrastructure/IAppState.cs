namespace MusicPlayer.Web.Client.Infrastructure
{
    public interface IAppState
    {
        string SessionId { get; set; }

        bool IsLoggedIn { get; }

        string Username { get; set; }

        string UserToken { get; set; }
    }
}
