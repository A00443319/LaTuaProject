using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LaTuaPizza.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Address = new HashSet<Address>();
            CardDetails = new HashSet<CardDetails>();
            OrderInfo = new HashSet<OrderInfo>();
        }

        //[PhoneValidate]
        public int Phone { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }

        public virtual LoginCred EmailNavigation { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<CardDetails> CardDetails { get; set; }
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}
