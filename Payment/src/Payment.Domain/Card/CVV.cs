using System.Collections.Generic;

namespace Payment.Domain
{
    public class CVV : ValueObject<CVV>
    {
        private readonly string _value;

        public CVV(string value)
        {
            this.CheckRule(new CCVShouldBeFourOrThreeDigit(value));

            _value = value;
        }

        public override string ToString() => _value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}
