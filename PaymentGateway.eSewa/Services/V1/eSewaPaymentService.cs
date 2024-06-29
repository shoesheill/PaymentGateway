using PaymentGateway.Core;

namespace PaymentGateway.eSewa.Services.V1
{
    public class eSewaPaymentService : IPaymentService
    {
        public async Task<T> ProcessPayment<T>(object content, PaymentVersion version)
        {
             var (apiUrl, httpMethod) = PaymentEndpointFactory.GetEndpoint(PaymentMethod.eSewa, version, "PaymentCheck");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            ApiService? apiClient = new ApiService(new HttpClient());
            return await apiClient.GetAsyncResult<T>("", HttpMethod.Post, headers, content);
        }
    }
}
