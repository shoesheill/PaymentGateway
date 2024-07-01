using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.eSewa
{
    public class eSewaRequest
    {
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public required string TransactionUuid { get; set; }
        public required string ProductCode { get; set; }
        public decimal ProductServiceCharge { get; set; }
        public decimal ProductDeliveryCharge { get; set; }
        public required string SuccessUrl { get; set; }
        public required string FailureUrl { get; set; }
        public required string SignedFieldNames { get; set; }
        public string Signature { get; set; }
    }
}
