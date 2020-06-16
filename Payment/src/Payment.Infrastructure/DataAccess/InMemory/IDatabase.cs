namespace Payment.Infrastructure.DataAccess.InMemory
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    public interface IDatabase
    {
        IDictionary<int, PaymentEntity> Payments { get; }
        IDictionary<int, CardEntity> Cards { get; }
    }
}