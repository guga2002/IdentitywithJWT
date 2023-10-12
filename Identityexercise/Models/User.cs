using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identityexercise.Models
{
    [Table("Users")]
    public class User:IdentityUser
    {
        [Key]
        public int  UserID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public byte age { get; set; }

    }
}
