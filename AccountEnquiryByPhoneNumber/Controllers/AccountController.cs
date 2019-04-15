using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Channels.Entities;
using AccountEnquiry.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountEnquiry.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [Produces("application/json")]
    [Route("v1/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountEnquiryRepository _orclRepo;

        public AccountController(ILogger<AccountController> logger, IAccountEnquiryRepository orclRepo)
        {
            _logger = logger;
            _orclRepo = orclRepo;
        }

        [HttpPost("enquiry")]
        [ProducesResponseType(typeof(List<AccountEnquiryResponse>), 201)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> enquiry([FromBody] PhoneEnquiryRequest request)
        {
            List<AccountEnquiryResponse> b = new List<AccountEnquiryResponse>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Channels.Helpers.Utility.GetResponse(ModelState));


                var tuple = await _orclRepo.GetPhoneEnquiryByAccountNumber(request);

                if (!tuple.Item2.status)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, tuple.Item2);
                }

                b = tuple.Item1;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.phoneNumber}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, Channels.Helpers.Utility.GetResponse(ex));
            }

            return CreatedAtAction("enquiry", b);
        }       
    }
}