using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BalanceEnquiry.Entities;
using System.Net;
using Microsoft.AspNetCore.Antiforgery;

namespace BalanceEnquiry.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("v1/balance")]
    [ApiController]
    public class BalanceEnquiryController : Controller
    {
        private readonly ILogger<BalanceEnquiryController> _logger;
        private readonly IBalanceEnquiryRepository _orclRepo;
        private readonly IAntiforgery _antiforgery;

        public BalanceEnquiryController(ILogger<BalanceEnquiryController> logger, IBalanceEnquiryRepository orclRepo
            , IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _antiforgery = antiforgery;
        }

        [HttpPost("enquiry")]
        [ProducesResponseType(typeof(Models.Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> enquiry([FromBody]BalanceEnquiryRequest request)
        {
            Models.Response br = new Models.Response();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                var tuple = await _orclRepo.GetBalanceByAccountNumber(request);

                if (!tuple.Item2.status)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, tuple.Item2);
                }

                br = tuple.Item1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return CreatedAtAction("enquiry", br);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult Get()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);

            return new ObjectResult(new
            {
                token = tokens.RequestToken,
                tokenName = tokens.HeaderName
            });
        }
    }
}
