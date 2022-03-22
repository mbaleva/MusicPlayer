namespace MusicPlayer.Data.Models
{
    using System.Collections.Generic;
    public class Playlist
    {
        public Playlist()
        {
            this.Songs = new HashSet<PlaylistSongs>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<PlaylistSongs> Songs { get; set; }
    }
}
