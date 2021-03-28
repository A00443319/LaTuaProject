using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LaTuaPizza.Models
{
    public partial class OrderInfo
    {
        public OrderInfo()
        {
            MenuItem = new HashSet<MenuItem>();
        }

        public int OrdId { get; set; }
        public string CnfNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Phone { get; set; }
        public int AddrId { get; set; }
        public int CardNo { get; set; }
        public int StatusId { get; set; }
        public string PriceBeforeTax { get; set; }
        public string PriceAfterTax { get; set; }

        public virtual Address Addr { get; set; }
        public virtual CardDetails CardNoNavigation { get; set; }
        public virtual Customer PhoneNavigation { get; set; }
        public virtual OrdStatus Status { get; set; }
        public virtual ICollection<MenuItem> MenuItem { get; set; }
    }
}
