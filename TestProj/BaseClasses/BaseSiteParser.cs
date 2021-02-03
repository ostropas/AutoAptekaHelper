using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.DataModel;
using TestProj.DataModel.AptekaHelper;

namespace TestProj.BaseClasses
{ 
    public abstract class BaseSiteParser
    {
        public async Task<List<OutputFileFormat>> ParseSite(SiteConfig siteConfig)
        {
            var res = new List<OutputFileFormat>();
            foreach (var city in siteConfig.Cities)
            {
                res.Add(await ParseSite(city, siteConfig.Products));
            }
            return res;
        }

        public event Action<float> ProgressUpdated;
        protected SiteConfig _siteConfig;
        protected void UpdateProgress(float progress) => ProgressUpdated?.Invoke(progress);
        protected abstract Task<OutputFileFormat> ParseSite(string city, List<ProductData> products);
    }
}
