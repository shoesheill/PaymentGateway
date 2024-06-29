using PaymentGateway.Core;


namespace PaymentGateway.Core
{
    public interface IPaymentService
    {
        Task<T> ProcessPayment<T>(object content, PaymentVersion version);
    }
}
