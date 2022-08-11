using System.ComponentModel.DataAnnotations;

namespace WebApplicationUI.Models
{
    public class UserLogin
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
