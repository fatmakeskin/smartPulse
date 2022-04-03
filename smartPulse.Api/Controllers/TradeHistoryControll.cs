using Microsoft.AspNetCore.Mvc;
using smartPulse.Api.Business;

namespace smartPulse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradeHistoryController : ControllerBase
    {
        [HttpGet(nameof(GetTrade))]
        public IActionResult GetTrade(string begDate, string endDate)
        {
            TradeHelper.Instance.GetData(begDate, endDate);
            return Ok();
        }
    }
}
