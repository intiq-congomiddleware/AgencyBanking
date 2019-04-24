using AccountOpening.Models;
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
using Microsoft.AspNetCore.DataProtection;
using AgencyBanking.Helpers;

namespace AccountOpening.Entities
{
    public class AccountOpeningRepository : IAccountOpeningRepository
    {
        private readonly AppSettings _settings;
        private readonly ILogger<AccountOpeningRepository> _logger;
        private IDataProtector _protector;

        public AccountOpeningRepository(IOptions<AppSettings> settings,
            ILogger<AccountOpeningRepository> logger, IDataProtectionProvider provider)
        {
            _settings = settings.Value;
            _logger = logger;
            _protector = provider.CreateProtector("treyryug");
        }

        public async Task<Models.Response> OpenAccount(AccountOpeningRequest request)
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
                    message = $"{Constant.ACC_CREATION_FAILED}: {ex.Message}"
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

            return GetResponse(res);
        }

        public AccountOpeningRequest GetAccountOpeningRequest(Request r)
        {
            return new AccountOpeningRequest()
            {
                ACCOUNT_CLASS = r.accountClass,
                AMOUNTS_CCY = r.amountsCcy,
                BRANCH_CODE = r.branchCode,
                COUNTRY = r.country,
                CUSTOMER_CATEGORY = r.customerCategory,
                CUSTOMER_NAME = r.customerName,
                CUSTOMER_PREFIX = r.customerPrefix,
                CUSTOMER_TYPE = r.customerType,
                DATE_OF_BIRTH = r.dateOfBirth,
                D_ADDRESS1 = r.dAddress1,
                D_ADDRESS2 = r.dAddress2,
                D_ADDRESS3 = r.dAddress3,
                E_MAIL = r.email,
                FIRST_NAME = r.firstName,
                LANGUAGE = r.language,
                LAST_NAME = r.lastName,
                MIDDLE_NAME = r.middleName,
                MINOR = r.minor,
                NATIONALITY = r.nationality,
                SEX = r.sex,
                SHORT_NAME = r.shortName,
                TELEPHONE = r.telephone

            };
        }

        private Models.Response GetResponse(AccountOpeningResponse r)
        {
            return new Models.Response()
            {
                accountNumber = r.accounT_NO,
                branchCode = r.brancH_CODE,
                customerName = r.customeR_NAME,
                customerNumber = r.customeR_NO,
                message = r.message
            };
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
    }
}
