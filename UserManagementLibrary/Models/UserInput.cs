using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserManagementLibrary.Models
{
    public class UserInput
    {
        [Required(ErrorMessage = "The first name field is required.")]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name field is required.")]
        [MinLength(2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The phone number field is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
    }
}
