using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Channels.Entities;
using Channels.Helpers;
using CashWithdrawal.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CashWithdrawal.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [Produces("application/json")]
    [Route("v1/cash")]
    [ApiController]
    public class CashController : Controller
    {
        private readonly ILogger<CashController> _logger;
        private readonly ICashWithdrawalRepository _orclRepo;

        public CashController(ILogger<CashController> logger, ICashWithdrawalRepository orclRepo)
        {
            _logger = logger;
            _orclRepo = orclRepo;
        }

        [HttpPost("withdrawal")]
        [ProducesResponseType(typeof(FundsTransferResponse), 200)]
        [ProducesResponseType(typeof(FundsTransferResponse), 400)]
        [ProducesResponseType(typeof(FundsTransferResponse), 500)]
        public async Task<IActionResult> withdrawal([FromBody] CashWithdrawalRequest request)
        {
            FundsTransferResponse a = new FundsTransferResponse();
            List<string> messages = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                a = await _orclRepo.CashWithdrawal(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.dract} {request.trnamt}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("withdrawal", a);
        }
    }
}