using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnyListen.Music.Proxy
{
    public class ProxyManager
    {
        private static ProxyManager _instance;
        public static ProxyManager Instance => _instance ?? (_instance = new ProxyManager());


        private ProxyManager()
        {
            InvalidHttpProxies = new List<string>();
        }

        public List<string> InvalidHttpProxies { get; set; }

        public async Task<HttpProxy> GetWebProxy()
        {
      