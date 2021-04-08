using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LaTuaPizza.Models
{
    public partial class LoginCred
    {
        public LoginCred()
        {
            Customer = new HashSet<Customer>();
        }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
    }
}
