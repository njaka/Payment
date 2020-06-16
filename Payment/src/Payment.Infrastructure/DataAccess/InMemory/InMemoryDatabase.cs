
namespace Payment.Infrastructure.DataAccess.InMemory
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class InMemoryDatabase : IDatabase
    {
        public IDictionary<int, PaymentEntity> Payments { get; private set; }
        public IDictionary<int, CardEntity> Cards { get; private set; }

        public InMemoryDatabase()
        {
            Payments = new ConcurrentDictionary<int, PaymentEntity>();
            Cards = new ConcurrentDictionary<int, CardEntity>();
        }

        public InMemoryDatabase(IDictionary<int, PaymentEntity> payments, IDictionary<int, CardEntity> cards)
        {
            Payments = payments;
            Cards = cards;
        }

    }
}
