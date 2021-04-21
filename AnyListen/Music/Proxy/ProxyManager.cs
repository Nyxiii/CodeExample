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
            var proxies = await GetProxies();
            var sortedList =
              proxies.OrderByDescending(x => x.Country == "United Kingdom").ThenBy(x => x.Speed).ThenBy(x => x.ResponseTime);

            var proxy = sortedList.FirstOrDefault(x => !InvalidHttpProxies.Contains(x.DecodeIp() + ":" + x.Port));
            if (proxy == null)
                throw new Exception("No proxies found");

            return new HttpProxy(proxy.DecodeIp(), proxy.Port);
        }

        public void AddInvalid(HttpProxy proxy)
        {
            InvalidHttpProxies.Add(proxy.ToString());
        }

        private async Task<List<ProxyEntry>> GetProxies()
        {
            var result = new List<ProxyEntry>();

            using (var wc = new WebClient { P