using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using CashWithdrawal.Entities;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CashWithdrawal.Controllers
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
        private readonly ICashWithdrawalRepository _orclRepo;
        private readonly IAntiforgery _antiforgery;

        public CashController(ILogger<CashController> logger, ICashWithdrawalRepository orclRepo
            , IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _antiforgery = antiforgery;
        }

        [HttpPost("withdrawalpluscharges")]
        [ProducesResponseType(typeof(Models.Response), 200)]
        [ProducesResponseType(typeof(Models.Response), 400)]
        [ProducesResponseType(typeof(Models.Response), 500)]
        public async Task<IActionResult> withdrawalpluscharges([FromBody] Models.Request request)
        {
            FundsTransferResponse a = new FundsTransferResponse();
            List<string> messages = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));


                var areq = _orclRepo.GetCashWithdrawalRequest(request);

                if (areq == null)
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        Utility.GetResponse(Constant.UNPROCESSABLE_REQUEST, HttpStatusCode.BadRequest));

                a = await _orclRepo.CashWithdrawal(areq);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.debitAccount} {request.amount}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("withdrawalpluscharges", _orclRepo.GetFTResponse(a));
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