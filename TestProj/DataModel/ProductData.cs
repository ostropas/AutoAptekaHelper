using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.DataModel
{
    namespace AptekaHelper
    {
        public class ProductData
        {
            public string ProductName { get; set; }
            public string Id { get; set; }
            public string Count { get; set; }

            public ProductData(string productName, string id, string count)
            {
                ProductName = productName;
                Id = id;
                Count = count;
            }

            public ProductData(string def)
            {
                var vals = def.Split(';');
                ProductName = vals[0];
                Id = vals[1];
                Count = vals[2];
            }

            public ProductData(ProductData productData)
            {
                ProductName = productData.ProductName;
                Id = productData.Id;
                Count = productData.Count;
            }
        }
    }
}
