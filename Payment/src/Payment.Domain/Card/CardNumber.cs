using System.Text;

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

        public string CardHint => $"{_value.Substring(0, 6)}XXXXXX{_value.Substring(12, _value.Length - 12)}";

        public override string ToString() => _value;
    }
}
