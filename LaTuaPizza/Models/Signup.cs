using System;
using System.Collections.Generic;
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
