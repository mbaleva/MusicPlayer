namespace MusicPlayer.Data.Models
{
    using System.Collections.Generic;
    public class Song
    {
        public Song()
        {
            this.Playlists = new HashSet<PlaylistSongs>();
            this.Metadata = new HashSet<SongMetadata>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string PlayableUrl { get; set; }

        public ICollection<SongMetadata> Metadata { get; set; }
        public ICollection<PlaylistSongs> Playlists { get; set; }
    }
}
