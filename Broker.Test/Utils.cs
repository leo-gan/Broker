using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Contracts;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Broker.Test
{
    internal class Utils
    {
        private static readonly string sampleDir =
           @"C:\Projects\CMS\Integration\JubDelq\Apps\Broker\Broker.Test\Samples\";

        private static string EmptyDir = sampleDir + "Empty";
        private static readonly string DoNotChangeDir = sampleDir + "DoNotChange";
        private static string InputDir = sampleDir + "Input";
        private static string ArchiveDir = sampleDir + "Archive";

        private static readonly string oldestFile =
            @"Tst-SAVE-CR_All_SAVE-CR-CASE_16LJJD00005A_lganeline-2016-07-11T16-16-37-130.xml";
         private static Config config = new Config();
    
       public static IEnumerable<CallRequest> CreateCallRequests(string filePath)
        {
            config.Initialize();
            var msg = XDocument.Load(filePath);
            var controlPoint = Processor.GetControlPoint(msg);
            Assert.IsNotNullOrEmpty(controlPoint);
            Console.WriteLine(controlPoint);
            var rqs = new List<CallRequest>();
            foreach (var targetOperation in config.GetOperations(controlPoint))
            {
                var callRequest = Processor.CreateCallRequest(config, msg, controlPoint, targetOperation);
                Assert.IsNotNull(callRequest);
                Console.WriteLine(JsonConvert.SerializeObject(callRequest, Formatting.Indented));
                rqs.Add(callRequest);
            }
            return rqs;
        }
        public static string GetControlPoint()
        {
            config.Initialize();
            var msg = FileManager.GetMessage(DoNotChangeDir); // get any message
            var controlPoint = Processor.GetControlPoint(msg.Item2);
            Assert.IsNotNullOrEmpty(controlPoint);
            Console.WriteLine(controlPoint);
            return controlPoint;
        }

    }
}
