using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LaTuaPizza.Models
{
    public partial class Address
    {
        public Address()
        {
            OrderInfo = new HashSet<OrderInfo>();
        }

        public int AddrId { get; set; }
        public int Phone { get; set; }
        public string Street { get; set; }
        public int Unit { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public virtual Customer PhoneNavigation { get; set; }
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}
