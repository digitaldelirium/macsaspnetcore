using System.ComponentModel.DataAnnotations;


namespace MacsASPNETCore.ViewModels
{
    /// <summary>
    ///  Email Message formatting
    /// 
    /// ToDo:  Add input validation by data annotations if possible with .DNX Core
    /// 
    /// <param name="FirstName">First Name of the Person sending email</param>
    /// <param name="LastName">Last name of person sending email</param>
    /// <param name="PhoneNumber">Phone Number of email sender</param>
    /// </summary>
    public class ContactViewModel
    {

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 2)]
        public string EmailBody { get; set; }
    }
}