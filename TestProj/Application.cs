using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProj.BaseClasses;
using TestProj.DataModel;
using TestProj.SiteParsers;
using System.IO;
using System.Text;
using System.Globalization;
using CsvHelper;
using Newtonsoft;
using Newtonsoft.Json;

namespace TestProj
{
    public class Application
    {
        private string _configPath;

        private Dictionary<Site, BaseSiteParser> _siteParsers;

        public Application(string pathToConfig)
        {
            _configPath = pathToConfig;

            _siteParsers = new Dictionary<Site, BaseSiteParser>
            {
                [Site.Volgofarm] = new VolgofarmSiteParser()
            };
        }

        public MainConfig GetConfigObj()
        {
            var res = File.ReadAllText(_configPath);
            return JsonConvert.DeserializeObject<MainConfig>(res);
        }

        public async Task Run()
        {
            var mainConfig = GetConfigObj();
            await Task.Delay(TimeSpan.FromSeconds(mainConfig.TimeOut));
            mainConfig = GetConfigObj();
            var resFiles = new List<OutputFileFormat>();

            foreach (var parserConfig in mainConfig.SiteConfigs)
            {
                var tmpRes = await _siteParsers[parserConfig.SiteParserType].ParseSite(parserConfig);
                resFiles.AddRange(tmpRes);
            }

            var directoryName = Path.Combine("Results", "Output", DateTime.UtcNow.ToUniversalTime().ToString("yyyy_MM_dd_HH_mm_ss_fff"));
            directoryName = directoryName.Replace(':', '_');

            Directory.CreateDirectory(directoryName);
            
            foreach (var item in resFiles)
            {
                Save(item, directoryName);
            }
        }

        private void Save(OutputFileFormat fileFormat, string directoryName)
        {
            WriteToTargetPath(fileFormat.OutputProducts, Path.Combine(directoryName, $"{fileFormat.Site}_{fileFormat.City}"));
        }

        private void WriteToTargetPath(IEnumerable<OutputData> values, string fileName)
        {
            var fullFileName = $"{fileName}_{DateTime.Now.ToLocalTime().ToString("yyyyMMddHHmmssfff")}.csv";
            fileName = fullFileName.Replace(':', '_');

            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
            {               
                var serializer = new CsvHelper.CsvSerializer(sw, CultureInfo.CurrentCulture);
                using (CsvWriter csvReader = new CsvWriter(serializer))
                {
                    csvReader.Configuration.Delimiter = "\t";
                    try
                    {
                        csvReader.WriteRecords(values);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Please close {fileName} file");
                        throw;
                    }
                }
            }
        }
    }
}
