using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOS
{
    public class UserForRegisterDTO
    {
        // [Required(ErrorMessage = "Username is required")]
        [Required]
        public string Username { get; set; }

        // [Required(ErrorMessage = "Password is required")]
        [Required]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Password must be a between 6 and 25 Characters")]
        public string Password { get; set; }
    }
}