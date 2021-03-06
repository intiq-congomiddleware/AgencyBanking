﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgencyBanking.Entities;
using AccountEnquiry.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Antiforgery;

namespace AccountEnquiry.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AccessAgencyBankingCorsPolicy")]
    [ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("v1/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountEnquiryRepository _orclRepo;
        private readonly IAntiforgery _antiforgery;

        public AccountController(ILogger<AccountController> logger, IAccountEnquiryRepository orclRepo
            , IAntiforgery antiforgery)
        {
            _logger = logger;
            _orclRepo = orclRepo;
            _antiforgery = antiforgery;
        }

        [HttpPost("enquiry")]
        [ProducesResponseType(typeof(List<Models.Response>), 201)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> enquiry([FromBody] CustomerEnquiryRequest request)
        {
            List<Models.Response> b = new List<Models.Response>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(AgencyBanking.Helpers.Utility.GetResponse(ModelState));


                var tuple = await _orclRepo.GetCustomerEnquiryByAccountNumber(request);

                if (!tuple.Item2.status)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, tuple.Item2);
                }

                b = tuple.Item1;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{request.customerNumber}:- {Environment.NewLine} {ex.ToString()}");
                return StatusCode((int)HttpStatusCode.InternalServerError, AgencyBanking.Helpers.Utility.GetResponse(ex));
            }

            return CreatedAtAction("enquiry", b);
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