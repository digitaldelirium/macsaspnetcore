using System.ComponentModel.DataAnnotations;

namespace MacsASPNETCore.Models
{
    public class Email
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; } 
    }
}