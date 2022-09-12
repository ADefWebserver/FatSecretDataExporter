using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;


namespace fatsecret.platform{
	public class FatSecretAPI{
		public const string URL_BASE = "https://platform.fatsecret.com/rest/server.api?";

		#region Private Data

		private string _consumerKey;
		private string _consumerSecret;

		#endregion

		#region Constructors

		public FatSecretAPI(){
		}

		/// <summary>
		/// Initialize with Consumer Key and Consumer Secret
		/// </summary>
		/// <param name="consumerKey">Your API Developer Key</param>
		/// <param name="consumerSecret">Your API Developer Secret</param>
		public FatSecretAPI(string consumerKey, string consumerSecret){
			ConsumerKey = consumerKey;
			ConsumerSecret = consumerSecret;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get or Set your Developer API Key
		/// </summary>
		public string ConsumerKey{
			get{
				return _consumerKey;
			}
			set{
				_consumerKey = value;
			}
		}

		/// <summary>
		/// Get or Set your Developer API Secret
		/// </summary>
		public string ConsumerSecret{
			get{
				return _consumerSecret;
			}
			set{
				_consumerSecret = value;
			}
		}

        #endregion

        #region Public Methods

        #region public void GetFood(string UserToken, string UserTokenSecret, int paramDate, out string response)
        public void GetFood(string UserToken, string UserTokenSecret, int paramDate, out string response)
        {
            string urlBase = URL_BASE + $"method=food_entries.get_month&format=json&date={paramDate}";

            OAuthBase oAuth = new OAuthBase();

            Uri url = new Uri(urlBase);
            string normalizedUrl, normalizedRequestParameters;

            string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, UserToken, UserTokenSecret, out normalizedUrl, out normalizedRequestParameters);

            string doc = GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature));

            response = doc;
        }
        #endregion

        #region public void GetExercise(string UserToken, string UserTokenSecret, int paramDate, out string response)
        public void GetExercise(string UserToken, string UserTokenSecret, int paramDate, out string response)
        {
            string urlBase = URL_BASE + $"method=exercise_entries.get_month&format=json&date={paramDate}";

            OAuthBase oAuth = new OAuthBase();

            Uri url = new Uri(urlBase);
            string normalizedUrl, normalizedRequestParameters;

            string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, UserToken, UserTokenSecret, out normalizedUrl, out normalizedRequestParameters);

            string doc = GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature));

            response = doc;
        }
        #endregion

        #region public void GetWeight(string UserToken, string UserTokenSecret, int paramDate, out string response)
        public void GetWeight(string UserToken, string UserTokenSecret, int paramDate, out string response)
        {
            string urlBase = URL_BASE + $"method=weights.get_month&format=json&date={paramDate}";

            OAuthBase oAuth = new OAuthBase();

            Uri url = new Uri(urlBase);
            string normalizedUrl, normalizedRequestParameters;

            string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, UserToken, UserTokenSecret, out normalizedUrl, out normalizedRequestParameters);

            string doc = GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature));

            response = doc;
        }
        #endregion

        #region public void GetProfile(string UserToken, string UserTokenSecret, out string response)
        public void GetProfile(string UserToken, string UserTokenSecret, out string response)
        {
            string urlBase = URL_BASE + "method=profile.get&format=json";

            OAuthBase oAuth = new OAuthBase();

            Uri url = new Uri(urlBase);
            string normalizedUrl, normalizedRequestParameters;

            string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, UserToken, UserTokenSecret, out normalizedUrl, out normalizedRequestParameters);

            string doc = GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature));

            response = doc;
        } 
        #endregion

        #region public void GetAuthToken(out string token, out string oauth_token_secret)
        public void GetAuthToken(out string token, out string oauth_token_secret)
        {
            string urlBase = "https://www.fatsecret.com/oauth/request_token?";
            urlBase = urlBase + "oauth_callback=oob";

            OAuthBase oAuth = new OAuthBase();

            Uri url = new Uri(urlBase);
            string normalizedUrl, normalizedRequestParameters;

            string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, null, null, out normalizedUrl, out normalizedRequestParameters);

            var result = GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature));

            string[] p = result.Split('&');

            token = p[1].Replace("oauth_token=", "");
            oauth_token_secret = p[2].Replace("oauth_token_secret=", "");
        } 
        #endregion

        #region public void GetAccessToken(string OauthTokenSecret, string oauth_request_token, string oauth_verifier, out string oauth_token, out string oauth_token_secret)
        public void GetAccessToken(string OauthTokenSecret, string oauth_request_token, string oauth_verifier, out string oauth_token, out string oauth_token_secret)
        {
            string urlBase = "https://www.fatsecret.com/oauth/access_token?";
            urlBase = urlBase + "oauth_token=" + oauth_request_token;
            urlBase = urlBase + "&oauth_verifier=" + oauth_verifier;

            OAuthBase oAuth = new OAuthBase();

            Uri url = new Uri(urlBase);
            string normalizedUrl, normalizedRequestParameters;

            string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, null, OauthTokenSecret, out normalizedUrl, out normalizedRequestParameters);

            var result = GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature));

            string[] p = result.Split('&');

            oauth_token = p[0].Replace("oauth_token=", "");
            oauth_token_secret = p[1].Replace("oauth_token_secret=", "");
        }
        #endregion

        /// <summary>
        /// Create a new profile
        /// </summary>
        /// <param name="token">The token for the newly created profile is returned here</param>
        /// <param name="secret">The secret for the newly created profile is returned here</param>
        public void ProfileCreate(out string token, out string secret){
			ProfileCreate(string.Empty, out token, out secret);
		}

		/// <summary>
		/// Create a new profile with a user specified ID
		/// </summary>
		/// <param name="userID">Your ID for the newly created profile</param>
		/// <param name="token">The token for the newly created profile is returned here</param>
		/// <param name="secret">The secret for the newly created profile is returned here</param>
		public void ProfileCreate(string userID, out string token, out string secret){
			string urlBase = URL_BASE + "method=profile.create";

			if(userID != null || userID != string.Empty)
				urlBase += "&user_id=" + userID;

			OAuthBase oAuth = new OAuthBase();

			Uri url = new Uri(urlBase);
			string normalizedUrl, normalizedRequestParameters;

			string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, null, null, out normalizedUrl, out normalizedRequestParameters);

			XmlDocument doc = LoadXMLDocument(GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature)));

			ErrorCheck(doc);

			token = doc["profile"]["auth_token"].InnerText;
			secret = doc["profile"]["auth_secret"].InnerText;
		}

		/// <summary>
		/// Get the auth details of a profile
		/// </summary>
		/// <param name="userID">Your ID for the profile</param>
		/// <param name="token">The token for the profile is returned here</param>
		/// <param name="secret">The secret for the profile is returned here</param>
		public void ProfileGetAuth(string userID, out string token, out string secret){
			string urlBase = URL_BASE + "method=profile.get_auth&user_id=" + userID;

			OAuthBase oAuth = new OAuthBase();

			Uri url = new Uri(urlBase);
			string normalizedUrl, normalizedRequestParameters;

			string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, null, null, out normalizedUrl, out normalizedRequestParameters);

			XmlDocument doc = LoadXMLDocument(GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature)));

			ErrorCheck(doc);

			token = doc["profile"]["auth_token"].InnerText;
			secret = doc["profile"]["auth_secret"].InnerText;
		}

		/// <summary>
		/// Create a new session for JavaScript API users
		/// </summary>
		/// <param name="token">The token of the profile</param>
		/// <param name="secret">The secret of the profile</param>
		/// <param name="expires">The number of minutes before a session is expired after it is first started. Set this to 0 to never expire the session. (Set to any value less than 0 for default)</param>
		/// <param name="consumeWithin">The number of minutes to start using a session after it is first issued. (Set to any value less than 0 for default)</param>
		/// <param name="permittedReferrerRegex">A domain restriction for the session. (Set to null if you do not need this)</param>
		/// <param name="cookie">The desired session_key format.</param>
		/// <param name="sessionKey">The session key for the newly created session is returned here</param>
		public void ProfileRequestScriptSessionKey(string token, string secret, int expires, int consumeWithin, string permittedReferrerRegex, bool cookie, out string sessionKey){
			string urlBase = URL_BASE + "method=profile.request_script_session_key";

			if(expires > -1)
				urlBase += "&expires=" + expires;

			if(consumeWithin > -1)
				urlBase += "&consume_within=" + consumeWithin;

			if(permittedReferrerRegex != null)
				urlBase += "&permitted_referrer_regex=" + permittedReferrerRegex;

			if(cookie)
				urlBase += "&cookie=true";

			OAuthBase oAuth = new OAuthBase();

			Uri url = new Uri(urlBase);
			string normalizedUrl, normalizedRequestParameters;

			string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, token, secret, out normalizedUrl, out normalizedRequestParameters);

			XmlDocument doc = LoadXMLDocument(GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature)));

			ErrorCheck(doc);

			sessionKey = doc["profile"]["session_key"].InnerText;   
		}

		/// <summary>
		/// Create a new session for JavaScript API users
		/// </summary>
		/// <param name="userID">Your ID for the profile</param>
		/// <param name="expires">The number of minutes before a session is expired after it is first started. Set this to 0 to never expire the session. (Set to any value less than 0 for default)</param>
		/// <param name="consumeWithin">The number of minutes to start using a session after it is first issued. (Set to any value less than 0 for default)</param>
		/// <param name="permittedReferrerRegex">A domain restriction for the session. (Set to null if you do not need this)</param>
		/// <param name="cookie">The desired session_key format.</param>
		/// <param name="sessionKey">The session key for the newly created session is returned here</param>
		public void ProfileRequestScriptSessionKey(string userID, int expires, int consumeWithin, string permittedReferrerRegex, bool cookie, out string sessionKey){
			string urlBase = URL_BASE + "method=profile.request_script_session_key&user_id=" + userID;

			if(expires > -1)
				urlBase += "&expires=" + expires;

			if(consumeWithin > -1)
				urlBase += "&consume_within=" + consumeWithin;

			if(permittedReferrerRegex != null)
				urlBase += "&permitted_referrer_regex=" + permittedReferrerRegex;

			if(cookie)
				urlBase += "&cookie=true";

			OAuthBase oAuth = new OAuthBase();

			Uri url = new Uri(urlBase);
			string normalizedUrl, normalizedRequestParameters;

			string signature = oAuth.GenerateSignature(url, ConsumerKey, ConsumerSecret, null, null, out normalizedUrl, out normalizedRequestParameters);

			XmlDocument doc = LoadXMLDocument(GetQueryResponse(normalizedUrl, normalizedRequestParameters + "&" + OAuthBase.OAUTH_SIGNATURE + "=" + HttpUtility.UrlEncode(signature)));

			ErrorCheck(doc);

			sessionKey = doc["profile"]["session_key"].InnerText;
		}

		#endregion

		#region Private Methods

		internal static XmlDocument LoadXMLDocument(string rawXML) {
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(rawXML);
			return xmlDocument;
		}

		internal static string GetQueryResponse(string requestUrl, string postString) {
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);

			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";

			byte[] parameterString = Encoding.ASCII.GetBytes(postString);
			webRequest.ContentLength = parameterString.Length;

			using (Stream buffer = webRequest.GetRequestStream()) {
				buffer.Write(parameterString, 0, parameterString.Length);
				buffer.Close();
			}

			WebResponse webResponse = webRequest.GetResponse();

			string responseData;
			using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
				responseData = streamReader.ReadToEnd();
			}
			return responseData;
		}

		internal static void ErrorCheck(XmlDocument doc) {
			XmlNodeList errors = doc.GetElementsByTagName("error");

			if (errors.Count > 0){
				throw new FatSecretException(Int16.Parse(errors[0]["code"].InnerText), errors[0]["message"].InnerText);
			}
		}

		#endregion

		#region OAuth

		protected class OAuthBase {

			#region OAuth Parameters

			public const string OAUTH_VERSION_NUMBER = "1.0";
			public const string OAUTH_PARAMETER_PREFIX = "oauth_";
			public const string XOAUTH_PARAMETER_PREFIX = "xoauth_";
			public const string OPEN_SOCIAL_PARAMETER_PREFIX = "opensocial_";

			public const string OAUTH_CONSUMER_KEY = "oauth_consumer_key";
			public const string OAUTH_CALLBACK = "oauth_callback";
			public const string OAUTH_VERSION = "oauth_version";
			public const string OAUTH_SIGNATURE_METHOD = "oauth_signature_method";
			public const string OAUTH_SIGNATURE = "oauth_signature";
			public const string OAUTH_TIMESTAMP = "oauth_timestamp";
			public const string OAUTH_NONCE = "oauth_nonce";
			public const string OAUTH_TOKEN = "oauth_token";
			public const string OAUTH_TOKEN_SECRET = "oauth_token_secret";

			#endregion

			protected string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

			/// <summary>
			/// Generates a signature using the HMAC-SHA1 algorithm
			/// </summary>		
			/// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
			/// <param name="consumerKey">The consumer key</param>
			/// <param name="consumerSecret">The consumer seceret</param>
			/// <param name="token">The token, if available. If not available pass null or an empty string</param>
			/// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
			/// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
			/// <returns>A base64 string of the hash value</returns>
			public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token, string tokenSecret, out string normalizedUrl, out string normalizedRequestParameters) {
				normalizedUrl = null;
				normalizedRequestParameters = null;

				string signatureBase = GenerateSignatureBase(url, consumerKey, token, "POST", GenerateTimeStamp(), GenerateNonce(), "HMAC-SHA1", out normalizedUrl, out normalizedRequestParameters);
				HMACSHA1 hmacsha1 = new HMACSHA1();
				hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", UrlEncode(consumerSecret), IsNullOrEmpty(tokenSecret) ? "" : UrlEncode(tokenSecret)));
				return GenerateSignatureUsingHash(signatureBase, hmacsha1);
			}

			protected class QueryParameter {
				private string name = null;
				private string value = null;

				public QueryParameter(string name, string value) {
					this.name = name;
					this.value = value;
				}

				public string Name {
					get { return name; }
				}

				public string Value {
					get { return value; }
				}
			}

			protected class QueryParameterComparer : IComparer{
				public int Compare(object a, object b) {
					QueryParameter x = (QueryParameter)a;
					QueryParameter y = (QueryParameter)b;
					if (x.Name == y.Name){
						return string.Compare(x.Value, y.Value);
					} 
					else{
						return string.Compare(x.Name, y.Name);
					}
				}
			}

			private static bool IsNullOrEmpty(string str){
				return (str == null || str.Length == 0);
			}

			private string ComputeHash(HashAlgorithm hashAlgorithm, string data) {
				byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(data);
				byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

				return Convert.ToBase64String(hashBytes);
			}

			public IList GetQueryParameters(string parameters, IList result) {
				if (parameters.StartsWith("?")) {
					parameters = parameters.Remove(0, 1);
				}

                if(parameters == "oauth_callback=oob")
                {
                    result.Add(new QueryParameter("oauth_callback", "oob"));
                }

                if (parameters.Contains("oauth_token"))
                {
                    string[] p = parameters.Split('&');                    
                    result.Add(new QueryParameter("oauth_token", p[0].Replace("oauth_token=", "")));
                }

                if (parameters.Contains("oauth_verifier"))
                {
                    string[] p = parameters.Split('&');
                    result.Add(new QueryParameter("oauth_verifier", p[1].Replace("oauth_verifier=", "")));
                }

                if (!IsNullOrEmpty(parameters)) {
					string[] p = parameters.Split('&');
					foreach (string s in p) {
						if (!IsNullOrEmpty(s) && !s.StartsWith(OAUTH_PARAMETER_PREFIX) && !s.StartsWith(XOAUTH_PARAMETER_PREFIX) && !s.StartsWith(OPEN_SOCIAL_PARAMETER_PREFIX)) {
							if (s.IndexOf('=') > -1) {
								string[] temp = s.Split('=');
								result.Add(new QueryParameter(temp[0], UrlEncode(temp[1])));
							} 
							else {
								result.Add(new QueryParameter(s, string.Empty));
							}
						}
					}
				}

				return result;
			}

			protected string UrlEncode(string value){
				StringBuilder result = new StringBuilder();

				foreach(char symbol in value){
					if(unreservedChars.IndexOf(symbol) != -1){
						result.Append(symbol);
					} 
					else {
						result.Append('%' + String.Format("{0:X2}", (int)symbol));
					}
				}

				return result.ToString();
			}

			protected string NormalizeRequestParameters(IList parameters) {
				StringBuilder sb = new StringBuilder();
				QueryParameter p = null;
				for (int i = 0; i < parameters.Count; i++) {
					p = (QueryParameter)parameters[i];
					sb.AppendFormat("{0}={1}", p.Name, p.Value);

					if (i < parameters.Count - 1) {
						sb.Append("&");
					}
				}

				return sb.ToString();
			}

			private string GenerateSignatureBase(Uri url, string consumerKey, string token, string httpMethod, string timeStamp, string nonce, string signatureType, out string normalizedUrl, out string normalizedRequestParameters){
				normalizedUrl = null;
				normalizedRequestParameters = null;

				IList parameters = new ArrayList();

				GetQueryParameters(url.Query, parameters);

				parameters.Add(new QueryParameter(OAUTH_VERSION, OAUTH_VERSION_NUMBER));
				parameters.Add(new QueryParameter(OAUTH_NONCE, nonce));
				parameters.Add(new QueryParameter(OAUTH_TIMESTAMP, timeStamp));
				parameters.Add(new QueryParameter(OAUTH_SIGNATURE_METHOD, signatureType));
				parameters.Add(new QueryParameter(OAUTH_CONSUMER_KEY, consumerKey));

				if (!IsNullOrEmpty(token)) {
					parameters.Add(new QueryParameter(OAUTH_TOKEN, token));
				}

				((ArrayList)parameters).Sort(new QueryParameterComparer());

				normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
				if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443))) {
					normalizedUrl += ":" + url.Port;
				}

				normalizedUrl += url.AbsolutePath;
				normalizedRequestParameters = NormalizeRequestParameters(parameters);

				StringBuilder signatureBase = new StringBuilder();
				signatureBase.AppendFormat("{0}&", httpMethod);
				signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl));
				signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters));

				return signatureBase.ToString();
			}

			private string GenerateSignatureUsingHash(string signatureBase, HashAlgorithm hash) {
				return ComputeHash(hash, signatureBase);
			}

			private string GenerateTimeStamp() {
				TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
				return Convert.ToInt64(ts.TotalSeconds).ToString();
			}

			private string GenerateNonce() {
				return Guid.NewGuid().ToString().Replace("-", "");
			}
		}

		#endregion
	}

	public class FatSecretException: Exception {
		private int errorCode;
		private string errorMessage;

		public FatSecretException(int errorCode, string errorMsg){
			this.errorCode = errorCode;
			this.errorMessage = errorMsg;
		}

		public int ErrorCode{
			get{
				return errorCode;
			}
		}

		public string ErrorMessage{
			get{
				return errorMessage;
			}
			set{
				errorMessage = value;
			}
		}
	}
}
