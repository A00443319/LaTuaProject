using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LaTuaPizza.Models
{
    public partial class CardDetails
    {
        public CardDetails()
        {
            OrderInfo = new HashSet<OrderInfo>();
        }

        public int CardNo { get; set; }
        public string CardName { get; set; }
        public int Phone { get; set; }
        public int Expiry { get; set; }
        public string CardType { get; set; }

        public virtual Customer PhoneNavigation { get; set; }
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}
