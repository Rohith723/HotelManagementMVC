using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementMVC.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6-9]\d{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        public int? Age { get; set; }

        public string Languages { get; set; } // Stored as comma-separated

        public string Country { get; set; }

        // For checkbox binding
        public List<string> SelectedLanguages { get; set; }
    }
}