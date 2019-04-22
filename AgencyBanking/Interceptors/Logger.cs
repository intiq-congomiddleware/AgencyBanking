using AgencyBanking.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgencyBanking.Interceptors
{
    public class Logger
    {

        private readonly RequestDelegate _next;
        IOptions<AppSettings> _settings;
        private readonly ILogger<Logger> _logger;
        private IDataProtector _protector;
        private LogToDB _logToDB;

        public Logger(RequestDelegate next, ILogger<Logger> logger
            , IOptions<AppSettings> settings, LogToDB logToDB)
        {
            _next = next;
            _settings = settings;
            _logger = logger;
            _logToDB = logToDB;
        }

        public async Task Invoke(HttpContext context)
        {
            string reqId = DateTime.Now.ToString(Constant.TIMESTAMP_FORMAT_2);
            string logpath = string.Empty;

            context.Request.EnableRewind();

            var originalRequestBody = context.Request.Body;
            var requestText = await FormatRequest(context.Request);
            var originalResponseBody = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Request.Body.Position = 0;
                context.Response.Body = responseBody;
                context.Request.Body = originalRequestBody;

                List<string> logValues = new List<string>();
                var path = context.Request.Path + context.Request.QueryString;
                var method = context.Request.Method;

                Stopwatch stopwatch = Stopwatch.StartNew();
                await _next(context);
                stopwatch.Stop();

                var responseText = await FormatResponse(context.Response);
                var timestamp = DateTimeOffset.Now.ToString(Constant.TIMESTAMP_FORMAT_1);
                var elapsed = $"{stopwatch.ElapsedMilliseconds.ToString().PadLeft(5)}ms";
                var status = context.Response.StatusCode.ToString();               

                if (_settings.Value.loggerModeOn.Any(path.Contains))
                {
                    try
                    {                        
                        logValues.Add("Elapsed: " + elapsed);
                        logValues.Add("Method: " + method);
                        logValues.Add("Path: " + path);
                        logValues.Add("Request: " + requestText);
                        logValues.Add("Response: " + responseText);
                        logValues.Add("Status: " + status);
                        logValues.Add("TimeStamp: " + timestamp);

                        if (_settings.Value.logAOToDB)
                        {
                            try
                            {
                                await _logToDB.AccountOpeningDump(requestText, responseText);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"_logToDB.AccountOpeningDump:- {ex.ToString()}");
                            }
                        }

                        if (_settings.Value.logFTToDB)
                        {
                            try
                            {
                                await _logToDB.FundsTransferDump(requestText, responseText);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"_logToDB.FundsTransferDump:- {ex.ToString()}");
                            }
                            
                        }

                        reqId = (!string.IsNullOrEmpty(reqId)) ? reqId : DateTime.Now.ToString(Constant.TIMESTAMP_FORMAT_2);

                        _logger.LogInformation($"{reqId}:-" + "{@LogValues}", logValues);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        _logger.LogError($"{ex.ToString()}:- " + "{@LogValues}", logValues);
                    }
                }
                await responseBody.CopyToAsync(originalResponseBody);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            return $"{bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{text}";
        }
    }

    public static class RequestResponseLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogger(this IApplicationBuilder builder
            , IOptions<AppSettings> options, LogToDB logToDB)
        {
            return builder.UseMiddleware<Logger>(options, logToDB);
        }
    }
}
