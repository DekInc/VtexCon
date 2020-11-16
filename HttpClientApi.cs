﻿using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon
{
    /// QA 
//    Vital2020QA
//vtexappkey-arvitalqa-FMOQTV
//AMOHYRRKEIUJSHVZAWOCOXZFDMSTTICRJIEQLNBNRLQSOXUZJFLZEIIDNIELZXRWFDZJCFOMVALCGGQFRSJNXCMBZBCCJPNUUUNLXKWERZEDXGWGSYTHNDZCQYKBCMCF


//QATR
//keymaster0311
//vtexappkey-arvitalqatr-ZLVYUS
//GKDQNNSYRRDDDILAUZOQPKJFVCGWCUKLSRGZBPIWBPVNKPIYSPFBSCCVAYUKHVVWHRLLCLYEQSOAPWUFUHENPJTPPKZDUYOGTYGSEXWGQAKZOCGDDPQZQIHXXVVSMVKS


//QALH
//keymaster0311_v2
//vtexappkey-arvitalqalh-EQXSPP
//FCFDKEOROQVGILRASPAZEVWAVAZTNVWBTIRCMBBXAEOBNMMDOCPYFGQJGEHJFVETUTNZTONRODZZGVNJOVELFXWNRIWHTSREAVGTZMKZOXKCGRCLRYVJQSNWXZILSGYV
    public class HttpClientApi {
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        void SetSsl() {
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
            string EndPoint = ConfigurationManager.AppSettings["MainEndPoint"] + Method;
            string VTexMainApiAppKey = ConfigurationManager.AppSettings["VTexMainApiAppKey"];
            string VTexMainApiAppPassword = ConfigurationManager.AppSettings["VTexMainApiAppPassword"];
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "GET";
                WebRequest.ContentType = "application/json";
                WebRequest.ServicePoint.Expect100Continue = false;
                WebRequest.Timeout = 60000;
                SetSsl();
                WebRequest.Headers.Add("X-VTEX-API-AppKey", VTexMainApiAppKey);
                WebRequest.Headers.Add("X-VTEX-API-AppToken", VTexMainApiAppPassword);
                WebRequest.ContentType = "application/json";
                WebRequest.Accept = "application/json";
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
                            string VerdaderoError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                }
            }
            return default;
        }
        public List<T> GetDynamicList<T>(string Method) {
            string EndPoint = ConfigurationManager.AppSettings["MainEndPoint"] + Method;
            string VTexMainApiAppKey = ConfigurationManager.AppSettings["VTexMainApiAppKey"];
            string VTexMainApiAppPassword = ConfigurationManager.AppSettings["VTexMainApiAppPassword"];
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "GET";
                WebRequest.ContentType = "application/json";
                WebRequest.ServicePoint.Expect100Continue = false;
                WebRequest.Timeout = 60000;
                SetSsl();
                WebRequest.Headers.Add("X-VTEX-API-AppKey", VTexMainApiAppKey);
                WebRequest.Headers.Add("X-VTEX-API-AppToken", VTexMainApiAppPassword);
                WebRequest.ContentType = "application/json";
                WebRequest.Accept = "application/json";
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
                            string VerdaderoError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                }
            }
            return default;
        }
        public T PutDynamic<T>(string Method, T V) {
            string EndPoint = ConfigurationManager.AppSettings["MainEndPoint"] + Method;
            string VTexMainApiAppKey = ConfigurationManager.AppSettings["VTexMainApiAppKey"];
            string VTexMainApiAppPassword = ConfigurationManager.AppSettings["VTexMainApiAppPassword"];
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "PUT";
                WebRequest.ContentType = "application/json";
                WebRequest.ServicePoint.Expect100Continue = false;
                WebRequest.Timeout = 60000;
                SetSsl();
                WebRequest.Headers.Add("X-VTEX-API-AppKey", VTexMainApiAppKey);
                WebRequest.Headers.Add("X-VTEX-API-AppToken", VTexMainApiAppPassword);
                WebRequest.ContentType = "application/json";
                WebRequest.Accept = "application/json";
                try {
                    string PostData = JsonConvert.SerializeObject(V);
                    byte[] PostByteData = Encoding.Default.GetBytes(PostData);
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
                            string VerdaderoError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                }
            }
            return default;
        }
        public T PostDynamic<T>(string Method, T V) {
            string EndPoint = ConfigurationManager.AppSettings["MainEndPoint"] + Method;
            string VTexMainApiAppKey = ConfigurationManager.AppSettings["VTexMainApiAppKey"];
            string VTexMainApiAppPassword = ConfigurationManager.AppSettings["VTexMainApiAppPassword"];
            HttpWebRequest WebRequest = System.Net.WebRequest.Create(EndPoint) as HttpWebRequest;
            if (WebRequest != null) {
                WebRequest.Method = "POST";
                WebRequest.ContentType = "application/json";
                WebRequest.ServicePoint.Expect100Continue = false;
                WebRequest.Timeout = 60000;
                SetSsl();
                WebRequest.Headers.Add("X-VTEX-API-AppKey", VTexMainApiAppKey);
                WebRequest.Headers.Add("X-VTEX-API-AppToken", VTexMainApiAppPassword);
                WebRequest.ContentType = "application/json";
                WebRequest.Accept = "application/json";
                try {
                    string PostData = JsonConvert.SerializeObject(V);
                    byte[] PostByteData = Encoding.Default.GetBytes(PostData);
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
                            string VerdaderoError = reader.ReadToEnd();
                            return default;
                        }
                    } catch { }
                } catch (Exception Ex) {
                    Log.Error(Ex.ToString());
                }
            }
            return default;
        }
    }
}
