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

        public async Task<Tuple<List<Models.Response>, Response>> GetCustomerEnquiryByAccountNumber(CustomerEnquiryRequest request)
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
                    var result = await client.PostAsync(_settings.baseURL + _settings.ceURL, content);
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

                _logger.LogInformation($"{request.customerNumber} : {ex.ToString()}");
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

            return new Tuple<List<Models.Response>, Response>(GetAccountEnquiryResponse(br), res);
        }

        private List<Models.Response> GetAccountEnquiryResponse(List<AccountEnquiryResponse> ars)
        {
            List<Models.Response> resps = new List<Models.Response>();

            foreach (var ar in ars)
            {
                Models.Response a = new Models.Response()
                {
                    accountStatus = ar.record_stat,
                    accountStatusDormant = ar.ac_stat_dormant,
                    availableBalance = StringToDecimal(ar.bal_available),
                    codAccountNumber = ar.cod_acct_no,
                    codAccountTitle = ar.cod_acct_title,
                    codCcBrn = ar.cod_cc_brn,
                    dateAccountOpen = ar.dat_acct_open,
                    phoneNumber = ar.customer_phonenumber
                };

                resps.Add(a);
            }

            return resps;
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
