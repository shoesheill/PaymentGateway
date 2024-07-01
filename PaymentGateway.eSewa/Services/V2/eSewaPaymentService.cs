using PaymentGateway.Core;
using System.Net.Http;
using System.Net.Sockets;
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

            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                byte[] hashBytes = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(message));

                // Convert byte array to Base64 string
                string hash = Convert.ToBase64String(hashBytes);

                Console.WriteLine(hash);
            }

            var (apiUrl, httpMethod) = PaymentEndpointFactory.GetEndpoint(PaymentMethod.eSewa, version, PaymentAction.ProcessPayment, _paymentMode);
            Dictionary<string, string> headers = new Dictionary<string, string>();

            // Create the form content
            //var formContent = new FormUrlEncodedContent(new[]
            //    {
            //    new KeyValuePair<string, string>("amount", request.Amount.ToString()),
            //    new KeyValuePair<string, string>("tax_amount", request.TaxAmount.ToString()),
            //    new KeyValuePair<string, string>("total_amount", request.TotalAmount.ToString()),
            //    new KeyValuePair<string, string>("transaction_uuid", request.TransactionUuid),
            //    new KeyValuePair<string, string>("product_code", request.ProductCode),
            //    new KeyValuePair<string, string>("product_service_charge", request.ProductServiceCharge.ToString()),
            //    new KeyValuePair<string, string>("product_delivery_charge", request.ProductDeliveryCharge.ToString()),
            //    new KeyValuePair<string, string>("success_url", request.SuccessUrl),
            //    new KeyValuePair<string, string>("failure_url", request.FailureUrl),
            //    new KeyValuePair<string, string>("signed_field_names", request.SignedFieldNames),
            //    new KeyValuePair<string, string>("signature", request.Signature)
            //});
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

            //var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            //{
            //    { "amount", request.Amount.ToString() },
            //    { "tax_amount", request.TaxAmount.ToString() },
            //    { "total_amount", request.TotalAmount.ToString() },
            //    { "transaction_uuid", request.TransactionUuid },
            //    { "product_code", request.ProductCode },
            //    { "product_service_charge", request.ProductServiceCharge.ToString() },
            //    { "product_delivery_charge", request.ProductDeliveryCharge.ToString() },
            //    { "success_url", request.SuccessUrl },
            //    { "failure_url", request.FailureUrl },
            //    { "signed_field_names", request.SignedFieldNames },
            //    { "signature", request.Signature }
            //});
           //HttpClient httpClient = new HttpClient();
           //var response = await httpClient.PostAsync("https://rc-epay.esewa.com.np/api/epay/main/v2/form", formContent);
           //response.EnsureSuccessStatusCode();
           //var responseBody = await response.Content.ReadAsStringAsync();
           //return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);

            // Send the request
            return await new ApiService(new HttpClient()).GetAsyncResult<T>(apiUrl, httpMethod, headers, formContent);
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

