using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Channels.Helpers;
using FundsTransfer.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FundsTransfer.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [Produces("application/json")]
    [Route("v1/funds")]
    [ApiController]
    public class FundsController : Controller
    {
        private readonly ILogger<FundsController> _logger;
        private readonly IFundsTransferRepository _orclRepo;

        public FundsController(ILogger<FundsController> logger, IFundsTransferRepository orclRepo)
        {
            _logger = logger;
            _orclRepo = orclRepo;
        }

        [HttpPost("transfer")]
        [ProducesResponseType(typeof(FundsTransferResponse), 200)]
        [ProducesResponseType(typeof(FundsTransferResponse), 400)]
        [ProducesResponseType(typeof(FundsTransferResponse), 500)]
        public async Task<IActionResult> transfer([FromBody] FundsTransferRequest request)
        {
            FundsTransferResponse a = new FundsTransferResponse();
            List<string> messages = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                a = await _orclRepo.FundsTransfer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.dract} {request.trnamt}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("transfer", a);
        }
    }
}