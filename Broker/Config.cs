using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Broker
{
    public class Config
    {
        private static readonly string ConfigFilePath = Environment.CurrentDirectory + @"\config.json";
        private static readonly string ConfigTempFilePath = Environment.CurrentDirectory + @"\config.writable.json";
        private static readonly string ConfigMappingFilePath = Environment.CurrentDirectory + @"\config.mappings.json";
        public int ListenCycleDelay_msec { get; set; }
        public string InputEndpointAddress { get; set; }
        public string ArchiveAddress { get; set; }
        public List<Mapping> Mappings { get; set; }
       
        public DateTime LastLogReportTimestamp
        {
            get
            {
                var json = File.ReadAllText(ConfigTempFilePath);
                return JsonConvert.DeserializeObject<DateTime>(json);
            }
            set { File.WriteAllText(ConfigTempFilePath, JsonConvert.SerializeObject(value)); }
        }

        public TimeSpan IntervalBetweenReports { get; set; }
        public string LogAddress { get; set; }

        public Dictionary<string, string> CallAddresses { get; set; }

        public void Initialize()
        {
            var json = File.ReadAllText(ConfigFilePath);
            var tempConfig = JsonConvert.DeserializeObject<Config>(json);
            ListenCycleDelay_msec = tempConfig.ListenCycleDelay_msec;
            InputEndpointAddress = tempConfig.InputEndpointAddress;
            ArchiveAddress = tempConfig.ArchiveAddress;
            IntervalBetweenReports = tempConfig.IntervalBetweenReports;
            LogAddress = tempConfig.LogAddress;
            CallAddresses = tempConfig.CallAddresses;

            // LastLogReportTimestamp saved in a separate file which is 'writable'.
            json = File.ReadAllText(ConfigTempFilePath);
            LastLogReportTimestamp = JsonConvert.DeserializeObject<DateTime>(json);

            json = File.ReadAllText(ConfigMappingFilePath);
            Mappings = JsonConvert.DeserializeObject<List<Mapping>>(json);
        }

        public List<string> GetOperations(string controlPoint)
        {
            Initialize();
            return (from mapping in Mappings 
                    where mapping.ControlPoint != null && mapping.Operation != null
                    where mapping.ControlPoint == controlPoint
                    select mapping.Operation
                    ).ToList();
        }
    }
}