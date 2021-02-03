using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.DataModel;
using TestProj.DataModel.AptekaHelper;

namespace TestProj.BaseClasses
{
    abstract public class HttpSiteParser : BaseSiteParser
    {
        protected abstract Task<List<OutputData>> AddProduct(string city, ProductData data);

        protected override async Task<OutputFileFormat> ParseSite(string city, List<ProductData> products)
        {
            UpdateProgress(0);
            var result = new OutputFileFormat
            {
                City = city
            };

            List<Task<List<OutputData>>> tasks = new List<Task<List<OutputData>>>();
            foreach (var data in products)
            {
                var task = new Task<List<OutputData>>(() => AddProduct(city, new ProductData(data)).Result);
                tasks.Add(task);
                task.Start();
            }

            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                result.OutputProducts.AddRange(item);
            }


            UpdateProgress(1);
            return result;
        }
    }
}
