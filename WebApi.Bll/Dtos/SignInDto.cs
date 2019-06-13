using System.ComponentModel.DataAnnotations;

namespace WebApi.Bll.Dtos
{
    public class SignInDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="Password minimum length", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
