using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Methanit_ASP_NET_Core_7.Models
{
    public class Fridge
    {
        public Guid FridgeId { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Value must be between 3 and 30 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Value must be between 3 and 30 symbols")]
        public string Owner_Name { get; set; }

        [ValidateNever]
        public string Image { get; set; }


        [Required(ErrorMessage = "You must choose model")]
        public Guid Fridge_ModelId { get; set; }

        [ValidateNever]
        public Fridge_Model Fridge_Model { get; set; }


        [ValidateNever]
        public IEnumerable<Fridge_Products> Fridge_Products { get; set; }
    }
}
