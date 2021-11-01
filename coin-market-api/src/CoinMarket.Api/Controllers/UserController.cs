using System.Linq;
using CoinMarker.Infrastructure.Request;
using CoinMarker.Infrastructure.Settings;
using CoinMarket.Api.Static;
using CoinMarket.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoinMarket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private ILogger<UserController> _logger;
        private UserDbContext _context;
        private readonly IOptions<AppSettings> _appSettings;

        public UserController(UserDbContext context, IOptions<AppSettings> appSettings, ILogger<UserController> logger)
        {
            _logger = logger;
            _appSettings = appSettings;
            _context = context;
        }
        
        [IgnoreAuthorizationCheckAttribute]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
            if (user != null)
            {
                var token = Token.GenerateJwtToken(user, _appSettings.Value.Secret);
                return Ok(token);
            }
            _logger.LogError("Girdiğiniz bilgiler kayıtlarımızla eşleşmemektedir");
            return NotFound();
        }
    }
}