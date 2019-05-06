using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using CashDeposit.Entities;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CashDeposit.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("v1/cash")]
    [ApiController]
    public class CashController : Controller
    {
        private readonly ILogger<CashController> _logger;
        private readonly ICashDepositRepository _orclRepo;
        private readonly IAntiforgery _antiforgery;

        public CashController(ILogger<CashController> logger, ICashDepositRepository orclRepo
            , IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _antiforgery = antiforgery;
        }

        [HttpPost("deposit")]
        [ProducesResponseType(typeof(Models.Response), 200)]
        [ProducesResponseType(typeof(Models.Response), 400)]
        [ProducesResponseType(typeof(Models.Response), 500)]
        public async Task<IActionResult> deposit([FromBody] Models.Request request)
        {
            FundsTransferResponse a = new FundsTransferResponse();
            List<string> messages = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                var areq = _orclRepo.GetCashDepositRequest(request);

                if (areq == null)
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        Utility.GetResponse(Constant.UNPROCESSABLE_REQUEST, HttpStatusCode.BadRequest));

                a = await _orclRepo.CashDeposit(areq);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.creditAccount} {request.amount}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("deposit", new Models.Response {  message = a.message, status = a.status });
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
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> encdata(string value)
        {
            return Ok(_orclRepo.EncData(value));
        }
    }
}