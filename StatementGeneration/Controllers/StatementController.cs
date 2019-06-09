using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StatementGeneration.Entities;

namespace StatementGeneration.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    //[ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("v1/generate")]
    [ApiController]
    public class StatementController : Controller
    {
        
        private readonly ILogger<StatementController> _logger;
        private readonly IStatementRepository _orclRepo;
        private readonly AppSettings _appSettings;
        private readonly IAntiforgery _antiforgery;

        public StatementController(ILogger<StatementController> logger, IStatementRepository orclRepo
           , IOptions<AppSettings> appSettings, IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _appSettings = appSettings.Value;
            _antiforgery = antiforgery;
        }

        [HttpPost("statement")]
        [ProducesResponseType(typeof(List<StatementResponse>), 201)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> statement([FromBody] StatementRequest request)
        {
            List<StatementResponse> r = new List<StatementResponse>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                request.userId = _appSettings.UserID;
                request.endDate = DateTime.Now;
                request.startDate = request.endDate.AddDays(_appSettings.Duration);

                var gs = await _orclRepo.GenerateStatement(request);

                if (gs != null)
                {
                    if (gs.Item2 != null && !string.IsNullOrEmpty(gs.Item2.message))
                    {
                        return StatusCode((int)HttpStatusCode.ExpectationFailed, gs.Item2);
                    }

                    r = gs.Item1;
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.ExpectationFailed, 
                        new Response() { message = "Statement Generation Failed", status = false });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _logger.LogError($"{request.accountNumber} ::- {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("statement", r);
        }

        [HttpGet("encdata/{value}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> encdata(string value)
        {
            return Ok(_orclRepo.EncData(value));
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