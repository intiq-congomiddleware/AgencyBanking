using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using CurrencyRates.Entities;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    //[ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("v1/currency")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly ICurrencyRepository _orclRepo;
        private readonly IAntiforgery _antiforgery;
        public CurrencyController(ILogger<CurrencyController> logger, ICurrencyRepository orclRepo
            , IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _antiforgery = antiforgery;
        }

        [HttpPost("rates")]
        [ProducesResponseType(typeof(CurrencyResponse), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> rates(CurrencyRequest request)
        {
            CurrencyResponse a = new CurrencyResponse();
            List<string> messages = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                a = await _orclRepo.GetRates(request);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("rates", a);
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

        [HttpGet("encdata/{value}")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> encdata(string value)
        {
            return Ok(_orclRepo.EncData(value));
        }
    }
}