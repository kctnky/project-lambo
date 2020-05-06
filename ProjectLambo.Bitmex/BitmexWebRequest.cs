using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLambo.Bitmex
{
    /// <summary>
    /// Do http requests to Bitmex.
    /// </summary>
    internal class BitmexWebRequest
    {
        #region Fields

        private const string domain = "https://www.bitmex.com";
        private string apiKey = string.Empty;
        private string apiSecret = string.Empty;

        #endregion

        #region Constructers

        /// <summary>
        /// Default constructer.
        /// </summary>
        public BitmexWebRequest()
        { }

        /// <summary>
        /// Constructer.
        /// </summary>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        public BitmexWebRequest(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set API access keys for authenticated requests.
        /// </summary>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        public virtual void SetKeys(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        /// <summary>
        /// Prepare a request and send it to Bitmex.
        /// </summary>
        /// <param name="method">request method, i.e. "GET"</param>
        /// <param name="endpoint">api endpoint</param>
        /// <param name="param">request parameters</param>
        /// <param name="auth">true if authentication required</param>
        /// <returns>response string</returns>
        public async virtual Task<string> Query(string method, string endpoint, Dictionary<string, object> param = null, bool auth = false)
        {
            string paramData = BuildQueryData(param);
            string postData = (method != "GET") ? paramData : "";
            string url = "/api/v1" + endpoint + (paramData != "" ? "?" + paramData : "");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(domain + url);
            webRequest.Method = method;
            webRequest.ContentType = "application/x-www-form-urlencoded";

            if (auth)
            {
                string expires = GetExpires().ToString();
                string message = method + url + expires + postData;
                byte[] signatureBytes = Hmacsha256(Encoding.UTF8.GetBytes(apiSecret), Encoding.UTF8.GetBytes(message));
                string signatureString = ByteArrayToString(signatureBytes);

                webRequest.Headers.Add("api-expires", expires);
                webRequest.Headers.Add("api-key", apiKey);
                webRequest.Headers.Add("api-signature", signatureString);
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
        private string BuildQueryData(Dictionary<string, object> param)
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
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /* Used for sending requests to api endpoints */
        private long GetExpires()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600;
        }

        /* Used for sending requests to api endpoints */
        private byte[] Hmacsha256(byte[] keyByte, byte[] messageBytes)
        {
            using (var hash = new HMACSHA256(keyByte))
            {
                return hash.ComputeHash(messageBytes);
            }
        }

        #endregion
    }
}
