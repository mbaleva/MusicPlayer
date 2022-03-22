namespace MusicPlayer.Web.Shared.Authentication
{
    using System.ComponentModel.DataAnnotations;
    public class LoginInputModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
