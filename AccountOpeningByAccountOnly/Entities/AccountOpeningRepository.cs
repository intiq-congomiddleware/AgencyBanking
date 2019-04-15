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

namespace AccountOpening.Entities
{
    public class AccountOpeningRepository : IAccountOpeningRepository
    {
        private readonly AppSettings _settings;
        private readonly ILogger<AccountOpeningRepository> _logger;

        public AccountOpeningRepository(IOptions<AppSettings> settings,
            ILogger<AccountOpeningRepository> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<AccountOpeningResponse> OpenAccount(AccountOpeningRequest request)
        {
            AccountOpeningResponse res = new AccountOpeningResponse();
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
            catch (TaskCanceledException tex)
            {
                respCode = (int)HttpStatusCode.RequestTimeout;
                res = new AccountOpeningResponse()
                {
                    message = Constant.TIMEOUT_MSG,
                };

                _logger.LogInformation($"{request.FIRST_NAME} {request.LAST_NAME} : {tex.ToString()}");
            }
            catch (Exception ex)
            {
                respCode = (int)HttpStatusCode.InternalServerError;
                res = new AccountOpeningResponse()
                {
                    message = $"{Constant.ACC_CREATION_FAILED} : {ex.Message}"
                };

                _logger.LogInformation($"{request.FIRST_NAME} {request.LAST_NAME} : {ex.ToString()}");
            }

            if (!string.IsNullOrEmpty(resultContent))
            {
                res = JsonHelper.fromJson<AccountOpeningResponse>(resultContent);
                res.message = string.IsNullOrEmpty(res.accounT_NO) ? Constant.ACC_CREATION_FAILED : Constant.ACC_CREATION_SUCCESSFUL;
            }
            else
            {
                res.message = Constant.UKNOWN_MSG;
            }

            return res;
        }
    }
}
