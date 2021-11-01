using CoinMarket.Api.Filter;
using Microsoft.AspNetCore.Mvc;

namespace CoinMarket.Api.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}