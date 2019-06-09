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

namespace StatementGeneration.Entities
{
    public class StatementRepository : IStatementRepository
    {
        private readonly AppSettings _appSettings;
        private IDataProtector _protector;
        private readonly ILogger<StatementRepository> _logger;

        public StatementRepository(IOptions<AppSettings> appSettings, IDataProtectionProvider provider, ILogger<StatementRepository> logger)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _protector = provider.CreateProtector("treyryug");
        }
        public async Task<Tuple<List<StatementResponse>, Response>> GenerateStatement(StatementRequest request)
        {
            List<StatementResponse> res = new List<StatementResponse>();
            Response _res = new Response();
            string reqString; string respMsg = string.Empty; string resultContent = string.Empty;
            int respCode = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    reqString = JsonHelper.toJson(request);
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
                _res = new Response()
                {
                    message = Constant.TIMEOUT_MSG,
                    status = false
                };

                _logger.LogInformation($"{request.accountNumber} : {tex.ToString()}");
            }
            catch (Exception ex)
            {
                respCode = (int)HttpStatusCode.InternalServerError;
                _res = new Response()
                {
                    message = ex.Message,
                    status = false
                };

                _logger.LogInformation($"{request.accountNumber} : {ex.ToString()}");
            }

            if (!string.IsNullOrEmpty(resultContent))
            {
                _res = JsonHelper.fromJson<Response>(resultContent);
                res = JsonHelper.fromJson<List<StatementResponse>>(resultContent);
            }
            else
            {
                _res.message = Constant.UKNOWN_MSG;
                _res.status = false;
            }

            return new Tuple<List<StatementResponse>, Response>(res, _res);
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
