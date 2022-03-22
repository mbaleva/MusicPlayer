namespace MusicPlayer.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Playlists = new HashSet<Playlist>();
        }
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
