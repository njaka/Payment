﻿namespace Payment.Domain
{
    using System.Collections.Generic;

    public interface ICurrencyLookup
    {
        Currency FindCurrency(string currencyCode);
    }
    public class Currency : ValueObject<Currency>
    {
        public string CurrencyCode { get; set; }
        public bool InUse { get; set; }
        public int DecimalPlaces { get; set; }

        public static Currency None = new Currency { InUse = false };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}
