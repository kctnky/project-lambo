using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLambo.Okex
{
    /// <summary>
    /// Do http requests to Okex.
    /// </summary>
    internal class OkexWebRequest
    {
        #region Fields

        private const string domain = "https://www.okex.com";
        private string apiBase = string.Empty;
        private string apiKey = string.Empty;
        private string apiSecret = string.Empty;
        private string apiPassphrase = string.Empty;

        #endregion

        #region Constructers

        /// <summary>
        /// Default constructer.
        /// </summary>
        /// <param name="apiBase">api base uri for resource access</param>
        public OkexWebRequest(string apiBase)
        {
            this.apiBase = apiBase;
        }

        /// <summary>
        /// Constructer.
        /// </summary>
        /// <param name="apiBase">api base uri for resource access</param>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        /// <param name="apiPassphrase">passphrase</param>
        public OkexWebRequest(string apiBase, string apiKey, string apiSecret, string apiPassphrase)
        {
            this.apiBase = apiBase;
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.apiPassphrase = apiPassphrase;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set API access keys for authenticated requests.
        /// </summary>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        /// <param name="apiPassphrase">passphrase</param>
        public virtual void SetKeys(string apiKey, string apiSecret, string apiPassphrase)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.apiPassphrase = apiPassphrase;
        }

        /// <summary>
        /// Prepare a request and send it to exchange.
        /// </summary>
        /// <param name="method">request method, i.e. "GET"</param>
        /// <param name="endpoint">api endpoint</param>
        /// <param name="param">request parameters</param>
        /// <param name="auth">true if authentication required</param>
        /// <returns>response string</returns>
        public async virtual Task<string> Query(string method, string endpoint, Dictionary<string, object> param = null, bool auth = false)
        {
            string paramData = (method == "GET") ? BuildQueryData(param) : "";
            string postData = (method != "GET") ? BuildJSON(param) : "";
            string url = apiBase + endpoint + (paramData != "" ? "?" + paramData : "");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(domain + url);
            webRequest.Method = method;
            webRequest.ContentType = "application/json";

            if (auth)
            {
                var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                var sign = HmacSHA256(timestamp + method + url + postData, apiSecret);

                webRequest.Headers.Add("OK-ACCESS-KEY", apiKey);
                webRequest.Headers.Add("OK-ACCESS-SIGN", sign);
                webRequest.Headers.Add("OK-ACCESS-TIMESTAMP", timestamp);
                webRequest.Headers.Add("OK-ACCESS-PASSPHRASE", apiPassphrase);
            }

            try
            {
                if (postData != "")
                {
                    var data = Encoding.UTF8.GetBytes(postData);
                    using (var stream = webRequest.GetRequestStream())
                        stream.Write(data, 0, data.Length);
                }

                using (WebResponse webResponse = await webRequest.GetResponseAsync().ConfigureAwait(false))
                using (Stream str = webResponse.GetResponseStream())
                using (StreamReader sr = new StreamReader(str))
                    return sr.ReadToEnd();
            }
            catch (WebException wex)
            {
                // If http status code for the request is not 200 (OK) then read the response and return it without throwing an exception

                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    if (response == null)
                        throw;

                    using (Stream str = response.GetResponseStream())
                    using (StreamReader sr = new StreamReader(str))
                        return sr.ReadToEnd();
                }
            }
        }

        #endregion

        #region Private Methods

        /* Used for sending requests to api endpoints */
        protected string BuildQueryData(Dictionary<string, object> param)
        {
            if (param == null)
                return "";

            StringBuilder b = new StringBuilder();
            foreach (var item in param)
                b.Append(string.Format("&{0}={1}", item.Key, WebUtility.UrlEncode(item.Value.ToString())));

            try { return b.ToString().Substring(1); }
            catch (Exception) { return ""; }
        }

        /* Used for sending requests to api endpoints */
        protected string BuildJSON(Dictionary<string, object> param)
        {
            if (param == null)
                return "";

            var entries = new List<string>();
            foreach (var item in param)
            {
                if (item.Value is string)
                    entries.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));
                else
                    entries.Add(string.Format("\"{0}\":{1}", item.Key, item.Value));
            }

            return "{" + string.Join(",", entries) + "}";
        }

        /* Used for sending requests to api endpoints */
        protected string HmacSHA256(string infoStr, string secret)
        {
            byte[] sha256Data = Encoding.UTF8.GetBytes(infoStr);
            byte[] secretData = Encoding.UTF8.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(secretData))
            {
                byte[] buffer = hmacsha256.ComputeHash(sha256Data);
                return Convert.ToBase64String(buffer);
            }
        }

        #endregion
    }
}
