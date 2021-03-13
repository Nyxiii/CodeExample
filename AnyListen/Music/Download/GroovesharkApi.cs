using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnyListen.Music.Download
{
    class GroovesharkApi
    {
        public class HttpHeaders
        {
            [JsonProperty("Accept-Charset")]
            public string AcceptCharset { get; set; }

            [JsonProperty("Content-Length")]
            public int ContentLength { get; set; }

            [JsonProperty("Accept-Language")]
            public string AcceptLanguage { get; set; }

            [JsonProperty("Accept-Encoding")]
            public string AcceptEncoding { get; set; }

            public string Cookie { get; set; }

            [JsonProperty("Content-Type")]
            public string ContentType { get; set; }

            public string Accept { get; set; }

            [JsonProperty("User-Agent")]
            public string UserAgent { get; set; }
        }

        public class GroovesharkResult
        {
            [JsonProperty("display_id")]
            public string Displ