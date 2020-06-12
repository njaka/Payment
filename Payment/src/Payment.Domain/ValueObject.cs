namespace Payment.Domain
{
    public abstract class ValueObject
    {
        protected void CheckRule(IValidationRule rule)
        {
            if (rule.IsBroken())
            {
                throw new ValueObjectException(rule);
            }
        }
    }
}
