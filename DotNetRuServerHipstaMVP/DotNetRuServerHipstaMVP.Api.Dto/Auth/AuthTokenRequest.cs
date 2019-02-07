using System.ComponentModel.DataAnnotations;

namespace DotNetRuServerHipstaMVP.Api.Dto.Auth
{
    public class AuthTokenRequest
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Email в неправильном формате")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }
    }
}