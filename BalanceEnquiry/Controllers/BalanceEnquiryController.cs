using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Channels.Entities;
using Channels.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BalanceEnquiry.Entities;
using System.Net;

namespace BalanceEnquiry.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [Produces("application/json")]
    [Route("v1/balance")]
    [ApiController]
    public class BalanceEnquiryController : Controller
    {
        private readonly ILogger<BalanceEnquiryController> _logger;
        private readonly IBalanceEnquiryRepository _orclRepo;

        public BalanceEnquiryController(ILogger<BalanceEnquiryController> logger, IBalanceEnquiryRepository orclRepo)
        {
            _logger = logger;
            _orclRepo = orclRepo;
        }

        [HttpPost("enquiry")]
        [ProducesResponseType(typeof(BEResponse), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> enquiry([FromBody]BalanceEnquiryRequest request)
        {
            BEResponse br = new BEResponse();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(Utility.GetResponse(ModelState));

                var tuple = await _orclRepo.GetBalanceByAccountNumber(request);

                if (!tuple.Item2.status)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, tuple.Item2);
                }

                br = tuple.Item1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return CreatedAtAction("enquiry", br);
        }
    }
}
