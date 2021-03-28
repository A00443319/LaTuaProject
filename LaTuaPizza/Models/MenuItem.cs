using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LaTuaPizza.Models
{
    public partial class MenuItem
    {
        public int OrdId { get; set; }
        public int MenuId { get; set; }
        public int Qty { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual OrderInfo Ord { get; set; }
    }
}
