using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace TestProj.DataModel
{
    public class OutputData
    {
        [Name("Продукт")] public string ProductName { get; set; }
        [Name("Аптека")] public string Name { get; set; }
        [Name("Адрес")] public string Address { get; set; }
        [Name("Количество")] public string Count { get; set; }

        public OutputData(string productName, string name, string address, string count)
        {
            ProductName = productName;
            Address = address;
            Name = name;
            Count = count;
        }

        public override string ToString()
        {
            return $"{ProductName};{Name};{Address};{Count}";
        }
    }
}
