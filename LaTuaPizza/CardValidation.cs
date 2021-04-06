using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LaTuaPizza
{
    public class CardValidation : ValidationAttribute
    {
        private CardType _cardTypes;
        public CardType AcceptedCardTypes
        {
            get { return _cardTypes; }
            set { _cardTypes = value; }
        }

        public CardValidation()
        {
            _cardTypes = CardType.All;
        }

        public CardValidation(CardType AcceptedCardTypes)
        {
            _cardTypes = AcceptedCardTypes;
        }


        public override bool IsValid(object value)
        {
            var number = Convert.ToInt32(value);
            return IsValidType(number, _cardTypes);
            Console.WriteLine("test");
        }


        public enum CardType
        {
           
            Visa,
            MasterCard,
            Amex,
            
            All = CardType.Visa | CardType.MasterCard | CardType.Amex,llOrUnknown = CardType.Visa | CardType.MasterCard | CardType.Amex 
        }


        private bool IsValidType(int cardNumber,CardType cardType)
        {
            var cNumber = Convert.ToString(cardNumber);

            // Visa
            if (Regex.IsMatch(cNumber, "^(4)")
                && (CardType.Visa) != 0)
                return cNumber.Length == 16;

            // MasterCard
            if (Regex.IsMatch(cNumber, "^(51|52|53|54|55)")
                && (CardType.MasterCard) != 0)
                return cNumber.Length == 16;

            // Amex
            if (Regex.IsMatch(cNumber, "^(34|37)")
                && (CardType.Amex) != 0)
                return cNumber.Length == 15;

           
           

            return false;
        }

       

    }

    }
   








