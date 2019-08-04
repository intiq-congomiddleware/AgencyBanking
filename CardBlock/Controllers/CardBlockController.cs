using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using CardBlock.Entities;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardBlock.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    //[ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("v1/card")]
    [ApiController]
    public class CardBlockController : Controller
    {
        private readonly ILogger<CardBlockController> _logger;
        private readonly ICardBlockRepository _orclRepo;
        private readonly IAntiforgery _antiforgery;
        public CardBlockController(ILogger<CardBlockController> logger, ICardBlockRepository orclRepo
            , IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _antiforgery = antiforgery;
        }

        [HttpPost("block")]
        [ProducesResponseType(typeof(CardBlockResponse), 200)]
        [ProducesResponseType(typeof(CardBlockResponse), 400)]
        [ProducesResponseType(typeof(CardBlockResponse), 500)]
        public async Task<IActionResult> block([FromBody] Models.Request request)
        {
            CardBlockResponse a = new CardBlockResponse();
            List<string> messages = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                var areq = _orclRepo.GetCardBlockRequest(request);

                if (areq == null)
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        Utility.GetResponse(Constant.UNPROCESSABLE_REQUEST, HttpStatusCode.BadRequest));

                a = await _orclRepo.BlockCard(areq);

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