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

        public async Task<Tuple<Models.Response, Response>> GetBalanceByAccountNumber(BalanceEnquiryRequest request)
        {
            BalanceEnquiryResponse br = new BalanceEnquiryResponse();
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
                    br = JsonHelper.fromJson<BalanceEnquiryResponse>(resultContent);
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

            return new Tuple<Models.Response, Response>(GetBalanceEnquiryResponse(br), res);
        }

        private Models.Response GetBalanceEnquiryResponse(BalanceEnquiryResponse br)
        {
            return new Models.Response()
            {
                accountType = br.acct_type,
                availableBalance = StringToDecimal(br.bal_available),
                codAccountNumber = br.cod_acct_no,
                codCcBrn = br.cod_cc_brn,
                codProd = br.cod_prod,
                currency = br.currency,
                customerName = br.cust_name,
                uncleardBalance = StringToDecimal(br.uncleard_bal)
            };
        }

        private decimal StringToDecimal(string value)
        {
            decimal outValue = 0;

            if(!string.IsNullOrEmpty(value) && decimal.TryParse(value, out outValue))
            {
                outValue = Convert.ToDecimal(value);
            }

            return outValue;
        }
    }
}
