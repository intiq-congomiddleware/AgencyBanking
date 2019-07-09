using AccountBlock.Entities;
using AgencyBanking.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using AgencyBanking.Helpers;
using AccountBlock.Models;

namespace AccountBlock.Entities
{
    public class AccountBlockRepository : IAccountBlockRepository
    {
        private readonly AppSettings _settings;
        private readonly ILogger<AccountBlockRepository> _logger;
        private IDataProtector _protector;

        public AccountBlockRepository(IOptions<AppSettings> settings,
            ILogger<AccountBlockRepository> logger, IDataProtectionProvider provider)
        {
            _settings = settings.Value;
            _logger = logger;
            _protector = provider.CreateProtector("treyryug");
        }     

        public async Task<AccountBlockResponse> BlockAccount(AccountBlockRequest request)
        {
            AccountBlockResponse res = new AccountBlockResponse();
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
            catch (TaskCanceledException tex)
            {
                respCode = (int)HttpStatusCode.RequestTimeout;
                res = new AccountBlockResponse()
                {
                    message = Constant.TIMEOUT_MSG,
                };

                _logger.LogError($"{request.accountNumber} : {tex.ToString()}");
            }
            catch (Exception ex)
            {
                respCode = (int)HttpStatusCode.InternalServerError;
                res = new AccountBlockResponse()
                {
                    message = $"{Constant.ACC_CREATION_FAILED}: {ex.Message}"
                };

                _logger.LogError($"{request.accountNumber} : {ex.ToString()}");
            }

            if (!string.IsNullOrEmpty(resultContent))
            {
                res = JsonHelper.fromJson<AccountBlockResponse>(resultContent);
            }
            else
            {
                res.message = Constant.UKNOWN_MSG;
            }

            _logger.LogInformation($"{request.accountNumber} : {res.status} : {res.message}");

            return res;
        }

        public bool isDuplicateID(string idString)
        {
            bool isDuplicate = false;

            try
            {
                isDuplicate = Utility.isDuplicateID(idString, _protector.Unprotect(_settings.ConnectionString),
                    _settings.FlexSchema, _settings.tableName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{idString}:- {Environment.NewLine} {ex.ToString()}");
                isDuplicate = true;
            }

            return isDuplicate;
        }

        public string EncData(string value)
        {
            string output = string.Empty;
            try
            {
                output = _protector.Protect(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return output;
        }

        public AccountBlockRequest GetAccountBlockRequest(Request request)
        {
            return new AccountBlockRequest()
            {
                accountNumber = request.accountNumber
            };
        }
    }
}
