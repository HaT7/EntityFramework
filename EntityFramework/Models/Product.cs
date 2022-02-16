using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Models
{
    public class Product
    {
        [Key] public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is must input")]
        public string Name { get; set; }

        [MinLength(5, ErrorMessage = "Must at least 5 characters")]
        public string Description { get; set; }
        
        [Required] public int CategoryId { get; set; }
        [ForeignKey("CategoryId")] public Category Category { get; set; }
    }
}