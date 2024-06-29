using PaymentGateway.Core;
namespace PaymentGateway.eSewa.Services.V2
{
    public class eSewaPaymentService : IPaymentService
    {

        public Task<T> ProcessPayment<T>(object content, PaymentVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
