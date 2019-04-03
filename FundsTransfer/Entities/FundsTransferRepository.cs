using AgencyBanking.Entities;
using FundsTransfer.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FundsTransfer.Entities
{
    public class FundsTransferRepository : IFundsTransferRepository
    {
        private readonly AppSettings _settings;
        private readonly ILogger<FundsTransferRepository> _logger;

        public FundsTransferRepository(IOptions<AppSettings> settings,
            ILogger<FundsTransferRepository> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<FundsTransferResponse> FundsTransfer(FundsTransferRequest request)
        {
            FundsTransferResponse res = new FundsTransferResponse();
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
                res = new FundsTransferResponse()
                {
                    message = Constant.TIMEOUT_MSG,
                    status = Constant.TIMEOUT_STATUS
                };

                _logger.LogInformation($"{request.cract} {request.trnrefno} : {tex.ToString()}");
            }
            catch (Exception ex)
            {
                respCode = (int)HttpStatusCode.InternalServerError;
                res = new FundsTransferResponse()
                {
                    message = ex.Message,
                    status = Constant.FAILED_STATUS
                };

                _logger.LogInformation($"{request.cract} {request.trnrefno} : {ex.ToString()}");
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
        public FundsTransferRequest GetFundsTransferRequest(Request r)
        {
            return new FundsTransferRequest()
            {
                cract = r.creditAccount,
                dract = r.debitAccount,
                trnamt = r.amount,
                txnnarra = r.narration,
                branch_code = r.branch_code,
                instr_code = "0",
                product = _settings.product,
                user_name = r.user_name
            };
        }
    }
}
