using AccountEnquiry.Entities;
using AgencyBanking.Entities;
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

        public async Task<Tuple<Models.Response, Response>> GetAccountEnquiryByAccountNumber(AccountEnquiryRequest request)
        {
            AccountEnquiryResponse br = new AccountEnquiryResponse();
            Response res = new Response();
            string reqString; string respMsg = string.Empty; string resultContent = string.Empty;
            int respCode = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    reqString = JsonHelper.toJson(request);
                    var content = new StringContent(reqString, Encoding.UTF8, Constant.CONTENTTYPE);
                    var result = await client.PostAsync(_settings.baseURL + _settings.aeURL, content);
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

                _logger.LogInformation($"{request.accountNumber} : {ex.ToString()}");
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
                    br = JsonHelper.fromJson<AccountEnquiryResponse>(resultContent);
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

            return new Tuple<Models.Response, Response>(GetAccountEnquiryResponse(br), res);
        }

        private Models.Response GetAccountEnquiryResponse(AccountEnquiryResponse ar)
        {
            return new Models.Response()
            {
                accountStatusDormant = ar.ac_stat_dormant,
                availableBalance = StringToDecimal(ar.bal_available),
                codAccountNumber = ar.cod_acct_no,
                codAccountTitle = ar.cod_acct_title,
                codCcBrn = ar.cod_cc_brn,
                dateAccountOpen = ar.dat_acct_open
            };
        }

        private decimal StringToDecimal(string value)
        {
            decimal outValue = 0;

            if (!string.IsNullOrEmpty(value) && decimal.TryParse(value, out outValue))
            {
                outValue = Convert.ToDecimal(value);
            }

            return outValue;
        }
    }
}
