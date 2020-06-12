﻿namespace Payment.Domain
{
    using System.Text.RegularExpressions;
    public class CardNumberShoulPassRegex : IValidationRule
    {
        private readonly string _value;

        internal CardNumberShoulPassRegex(string value)
        {
            this._value = value;
        }
        public string Message => "Card number was not valid";

        public bool IsBroken()
        {
            const string RegExForValidation = @"^4[0-9]{12}(?:[0-9]{3})?$";

            Regex regex = new Regex(RegExForValidation);
            Match match = regex.Match(_value);

            return (!match.Success);
        }
    }
}
