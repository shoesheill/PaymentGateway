using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Interfaces
{
    public interface IEndPointService
    {
        Task<(string apiUrl, HttpMethod httpMethod)> GetEndpoint(string gateway, PaymentVersion version, string action);
    }
}
