using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagementLibrary.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
