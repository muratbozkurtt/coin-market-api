using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace CoinMarket.Api.Extension{

    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<JWTMiddleware> _logger;
        public JWTMiddleware(RequestDelegate next, ILogger<JWTMiddleware> logger )
        {
            _logger = logger;
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