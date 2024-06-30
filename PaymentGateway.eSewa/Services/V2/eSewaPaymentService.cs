using PaymentGateway.Core;
using System.Net.Http;
namespace PaymentGateway.eSewa.Services.V2
{
    public class eSewaPaymentService : IPaymentService
    {
        private readonly string _secretKey;
        private readonly PaymentMode _paymentMode;


        public eSewaPaymentService(string secretKey, PaymentMode paymentMode)
        {
            _secretKey = secretKey;
            _paymentMode = paymentMode;
        }

        public async Task<T> ProcessPayment<T>(object content, PaymentVersion version)
        {
            if (content is not eSewaRequest request)
            {
                throw new ArgumentException("Invalid content type", nameof(content));
            }

            // Generate the signature
            string message = $"{request.TotalAmount},{request.TransactionUuid},{request.ProductCode}";
            request.Signature = HmacHelper.GenerateHmacSha256Signature(message, _secretKey);

            var (apiUrl, httpMethod) = PaymentEndpointFactory.GetEndpoint(PaymentMethod.eSewa, version, PaymentAction.ProcessPayment, _paymentMode);
            Dictionary<string, string> headers = new Dictionary<string, string>();

            // Create the form content
            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("amount", request.Amount.ToString()),
                new KeyValuePair<string, string>("tax_amount", request.TaxAmount.ToString()),
                new KeyValuePair<string, string>("total_amount", request.TotalAmount.ToString()),
                new KeyValuePair<string, string>("transaction_uuid", request.TransactionUuid),
                new KeyValuePair<string, string>("product_code", request.ProductCode),
                new KeyValuePair<string, string>("product_service_charge", request.ProductServiceCharge.ToString()),
                new KeyValuePair<string, string>("product_delivery_charge", request.ProductDeliveryCharge.ToString()),
                new KeyValuePair<string, string>("success_url", request.SuccessUrl),
                new KeyValuePair<string, string>("failure_url", request.FailureUrl),
                new KeyValuePair<string, string>("signed_field_names", request.SignedFieldNames),
                new KeyValuePair<string, string>("signature", request.Signature)
            });

            // Send the request
            return await new ApiService(new HttpClient()).GetAsyncResult<T>(apiUrl, httpMethod, headers, content);
        }

        //    public async Task<T> VerifyPayment<T>(object content, PaymentVersion version)
        //    {
        //        if (content is not eSewaRequest request)
        //        {
        //            throw new ArgumentException("Invalid content type", nameof(content));
        //        }

        //        string url = $"https://epay.esewa.com.np/api/epay/transaction/status/?product_code={request.ProductCode}&total_amount={request.TotalAmount}&transaction_uuid={request.TransactionUuid}";
        //        var response = await _httpClient.GetAsync(url);
        //        response.EnsureSuccessStatusCode();

        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
        //    }

        //    public async Task<T> PaymentCheck<T>(object content, PaymentVersion version)
        //    {
        //        if (content is not eSewaRequest request)
        //        {
        //            throw new ArgumentException("Invalid content type", nameof(content));
        //        }

        //        string url = $"https://epay.esewa.com.np/api/epay/transaction/status/?product_code={request.ProductCode}&total_amount={request.TotalAmount}&transaction_uuid={request.TransactionUuid}";
        //        var response = await _httpClient.GetAsync(url);
        //        response.EnsureSuccessStatusCode();

        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
        //    }
        //}
    }
}

