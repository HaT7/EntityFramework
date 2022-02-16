using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EntityFramework.Models
{
    public class User
    {
        [Key] public int UserId { get; set; }

        [Required(ErrorMessage = "Name is must input")]
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
    }
}