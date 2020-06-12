namespace Payment.Domain
{
    public class CCV : ValueObject
    {
        private readonly string _value;

        public CCV(string value)
        {
            this.CheckRule(new CCVShouldBeFourOrThreeDigit(value));

            _value = value;
        }
    }
}
