using System;
using System.Collections.Generic;

#nullable disable

namespace LaTuaPizza.Models
{
    public partial class SignUp
    {
        public int SignupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
}
