using Newtonsoft.Json;
using PaymentGateway.Core;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
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
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            eSewaRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<eSewaRequest>(json) ?? throw new ArgumentException("Invalid content type", nameof(content));

            // Generate the signature
            string message = $"total_amount={request.TotalAmount},transaction_uuid={request.TransactionUuid},product_code={request.ProductCode}";
            request.Signature = HmacHelper.GenerateHmacSha256Signature(message, _secretKey);
            var (apiUrl, httpMethod) = PaymentEndpointFactory.GetEndpoint(PaymentMethod.eSewa, version, PaymentAction.ProcessPayment, _paymentMode);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            var formContent = new Dictionary<string, string>
            {
                { "amount", request.Amount.ToString() },
                { "tax_amount", request.TaxAmount.ToString() },
                { "total_amount", request.TotalAmount.ToString() },
                { "transaction_uuid", request.TransactionUuid },
                { "product_code", request.ProductCode },
                { "product_service_charge", request.ProductServiceCharge.ToString() },
                { "product_delivery_charge", request.ProductDeliveryCharge.ToString() },
                { "success_url", request.SuccessUrl },
                { "failure_url", request.FailureUrl },
                { "signed_field_names", request.SignedFieldNames },
                { "signature", request.Signature }
            };
            // Send the request
            var response = await new ApiService(new HttpClient()).GetAsyncResult<string>(apiUrl, httpMethod, headers, formContent);
            ApiResponse apiResponse = new ApiResponse { data = response };
            if (string.Equals(apiUrl, response))
            {
                apiResponse.status = HttpStatusCode.BadRequest;
                apiResponse.error_code = (int)HttpStatusCode.BadRequest;
                apiResponse.success = false;
            }

            return (T)Convert.ChangeType(apiResponse, typeof(T));
        }

        public async Task<T> VerifyPayment<T>(object content, PaymentVersion version)
        {
            if (content is not eSewaRequest request)
            {
                throw new ArgumentException("Invalid content type", nameof(content));
            }

            string url = $"https://epay.esewa.com.np/api/epay/transaction/status/?product_code={request.ProductCode}&total_amount={request.TotalAmount}&transaction_uuid={request.TransactionUuid}";
            var response = await new ApiService(new HttpClient()).GetAsyncResult<string>(url, HttpMethod.Get, null, null);

            return (T)Convert.ChangeType(response, typeof(T));
        }

        //public async Task<T> PaymentCheck<T>(object content, PaymentVersion version)
        //{
        //    if (content is not eSewaRequest request)
        //    {
        //        throw new ArgumentException("Invalid content type", nameof(content));
        //    }

        //    string url = $"https://epay.esewa.com.np/api/epay/transaction/status/?product_code={request.ProductCode}&total_amount={request.TotalAmount}&transaction_uuid={request.TransactionUuid}";
        //    var response = await _httpClient.GetAsync(url);
        //    response.EnsureSuccessStatusCode();

        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
        //}
    }
}


