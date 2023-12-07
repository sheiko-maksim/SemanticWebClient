using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class SignInRequestViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password's length cannot be less than 8 symbols")]
        public string? Password { get; set; }

    }
}
