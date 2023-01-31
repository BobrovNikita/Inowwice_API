using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Methanit_ASP_NET_Core_7.Models
{
    public class Fridge_Model
    {
        public Guid Fridge_ModelId { get; set; }

        [Required(ErrorMessage ="It's require field")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Value must be between 3 and 30 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "It's require field")]
        [Range(1900, 2023, ErrorMessage = "Year must be between 1900 and 2023")]
        public int Year { get; set; }

        [ValidateNever]
        public IEnumerable<Fridge> Fridges { get; set; }

    }
}
