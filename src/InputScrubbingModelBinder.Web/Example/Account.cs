using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace InputScrubbingModelBinder.Web.Example
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
