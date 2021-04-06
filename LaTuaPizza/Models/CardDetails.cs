using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        [CardValidation(AcceptedCardTypes = CardValidation.CardType.Visa | CardValidation.CardType.MasterCard | CardValidation.CardType.Amex)]
        [Display(Name = "Credit Card Number")]
        [Required(ErrorMessage = "Card Number is required")]
        [Range(100000000000000, 9999999999999999, ErrorMessage = "must be between 15 or 16 digits")]
        public int CardNo { get; set; }

        [Required(ErrorMessage = "Card Holders Name is required")]
        [DisplayName("Card Holders Name")]
        [StringLength(100)]
        public string CardName { get; set; }

        public int Phone { get; set; }
        [Required(ErrorMessage = " Expiry Date is required")]
        public int Expiry { get; set; }
        [Required(ErrorMessage = "Card Type is required")]
        [DisplayName("Card Type")]
        [StringLength(20)]
        public string CardType { get; set; }

        public virtual Customer PhoneNavigation { get; set; }
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }

       

    }
}
