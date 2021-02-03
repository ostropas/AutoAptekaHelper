using CsQuery;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestProj.BaseClasses;
using TestProj.DataModel;
using TestProj.DataModel.AptekaHelper;

namespace TestProj.SiteParsers
{
    public class VolgofarmSiteParser : HttpSiteParser
    {
        private string _siteUrl => "http://volgofarm.ru";
        protected override async Task<List<OutputData>> AddProduct(string city, ProductData data)
        {
            var baseAddress = new Uri(_siteUrl);
            var cookieContainer = new CookieContainer();
            string res = "";
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            {
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    cookieContainer.Add(baseAddress, new System.Net.Cookie($"basket[{data.Id}]", data.Count));
                    var result = await client.GetAsync("/step2.html");
                    result.EnsureSuccessStatusCode();
                    res = await result.Content.ReadAsStringAsync();
                }
            }
            return Parse(res, data.ProductName);
        }

        private List<OutputData> Parse(string sitePage, string productTitle)
        {
            var content = new List<OutputData>();

            CQ cq = CQ.Create(sitePage);

            var titles = cq[".apt_title"].Select(x => x.Cq().Text()).ToList();
            var addresses = cq[".apt_adress"].Select(x => x.Cq().Text()).ToList();
            var counts = cq[".vnalichii_vsego"].Select(x => x.Cq().Text()).ToList();

            for (int i = 0; i < titles.Count; i++)
            {
                var address = addresses[i];
                if (address.Contains("\r\n"))
                    address = address.Substring(0, address.IndexOf("\r\n"));
                var count = "";
                if (i < counts.Count)
                    count = counts[i];

                if (string.IsNullOrEmpty(count))
                    count = "0";

                content.Add(new OutputData(productTitle, titles[i], address, count));
            }

            return content;
        }
    }
}
