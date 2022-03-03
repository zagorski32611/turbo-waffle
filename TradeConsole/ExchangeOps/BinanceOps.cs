using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeConsole.ExchangeData;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace TradeConsole.ExchangeOps
{
    public class BinanceOps
    {
        HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.binance.us/api/v3/"),
            Timeout = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 5, milliseconds: 0)
        };

        BinanceData _binanceData = new();
        BinanceObjects _binanceObjects = new();
        long _recvWindow = 10000;
        

        public async Task<string> CallBinance(string targetUrl, HttpMethod method, string? queries = null)
        {
            long currentTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (queries is not null)
                queries = $"recvWindow=15000&timestamp={currentTimeStamp}" + $"&{queries}";
            else
                queries = $"recvWindow=15000&timestamp={currentTimeStamp}";

            HttpRequestMessage requestMessage = GenerateHeaders();
            requestMessage.Method = method;

            var signature = GenerateSignature(queries);
            queries = queries + $"&signature={signature}";

            requestMessage.RequestUri = new Uri(_client.BaseAddress + $"{targetUrl}?" + queries);

            var response = await _client.SendAsync(requestMessage);
            string? jsonResp = response.Content.ReadAsStringAsync().Result;
            return jsonResp is null ? "" : jsonResp;
        }

        public async Task<string> CallBinanceNoAuth(string targetUrl, string? queries = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(_client.BaseAddress + $"{targetUrl}" + $"?{queries}");
            var response = await _client.SendAsync(requestMessage);

            string? jsonReponse = response.Content.ReadAsStringAsync().Result;

            return jsonReponse is null ? "" : jsonReponse;
        }

        public HttpRequestMessage GenerateHeaders()
        {
            HttpRequestMessage message = new();
            using (var db = new TradingContext())
            {
                var key = from p in db.binanceDatas
                          select p.BinanceAPIKey;

                message.Headers.TryAddWithoutValidation("X-MBX-APIKEY", key.First());
                return message;
            }
        }

        public string GenerateSignature(string querystring)
        {
            string secret;
            try
            {
                secret = Environment.GetEnvironmentVariable("BinanceAPISecret", EnvironmentVariableTarget.User);
                byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
                byte[] queryBytes = Encoding.UTF8.GetBytes(querystring);

                HMACSHA256 hmacsha256 = new HMACSHA256(secretBytes);

                byte[] bytes = hmacsha256.ComputeHash(queryBytes);

                return BitConverter.ToString(bytes).Replace("-", String.Empty).ToLower();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return String.Empty;
            }
        }


        public long GenerateTimestamp()
        {
            DateTimeOffset.Now.ToUnixTimeMilliseconds();
            DateTimeOffset dto = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            long unixTimeStamp = dto.ToUnixTimeMilliseconds();
            return unixTimeStamp;
        }

    }
}


/*
         public async Task<string> DustLog()
        {
            string target = "/wapi/v3/userAssetDribbletLog.html";

            var queryparams = $"recvWindow={_recvWindow}&timestamp={_timeStamp}";
            HttpRequestMessage requestMessage = GenerateHeaders();
            requestMessage.Method = HttpMethod.Get;

            var signature = GenerateSignature(queryparams);
            queryparams = queryparams + $"&signature={signature}";

            requestMessage.RequestUri = new Uri("https://api.binance.us" + target + "/" + queryparams);
            var response = await _client.SendAsync(requestMessage);
            var jsonResp = response.Content.ReadAsStringAsync().Result;
            Console.Write(response.StatusCode);
            return jsonResp;
        }

        public async Task<string> ConvertDust(string dust)
        {
            string target = "https://api.binance.us/sapi/v1/asset/dust?";

            long recvWindow = 10000;
            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var queryparams = $"&timestamp={time}&" + dust;
            HttpRequestMessage requestMessage = GenerateHeaders();
            requestMessage.Method = HttpMethod.Post;

            var signature = GenerateSignature(queryparams);
            queryparams = queryparams + $"signature={signature}";

            requestMessage.RequestUri = new Uri(target + queryparams);
            var response = await _client.SendAsync(requestMessage);
            var jsonResp = response.Content.ReadAsStringAsync().Result;
            Console.Write(response.StatusCode);
            return jsonResp;
        }

  
 */