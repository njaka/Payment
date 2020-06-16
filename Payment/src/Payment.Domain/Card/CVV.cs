namespace Payment.Domain
{
    public class CVV : ValueObject
    {
        private readonly string _value;

        public CVV(string value)
        {
            this.CheckRule(new CCVShouldBeFourOrThreeDigit(value));

            _value = value;
        }

        public override string ToString() => _value;
    }
}
