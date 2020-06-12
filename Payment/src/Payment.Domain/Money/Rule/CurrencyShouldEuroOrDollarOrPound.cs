
namespace Payment.Domain
{
    using System;

    public class CurrencyShouldEuroOrDollarOrPound : IValidationRule
    {
        private readonly Currency _value;

        internal CurrencyShouldEuroOrDollarOrPound(Currency value)
        {
            this._value = value;
        }

        public string Message => "Currency was not valid";

        public bool IsBroken()
        {
            return (!(Enum.IsDefined(typeof(Currency), (int)_value)));
        }
    }
}
