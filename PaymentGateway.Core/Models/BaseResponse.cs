﻿using System.Net;

namespace PaymentGateway.Core
{
    public abstract class BaseResponse
    {

        /// <summary>
        /// Defines the status of respose 
        /// </summary>
        public HttpStatusCode status { get; set; } = HttpStatusCode.OK;
        /// <summary>
        /// Clear message that defines the respose clearly
        /// </summary>
        public string message { get; set; } = string.Empty;
        /// <summary>
        /// Holds the result object to be transmitted
        /// </summary>
        public object data { get; set; }
        public bool success { get; set; }
        public int error_code { get; set; }
    }
}
