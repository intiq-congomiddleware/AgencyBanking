﻿using Oracle.ManagedDataAccess.Client;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace AgencyBanking.Entities
{
    public class LogToDB
    {
        private readonly AppSettings _appSettings;
        private IDataProtector _protector;

        public LogToDB(IOptions<AppSettings> appSettings, IDataProtectionProvider provider)
        {
            _appSettings = appSettings.Value;
            _protector = provider.CreateProtector("treyryug");
        }

        public async Task<bool> AccountOpeningDump(string request, string response)
        {
            AORequest req = new AORequest();
            AOResponse res = new AOResponse();

            try { req = JsonHelper.fromJson<AORequest>(request); } catch (Exception) { }

            try { res = JsonHelper.fromJson<AOResponse>(response); } catch (Exception) { }

            LogAccountOpening a = new LogAccountOpening()
            {
                accountNo = res.accountNumber,
                branchCode = req.branchCode,
                customerName = res.customerName,
                customerNo = res.customerNumber,
                customerType = req.customerType,
                request = request,
                requestId = req.requestId,
                response = response
            };

            int r = 0;
            var oralConnect = new OracleConnection(_protector.Unprotect(_appSettings.ConnectionString));
            using (oralConnect)
            {
                string queryAccount = $@"INSERT INTO ACCOUNTOPENING (requestId, customerNo, accountNo, customerName, branchCode, customerType, request, response)
                                            VALUES (:requestId, :customerNo, :accountNo, :customerName, :branchCode, :customerType, :request, :response)";

                oralConnect.Open();

                r = await oralConnect.ExecuteAsync(queryAccount, a);
            }

            return (r > 0);
        }

        public async Task<bool> FundsTransferDump(string request, string response)
        {
            FTRequest req = new FTRequest();
            FTResponse res = new FTResponse();

            try { req = JsonHelper.fromJson<FTRequest>(request); } catch (Exception) { }

            try { res = JsonHelper.fromJson<FTResponse>(response); } catch (Exception) { }

            LogFundsTransfer a = new LogFundsTransfer()
            {
                actualTrnAmt = res.actualTrnAmt,
                amount = req.amount,
                branchCode = req.branchCode,
                creditAccount = req.creditAccount,
                debitAccount = req.debitAccount,
                message = res.message,
                narration = req.narration,
                product = _appSettings.product,
                requestId = req.requestId,
                status = res.status,
                trnRefNo = res.trnRefNo,
                userName = req.userName
            };

            int r = 0;
            var oralConnect = new OracleConnection(_protector.Unprotect(_appSettings.ConnectionString));
            using (oralConnect)
            {
                string queryAccount = $@"INSERT INTO FUNDSTRANSFERS (requestId, debitAccount, creditAccount, product, amount, narration, userName, branchCode, status, message, actualTrnAmt, rate, trnRefNo)
                                            VALUES (:requestId, :debitAccount, :creditAccount, :product, :amount, :narration, :userName, :branchCode, :status, :message, :actualTrnAmt, :rate, :trnRefNo)";

                oralConnect.Open();

                r = await oralConnect.ExecuteAsync(queryAccount, a);
            }

            return (r > 0);
        }

        public async Task<bool> AccountBlockDump(string request, string response)
        {
            ABRequest req = new ABRequest();
            Response3 res = new Response3();

            try { req = JsonHelper.fromJson<ABRequest>(request); } catch (Exception) { }

            try { res = JsonHelper.fromJson<Response3>(response); } catch (Exception) { }

            LogAccountBlock a = new LogAccountBlock()
            {
                accountNumber = req.accountNumber,
                message = res.message,
                reason = req.reason,
                status = res.status,
                userName = req.userName,
                request = request,
                requestId = req.requestId,
                response = response
            };

            int r = 0;
            var oralConnect = new OracleConnection(_protector.Unprotect(_appSettings.ConnectionString));
            using (oralConnect)
            {
                string queryAccount = $@"INSERT INTO ACCOUNTBLOCK (requestId, accountNumber, message, status, userName, request, response)
                                            VALUES (:requestId, :accountNumber, :message, :status, :userName, :request, :response)";

                oralConnect.Open();

                r = await oralConnect.ExecuteAsync(queryAccount, a);
            }

            return (r > 0);
        }
    }
}
