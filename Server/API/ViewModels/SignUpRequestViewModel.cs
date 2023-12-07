using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class SignUpRequestViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password's length cannot be less than 8 symbols")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
