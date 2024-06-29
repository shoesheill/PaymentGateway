using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core
{
    public class PaymentEndpointFactory
    {
        public static (string apiUrl, HttpMethod httpMethod) GetEndpoint(PaymentMethod paymentMethod, PaymentVersion version, string action)
        {
            return (paymentMethod, version, action) switch
            {
                (PaymentMethod.eSewa, PaymentVersion.v1, "ProcessPayment") => (ApiEndPoints.eSewa.V1.ProcessPaymentUrl, ApiEndPoints.eSewa.V1.ProcessPaymentMethod),
                (PaymentMethod.eSewa, PaymentVersion.v1, "VerifyPayment") => (ApiEndPoints.eSewa.V1.VerifyPaymentUrl, ApiEndPoints.eSewa.V1.VerifyPaymentMethod),
                (PaymentMethod.eSewa, PaymentVersion.v1, "PaymentCheck") => (ApiEndPoints.eSewa.V1.PaymentCheckUrl, ApiEndPoints.eSewa.V1.PaymentCheckMethod),
                (PaymentMethod.eSewa, PaymentVersion.v2, "ProcessPayment") => (ApiEndPoints.eSewa.V2.ProcessPaymentUrl, ApiEndPoints.eSewa.V2.ProcessPaymentMethod),
                (PaymentMethod.eSewa, PaymentVersion.v2, "VerifyPayment") => (ApiEndPoints.eSewa.V2.VerifyPaymentUrl, ApiEndPoints.eSewa.V2.VerifyPaymentMethod),
                (PaymentMethod.eSewa, PaymentVersion.v2, "PaymentCheck") => (ApiEndPoints.eSewa.V2.PaymentCheckUrl, ApiEndPoints.eSewa.V2.PaymentCheckMethod),
                _ => throw new ArgumentException("Invalid gateway, version, or action", nameof(paymentMethod)),
            };
        }
    }
}
