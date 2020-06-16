using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Infrastructure.DataAccess.InMemory
{
    public class CardEntity
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public string CVV { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual ICollection<PaymentEntity> Payments { get; set; }
    }
}
