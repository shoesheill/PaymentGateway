using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.eSewa
{
    public class eSewaResponse
    {
        public required string Status { get; set; }
        public required string Signature { get; set; }
        public required string TransactionCode { get; set; }
        public decimal TotalAmount { get; set; }
        public required string TransactionUuid { get; set; }
        public required string ProductCode { get; set; }
        public required string SuccessUrl { get; set; }
        public required string SignedFieldNames { get; set; }
    }
}
