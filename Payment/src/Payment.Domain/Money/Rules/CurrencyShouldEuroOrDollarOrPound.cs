
namespace Payment.Domain
{
    using System;

    public class CurrencyShouldEuroOrDollarOrPound : IValidationRule
    {
        private readonly string _value;

        internal CurrencyShouldEuroOrDollarOrPound(string value)
        {
            _value = value;
        }

        public string Message => "Currency was not valid";

        public bool IsBroken()
        {
            if (_value is null) return true;

            return (!(Enum.IsDefined(typeof(Currency), _value)));
        }
    }
}
