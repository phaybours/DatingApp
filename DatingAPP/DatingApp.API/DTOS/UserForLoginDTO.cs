using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOS
{
    public class UserForLoginDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}