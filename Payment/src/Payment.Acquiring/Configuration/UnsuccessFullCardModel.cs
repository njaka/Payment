using System.Collections.Generic;

namespace Payment.Acquiring
{ 
    public class UnsuccessFullCardModel
    {
        public string CardNumber { get; set; }

        public string Reason { get; set; }

        public string ErrorCode { get; set; }
    }
}
