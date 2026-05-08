using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCenterWebApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]
        public string Salt { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }  // <-- новое поле

        [Required]
        public DateTime RegistrationDate { get; set; } // задаём через код при регистрации

        [Required, StringLength(20)]
        public string Role { get; set; } = "User"; // по умолчанию пользователь

    
    }
}





