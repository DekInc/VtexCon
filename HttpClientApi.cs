using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon
{
    public class HttpClientApi {
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        int WsTimeout { get; set; }
        string BaseEndPoint { get; set; }
        string VTexMainApiAppKey { get; set; }
        string VTexMainApiAppPassword { get; set; }
        public string LastHttpError { get; set; }
        public HttpClientApi(short Typ, string _BaseEndPoint, string _VTexMainApiAppKey, string _VTexMainApiAppPassword) {
            if (Typ == 0)
                BaseEndPoint = ConfigurationManager.AppSettings["MainEndPoint"].Replace("arvitalqa", _BaseEndPoint);
            else
                BaseEndPoint = string.Format(ConfigurationManager.AppSettings["MainEndPoint2"], _BaseEndPoint);
            WsTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["WsTimeout"]);
            VTexMainApiAppKey = _VTexMainApiAppKey;
            VTexMainApiAppPassword = _VTexMainApiAppPassword;
        }
        private void SetNormalHeaders(ref HttpWebRequest WebRequest) {
            WebRequest.ServicePoint.Expect100Continue = false;
            WebRequest.Timeout = WsTimeout;
            WebRequest.Headers.Add("X-VTEX-API-AppKey", VTexMainApiAppKey);
            WebRequest.Headers.Add("X-VTEX-API-AppToken", VTexMainApiAppPassword);
            WebRequest.ContentType = "application/json; charset=utf-8";
            WebRequest.Accept = "application/json";
        }
        void SetSsl() {
            LastHttpError = string.Empty;
            try { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            } catch (NotSupportedException) {
                try { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                } catch (NotSupportedException) {
                    try { //try TLS 1.1
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768
                                                             | SecurityProtocolType.Tls;
                    } catch (NotSupportedException) { //TLS 1.0
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    }
                }
            }
        }
        public T GetDynamic<T>(string Method) {
            string EndPoint = BaseEndPoint + Method;
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "GET";                
                SetSsl();
                SetNormalHeaders(ref WebRequest);
                try {
                    HttpWebResponse resp = (HttpWebResponse)WebRequest.GetResponse();
                    Stream resStream = resp.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    string RespuestaJson = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(RespuestaJson);
                } catch (WebException ex) {
                    try {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream)) {
                            LastHttpError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                    LastHttpError = Ex.ToString();
                }
            }
            return default;
        }
        public List<T> GetDynamicList<T>(string Method) {
            string EndPoint = BaseEndPoint + Method;
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "GET";                
                SetSsl();
                SetNormalHeaders(ref WebRequest);
                try {
                    HttpWebResponse resp = (HttpWebResponse)WebRequest.GetResponse();
                    Stream resStream = resp.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    string RespuestaJson = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<T>>(RespuestaJson);
                } catch (WebException ex) {
                    try {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream)) {
                            LastHttpError = reader.ReadToEnd();
                            Debug.WriteLine(LastHttpError);
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                    LastHttpError = Ex.ToString();
                }
            }
            return default;
        }
        public T PutDynamic<T>(string Method, T V) {
            string EndPoint = BaseEndPoint + Method;
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "PUT";                
                SetSsl();
                SetNormalHeaders(ref WebRequest);
                try {
                    //string PostData = JsonConvert.SerializeObject(V, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    string PostData = JsonConvert.SerializeObject(V);
                    byte[] PostByteData = Encoding.UTF8.GetBytes(PostData);
                    WebRequest.ContentLength = PostByteData.Length;                    
                    Stream RequestStream = WebRequest.GetRequestStream();
                    RequestStream.Write(PostByteData, 0, PostByteData.Length);
                    RequestStream.Close();
                    HttpWebResponse resp = (HttpWebResponse)WebRequest.GetResponse();
                    Stream resStream = resp.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    string RespuestaJson = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(RespuestaJson);
                } catch (WebException ex) {
                    try {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream)) {
                            LastHttpError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                    LastHttpError = Ex.ToString();
                }
            }
            return default;
        }
        public R PutDynamic<T, R>(string Method, T V) {
            string EndPoint = BaseEndPoint + Method;
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "PUT";
                SetSsl();
                SetNormalHeaders(ref WebRequest);
                try {
                    //string PostData = JsonConvert.SerializeObject(V, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    string PostData = JsonConvert.SerializeObject(V);
                    byte[] PostByteData = Encoding.UTF8.GetBytes(PostData);
                    WebRequest.ContentLength = PostByteData.Length;
                    Stream RequestStream = WebRequest.GetRequestStream();
                    RequestStream.Write(PostByteData, 0, PostByteData.Length);
                    RequestStream.Close();
                    HttpWebResponse resp = (HttpWebResponse)WebRequest.GetResponse();
                    Stream resStream = resp.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    string RespuestaJson = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<R>(RespuestaJson);
                } catch (WebException ex) {
                    try {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream)) {
                            LastHttpError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                    LastHttpError = Ex.ToString();
                }
            }
            return default;
        }
        public T PostDynamic<T>(string Method, T V) {
            string EndPoint = BaseEndPoint + Method;
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "POST";                
                SetSsl();
                SetNormalHeaders(ref WebRequest);
                try {
                    string PostData = JsonConvert.SerializeObject(V);
                    byte[] PostByteData = Encoding.UTF8.GetBytes(PostData);
                    WebRequest.ContentLength = PostByteData.Length;
                    Stream RequestStream = WebRequest.GetRequestStream();
                    RequestStream.Write(PostByteData, 0, PostByteData.Length);
                    RequestStream.Close();
                    HttpWebResponse resp = (HttpWebResponse)WebRequest.GetResponse();
                    Stream resStream = resp.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    string RespuestaJson = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(RespuestaJson);
                } catch (WebException ex) {
                    try {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream)) {
                            LastHttpError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                    LastHttpError = Ex.ToString();
                }
            }
            return default;
        }
        public void Delete(string Method) {
            string EndPoint = BaseEndPoint + Method;
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "DELETE";
                SetSsl();
                SetNormalHeaders(ref WebRequest);
                try {
                    HttpWebResponse resp = (HttpWebResponse)WebRequest.GetResponse();                    
                } catch (WebException ex) {
                    try {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream)) {
                            LastHttpError = reader.ReadToEnd();
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                    LastHttpError = Ex.ToString();
                }
            }
        }
    }
}
