using System.ComponentModel.DataAnnotations;

namespace TransactionWebAPI.Models.Dto
{
    public class SignupDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
