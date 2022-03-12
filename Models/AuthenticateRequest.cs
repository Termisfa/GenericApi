using System.ComponentModel.DataAnnotations;

namespace GenericApi.Authorization
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Schema { get; set; }
    }
}
