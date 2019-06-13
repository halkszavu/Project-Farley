using System.ComponentModel.DataAnnotations;

namespace WebApi.Bll.Dtos
{
    public class SignInDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
