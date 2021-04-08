using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTuaPizza.Models
{
    public class Register
    {
        public Register()
        {
            Credentials = new LoginCred();
            User = new Customer();
        }

        public LoginCred Credentials { get; set; }
        public Customer User { get; set; }
    }
}
