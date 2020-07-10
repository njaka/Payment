using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain
{
    public class CurrencySouldBeInUse : IValidationRule
    {
        private readonly string _value;

        private readonly bool _inUser;

        internal CurrencySouldBeInUse(string value, bool inUse)
        {
            this._value = value;
            this._inUser = inUse;
        }

        public string Message => $"Currency {_value} is not valid";

        public bool IsBroken()
        {
            return (!_inUser);
        }
    }
}
