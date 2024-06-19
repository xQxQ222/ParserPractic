using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parser.Models;
using Parsers;

namespace Parser.Controllers
{
    [ApiController]
    [Route("/api")]
    public class GovPurchaseController:ControllerBase
    {
        ParserWorker worker = new ParserWorker();
        [Route("/api/get")]
        [HttpPost]
        public async Task<IActionResult> GetPurchases([FromBody] RequestedBodyData body)
        {
            var res = await worker.GetPurchasesAsync(body.PurchaseId);
            return Ok($"{JsonConvert.SerializeObject(res)}\n");
        }
    }
}
