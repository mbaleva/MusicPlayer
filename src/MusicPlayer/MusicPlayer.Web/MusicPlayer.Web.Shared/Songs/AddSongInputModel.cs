namespace MusicPlayer.Web.Shared.Songs
{
    using System.ComponentModel.DataAnnotations;
    public class AddSongInputModel
    {
        [Required]
        [MinLength(5)]
        public string SongName { get; set; }
        [Required]
        [MinLength(5)]

        public string ArtistName { get; set; }
    }
}
