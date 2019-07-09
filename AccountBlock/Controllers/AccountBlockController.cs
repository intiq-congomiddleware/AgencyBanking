using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AccountBlock.Entities;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountBlock.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("v1/account")]
    [ApiController]
    public class AccountBlockController : Controller
    {
        private readonly ILogger<AccountBlockController> _logger;
        private readonly IAccountBlockRepository _orclRepo;
        private readonly IAntiforgery _antiforgery;
        public AccountBlockController(ILogger<AccountBlockController> logger, IAccountBlockRepository orclRepo
            , IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _antiforgery = antiforgery;
        }

        [HttpPost("block")]
        [ProducesResponseType(typeof(AccountBlockResponse), 200)]
        [ProducesResponseType(typeof(AccountBlockResponse), 400)]
        [ProducesResponseType(typeof(AccountBlockResponse), 500)]
        public async Task<IActionResult> block([FromBody] Models.Request request)
        {
            AccountBlockResponse a = new AccountBlockResponse();
            List<string> messages = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                var areq = _orclRepo.GetAccountBlockRequest(request);

                if (areq == null)
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        Utility.GetResponse(Constant.UNPROCESSABLE_REQUEST, HttpStatusCode.BadRequest));

                a = await _orclRepo.BlockAccount(areq);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.accountNumber} {request.requestId}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("block", a);
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