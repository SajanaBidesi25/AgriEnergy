using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(Farmer|Employee)$", ErrorMessage = "Role must be either 'Farmer' or 'Employee'")]
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Farmer,
        Employee
    }
}
