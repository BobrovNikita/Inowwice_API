using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace Methanit_ASP_NET_Core_7.Models
{
    public class Fridge_Products
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [Range(0, 5000, ErrorMessage = "Year must be between 1 and 5000")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "It's require field")]
        public Guid FridgeId { get; set; }

        [ValidateNever]
        public Fridge Fridges { get; set; }

        [Required(ErrorMessage = "It's require field")]
        public Guid ProductsId { get; set; }

        [ValidateNever]
        public Products Products { get; set; }
    }
}
