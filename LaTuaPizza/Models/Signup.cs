using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaTuaPizza.Models
{
    public class Signup
    {
        public Signup()
        {
            Address = new HashSet<Address>();
            CardDetails = new HashSet<CardDetails>();
            OrderInfo = new HashSet<OrderInfo>();
        }
        [Key]
        public int Signup_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public int Phone_Number { get; set; }
        public string Password { get; set; }
        public string Confirm_Pass { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postal_code { get; set; }

           

        public virtual LoginCred EmailNavigation { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<CardDetails> CardDetails { get; set; }
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}
