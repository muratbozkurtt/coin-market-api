using System.Linq;
using System.Threading.Tasks;
using CoinMarket.Api.Static;
using Microsoft.AspNetCore.Http;

namespace CoinMarket.Api.Extension{

    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["TokenParibu"].FirstOrDefault();
            
            if (!string.IsNullOrWhiteSpace(token))
                context.Request.Headers.Add("Authorization", "Bearer " + token);

            await _next(context);
        }
    }
}