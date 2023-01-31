using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace Methanit_ASP_NET_Core_7.Models
{
    public class Products
    {
        public Guid ProductsId { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Value must be between 3 and 30 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [Range(1, 5000, ErrorMessage = "Year must be between 1 and 5000")]
        public int Default_Quantity { get; set; }


        [ValidateNever]
        public IEnumerable<Fridge_Products> Fridge_Products { get; set; }
    }
}
