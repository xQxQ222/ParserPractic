using AngleSharp.Browser;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parser.Models;
using Parser.ParserTools;
using System;

namespace Parser.Controllers
{
    [ApiController]
    [Route("/api")]
    public class GovPurchaseController:ControllerBase
    {
        private readonly IParsing parser;
        public GovPurchaseController(IParsing parser)=>this.parser = parser;
        [HttpGet("get/{PurchaseId}")]
        public async Task<ActionResult<string>> GetPurchases([FromRoute] RequestedBodyData body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await parser.Parse<Purchase>(body.PurchaseId);
            return $"{JsonConvert.SerializeObject(res)}\n";
        }
    }
}
