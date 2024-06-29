using PaymentGateway.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public class PaymentManager
    {
        public async Task<T> ProcessPayment<T>(PaymentMethod paymentMethod, PaymentVersion version, decimal amount, string currency)
        {
            var paymentService = PaymentServiceFactory.GetPaymentService(paymentMethod, version);
            eSewaRequest eSewaRequest = new eSewaRequest
            {
                Amount = amount
            };
            return await paymentService.ProcessPayment<dynamic>(eSewaRequest, version);
        }
    }
}
