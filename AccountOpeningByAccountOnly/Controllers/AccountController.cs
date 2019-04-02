using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AccountOpening.Entities;
using AgencyBanking.Entities;
using AgencyBanking.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountOpening.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [Produces("application/json")]
    [Route("v1/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountOpeningRepository _orclRepo;

        public AccountController(ILogger<AccountController> logger, IAccountOpeningRepository orclRepo)
        {
            _logger = logger;
            _orclRepo = orclRepo;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Models.Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> create([FromBody] Models.Request request)
        {
            Models.Response a = new Models.Response();
            List<string> messages = new List<string>();           

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                var areq = _orclRepo.GetAccountOpeningRequest(request);

                if (areq == null)
                    return StatusCode((int)HttpStatusCode.BadRequest,
                        Utility.GetResponse(Constant.UNPROCESSABLE_REQUEST, HttpStatusCode.BadRequest));

                a = await _orclRepo.OpenAccount(areq);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.firstName} {request.lastName}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Utility.GetResponse(ex));
            }

            return CreatedAtAction("create", a);
        }      
    }
}