using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductListPreview.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }
        public int DisplayOrder {  get; set; }  
    }
}
