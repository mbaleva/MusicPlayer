namespace MusicPlayer.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Artist
    {
        public Artist()
        {
            this.Songs = new HashSet<Song>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
