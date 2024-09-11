using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductListPreview.Models.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category category { get; set; }
        public string ImageaUrl { get; set; }
    }
}
