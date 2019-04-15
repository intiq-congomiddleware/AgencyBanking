using AccountEnquiry.Entities;
using Channels.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountEnquiry.Entities
{
    public class AccountEnquiryRepository : IAccountEnquiryRepository
    {
        private readonly AppSettings _settings;
        private readonly ILogger<AccountEnquiryRepository> _logger;

        public AccountEnquiryRepository(IOptions<AppSettings> settings, 
            ILogger<AccountEnquiryRepository> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<Tuple<List<AccountEnquiryResponse>, Response>> GetPhoneEnquiryByAccountNumber(PhoneEnquiryRequest request)
        {
            List<AccountEnquiryResponse> br = new List<AccountEnquiryResponse>();
            Response res = new Response();
            string reqString; string respMsg = string.Empty; string resultContent = string.Empty;
            int respCode = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    reqString = JsonHelper.toJson(request);
                    var content = new StringContent(reqString, Encoding.UTF8, Constant.CONTENTTYPE);
                    var result = await client.PostAsync(_settings.baseURL + _settings.peURL, content);
                    respCode = (int)result.StatusCode;
                    respMsg = result.ReasonPhrase;
                    resultContent = await result.Content.ReadAsStringAsync();
                };
            }
            catch (Exception ex)
            {
                respCode = (int)HttpStatusCode.InternalServerError;
                res = new Response()
                {
                    message = ex.Message,
                    status = false
                };

                _logger.LogInformation($"{request.phoneNumber} : {ex.ToString()}");
            }

            res.message = respMsg;
            res.status = respCode == (int)HttpStatusCode.Created || respCode == (int)HttpStatusCode.OK;

            if (!string.IsNullOrEmpty(resultContent))
            {
                if (!res.status)
                {
                    res = JsonHelper.fromJson<Response>(resultContent);
                }
                else
                {
                    br = JsonHelper.fromJson<List<AccountEnquiryResponse>>(resultContent);
                }
            }
            else
            {
                res = new Response()
                {
                    message = Constant.UKNOWN_MSG,
                    status = false
                };
            }

            return new Tuple<List<AccountEnquiryResponse>, Response>(br, res);
        }
    }
}
