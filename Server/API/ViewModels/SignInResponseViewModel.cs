namespace API.ViewModels
{
    public class SignInResponseViewModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}
