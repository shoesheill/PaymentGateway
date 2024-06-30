using PaymentGateway.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public static class PaymentServiceFactory
    {
        public static IPaymentService GetPaymentService(PaymentMethod paymentMethod, PaymentVersion version, string secretKey, PaymentMode paymentMode)
        {
            return (paymentMethod, version) switch
            {
                (PaymentMethod.eSewa, PaymentVersion.v2) => new PaymentGateway.eSewa.Services.V2.eSewaPaymentService(secretKey, paymentMode),
                _ => throw new ArgumentException("Invalid gateway or version", nameof(paymentMethod)),
            };
        }
    }
}
