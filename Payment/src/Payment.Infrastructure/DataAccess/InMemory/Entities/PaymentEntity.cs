using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Infrastructure.DataAccess.InMemory
{
    public class PaymentEntity
    {
        public int Id { get; set; }
        public Guid PaymentId { get; set; }

        public decimal Amount { get; set; }

        public int Currency { get; set; }

        public DateTime CreateDate { get; set; }

        public int Status { get; set; }

        public virtual CardEntity Card { get; set; }

    }
}
