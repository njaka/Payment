namespace Payment.Domain
{
    public class CardNumber : ValueObject
    {
        private readonly string _value;

        public CardNumber(string value)
        {
            this.CheckRule(new CardNumberShoulPassRegex(value));

            _value = value;
        }

        public override string ToString() => _value;
    }
}
