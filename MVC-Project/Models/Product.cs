using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Name Can't aceed 100 Char")]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Description { get; set; }
        [Required]
        [Range(0.1,10000)]
        public decimal Price { get; set; }
        [Required]
        [Range(1,5)]
        public int Rate { get; set; }
        [Required]
        [Range (1,int.MaxValue)]
        public int Quantity { get; set; }
        [ValidateNever]
        public string? Image {  get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
