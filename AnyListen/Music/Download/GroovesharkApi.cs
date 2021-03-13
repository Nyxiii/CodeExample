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
            public string AcceptCharset { get; s