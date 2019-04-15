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

namespace BalanceEnquiry.Entities
{
    public class BalanceEnquiryRepository : IBalanceEnquiryRepository
    {
        private readonly AppSettings _settings;
        private readonly ILogger<BalanceEnquiryRepository> _logger;

        public BalanceEnquiryRepository(IOptions<AppSettings> settings, 
            ILogger<BalanceEnquiryRepository> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<Tuple<BEResponse, Response>> GetBalanceByAccountNumber(BalanceEnquiryRequest request)
        {
            BalanceEnquiryResponse br = new BalanceEnquiryResponse();
            BEResponse ber = new BEResponse();
            Response res = new Response();
            string reqString; string respMsg = string.Empty; string resultContent = string.Empty;
            int respCode = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    reqString = JsonHelper.toJson(request);
                    var content = new StringContent(reqString, Encoding.UTF8, Constant.CONTENTTYPE);
                    var result = await client.PostAsync(_settings.baseURL + _settings.beURL, content);
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
                _logger.LogInformation($"{request.accountnumber} : {ex.ToString()}");
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
                    br = JsonHelper.fromJson<BalanceEnquiryResponse>(resultContent);
                    ber = GetBEResponse(br, res);
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

            return new Tuple<BEResponse, Response>(ber, res);
        }

        private BEResponse GetBEResponse(BalanceEnquiryResponse resp, Response res)
        {
            return new BEResponse()
            {
                availableBalance = resp.bal_available,
                message = res.message
            };
        }
    }
}
