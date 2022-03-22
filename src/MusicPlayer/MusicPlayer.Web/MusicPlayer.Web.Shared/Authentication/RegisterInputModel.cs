﻿namespace MusicPlayer.Web.Shared.Authentication
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterInputModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
