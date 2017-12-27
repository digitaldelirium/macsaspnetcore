using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MacsASPNETCore.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string LastName { get; set; }
        [Phone]
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        [Required]
        public int EmailId { get; set; }
        public int AddressId { get; set; }


    }
}
