namespace MusicPlayer.Web.Client.Infrastructure
{
    using System;
    public class AppState : IAppState
    {
        private string userToken;

        private string username;

        public AppState()
        {
            this.SessionId = Guid.NewGuid().ToString();
        }

        public event Action UserChanged;

        public string SessionId { get; set; }

        public bool IsLoggedIn => this.UserToken != null;

        public string Username
        {
            get => this.username;
            set
            {
                this.username = value;
                this.UserChanged?.Invoke();
            }
        }

        public string UserToken
        {
            get => this.userToken;
            set
            {
                this.userToken = value;
                this.UserChanged?.Invoke();
            }
        }
    }
}
