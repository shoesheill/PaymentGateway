namespace PaymentGateway.Core
{
    public static class ApiEndPoints
    {
        public static class eSewa
        {
            public const string BaseUrl = "https://api.esewa.com/";

            public static class V1
            {
                public const string ProcessPaymentUrl = BaseUrl + "v1/payment/process";
                public const string VerifyPaymentUrl = BaseUrl + "v1/payment/verify";
                public const string PaymentCheckUrl = BaseUrl + "v1/payment/check";
                public static readonly HttpMethod ProcessPaymentMethod = HttpMethod.Post;
                public static readonly HttpMethod VerifyPaymentMethod = HttpMethod.Get;
                public static readonly HttpMethod PaymentCheckMethod = HttpMethod.Get;
            }

            public static class V2
            {
                public const string ProcessPaymentUrl = BaseUrl + "v2/payment/process";
                public const string VerifyPaymentUrl = BaseUrl + "v2/payment/verify";
                public const string PaymentCheckUrl = BaseUrl + "v2/payment/check";
                public static readonly HttpMethod ProcessPaymentMethod = HttpMethod.Post;
                public static readonly HttpMethod VerifyPaymentMethod = HttpMethod.Get;
                public static readonly HttpMethod PaymentCheckMethod = HttpMethod.Get;
            }
        }

        public static class Khalti
        {
            public const string BaseUrl = "https://api.khalti.com/";

            public static class V1
            {
                public const string ProcessPaymentUrl = BaseUrl + "v1/payment/process";
                public const string VerifyPaymentUrl = BaseUrl + "v1/payment/verify";
                public const string PaymentCheckUrl = BaseUrl + "v1/payment/check";
                public static readonly HttpMethod ProcessPaymentMethod = HttpMethod.Post;
                public static readonly HttpMethod VerifyPaymentMethod = HttpMethod.Get;
                public static readonly HttpMethod PaymentCheckMethod = HttpMethod.Get;
            }

            public static class V2
            {
                public const string ProcessPaymentUrl = BaseUrl + "v2/payment/process";
                public const string VerifyPaymentUrl = BaseUrl + "v2/payment/verify";
                public const string PaymentCheckUrl = BaseUrl + "v2/payment/check";
                public static readonly HttpMethod ProcessPaymentMethod = HttpMethod.Post;
                public static readonly HttpMethod VerifyPaymentMethod = HttpMethod.Get;
                public static readonly HttpMethod PaymentCheckMethod = HttpMethod.Get;
            }
        }
    }
}
