using AgencyBanking.Entities;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TransactionStatus.Entities
{
    public class TransactionStatusRepository : ITransactionStatusRepository
    {
        private readonly AppSettings _appSettings;
        private IDataProtector _protector;
        private readonly ILogger<TransactionStatusRepository> _logger;

        public TransactionStatusRepository(IOptions<AppSettings> appSettings, ILogger<TransactionStatusRepository> logger, 
            IDataProtectionProvider provider)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _protector = provider.CreateProtector("treyryug");
        }

        public async Task<LogFundsTransfer> getTransactionRef(StatusRequest request)
        {
            LogFundsTransfer response = new LogFundsTransfer();

            var oralConnect = new OracleConnection(_protector.Unprotect(_appSettings.ConnectionString));

            using (oralConnect)
            {
                string query = $@"select * from FUNDSTRANSFERS where requestId = :requestId";

                var r = await oralConnect.QueryAsync<LogFundsTransfer>(query, request);

                response = r.FirstOrDefault();
            }

            return response;
        }

        public async Task<FundsTransferResponse> ValidateTransactionByRef(_StatusRequest _request)
        {
            FundsTransferResponse res = new FundsTransferResponse();
            string reqString; string respMsg = string.Empty; string resultContent = string.Empty;
            int respCode = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    reqString = JsonHelper.toJson(_request);
                    var content = new StringContent(reqString, Encoding.UTF8, Constant.CONTENTTYPE);
                    var result = await client.PostAsync(_appSettings.baseURL + _appSettings.peURL, content);
                    respCode = (int)result.StatusCode;
                    respMsg = result.ReasonPhrase;
                    resultContent = await result.Content.ReadAsStringAsync();
                };
            }
            catch (TaskCanceledException tex)
            {
                respCode = (int)HttpStatusCode.RequestTimeout;
                res = new FundsTransferResponse()
                {
                    message = Constant.TIMEOUT_MSG,
                    status = Constant.TIMEOUT_STATUS
                };

                _logger.LogInformation($"{_request.transactionRef} : {tex.ToString()}");
            }
            catch (Exception ex)
            {
                respCode = (int)HttpStatusCode.InternalServerError;
                res = new FundsTransferResponse()
                {
                    message = ex.Message,
                    status = Constant.FAILED_STATUS
                };

                _logger.LogInformation($"{_request.transactionRef} : {ex.ToString()}");
            }

            if (!string.IsNullOrEmpty(resultContent))
            {
                res = JsonHelper.fromJson<FundsTransferResponse>(resultContent);
            }
            else
            {
                res.message = Constant.UKNOWN_MSG;
                res.status = Constant.UKNOWN_STATUS;
            }

            return res;
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
