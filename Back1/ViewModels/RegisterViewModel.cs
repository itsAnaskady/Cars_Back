using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Back1.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string Telephone { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public string Adress { get; set; }


        public string Role { get; set; }
    }
}
