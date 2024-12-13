﻿using System.ComponentModel.DataAnnotations;

namespace WizStore.Models.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
