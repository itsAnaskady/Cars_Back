using System.ComponentModel.DataAnnotations;

namespace Back1.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
