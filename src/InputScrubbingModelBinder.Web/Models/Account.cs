using InputScrubbingModelBinder.Web.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace InputScrubbingModelBinder.Web.Models
{
    public class Account
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [CurrencyScrubber]
        public decimal Balance { get; set; }
    }
}
