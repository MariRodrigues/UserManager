using System.ComponentModel.DataAnnotations;

namespace UserManager.Features.Users.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string Repassword { get; set; }
    }
}
