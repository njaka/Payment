﻿namespace Payment.Domain
{
    using System.Text.RegularExpressions;

    public class CCVShouldBeFourOrThreeDigit : IValidationRule
    {
        private readonly string _value;

        internal CCVShouldBeFourOrThreeDigit(string value)
        {
            this._value = value;
        }

        public string Message => "CCV should be Four or Three digit";

        public bool IsBroken()
        {
            const string RegExForValidation = @"/^[0-9]{3,4}$/";

            Regex regex = new Regex(RegExForValidation);
            Match match = regex.Match(_value);

            return (!match.Success);
        }
    }
}
