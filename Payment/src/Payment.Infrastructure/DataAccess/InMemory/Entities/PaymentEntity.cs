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

        public string Currency { get; set; }

        public string BeneficiaryAlias { get; set; }

        public byte Status { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual CardEntity Card { get; set; }

    }
}
