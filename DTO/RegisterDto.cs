using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8,ErrorMessage ="Password must be 8 Character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone(ErrorMessage ="Invalid phone number format")]
        public string PhoneNumber { get; set; }
        public IFormFile ProfilePicture { get; set; }

        [Required(ErrorMessage ="Role is required")]
        public string RoleName { get; set; } = string.Empty;
       
    }
}
