using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.DataModel
{
    public class OutputFileFormat
    {
        public Site Site { get; set; }
        public string City { get; set; }
        public DateTime CreateTime { get; set; }
        public List<OutputData> OutputProducts { get; set; }
        public List<string> Exceptions { get; set; }

        public OutputFileFormat()
        {
            OutputProducts = new List<OutputData>();
            Exceptions = new List<string>();
        }
    }
}
