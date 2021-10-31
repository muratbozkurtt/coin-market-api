using System;
using System.Threading.Tasks;
using CoinMarker.Infrastructure.Request;

namespace CoinMarker.Infrastructure.Helper
{

        public interface IApiHelper<out T> where T:class
        {
            Task<IResponse> GetAsync<IResponse>(string url, string method,IRequest request);
        }
}