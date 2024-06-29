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
        public static IPaymentService GetPaymentService(PaymentMethod paymentMethod, PaymentVersion version)
        {
            return (paymentMethod, version) switch
            {
                (PaymentMethod.eSewa, PaymentVersion.v1) => new PaymentGateway.eSewa.Services.V1.eSewaPaymentService(),
                _ => throw new ArgumentException("Invalid gateway or version", nameof(paymentMethod)),
            };
        }
    }
}
