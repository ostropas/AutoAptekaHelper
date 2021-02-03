using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.DataModel.AptekaHelper;

namespace TestProj.DataModel
{
    public enum Site
    {
        Volgofarm,
        April,
        Asna
    }

    public class SiteConfig
    {
        public Site SiteParserType { get; set; }
        public List<string> Cities { get; set; }
        public List<ProductData> Products
        {
            get
            {
                if (_products != null)
                    return _products;

                _products = JsonProducts.Select(x => new ProductData(x)).ToList();
                return _products;
            }
        }
        public List<string> JsonProducts { get; set; }
        private List<ProductData> _products;
    }
}
