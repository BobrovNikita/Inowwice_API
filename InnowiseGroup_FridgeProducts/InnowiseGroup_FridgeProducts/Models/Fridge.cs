using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace FridgeProducts.Models
{
    public class Fridge
    {
        public Guid FridgeId { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Value must be between 3 and 30 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Value must be between 3 and 30 symbols")]
        public string OwnerName { get; set; }

        [ValidateNever]
        public string Image { get; set; }


        [Required(ErrorMessage = "You must choose model")]
        public Guid FridgeModelId { get; set; }

        [ValidateNever]
        public FridgeModel FridgeModel { get; set; }


        [ValidateNever]
        public IEnumerable<FridgeProducts> FridgeProducts { get; set; }
    }
}
