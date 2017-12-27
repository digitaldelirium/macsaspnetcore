using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MacsASPNETCore.Models
{
    public class PhoneNumber
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Phone]
        [Required]
        public string TelNumber { get; set; }

    }
}