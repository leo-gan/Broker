using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Broker.Test
{
    [TestFixture]
    public class ConfigTest
    {
        [SetUp]
        protected void SetUp() {}

        private static readonly string ConfigFilePath = Environment.CurrentDirectory + @"\config.json";
        private static readonly string ConfigTempFilePath = Environment.CurrentDirectory + @"\config.writable.json";

        [Test]
        public void Initialize()
        {
            var config = new Config();
            config.Initialize();

            Assert.IsNotNull(config);
            Assert.IsNotNull(config.ArchiveAddress);
            Assert.IsNotNull(config.InputEndpointAddress);
            Assert.IsNotNull(config.IntervalBetweenReports);
            Assert.IsNotNull(config.LastLogReportTimestamp);
            Assert.IsNotNull(config.ListenCycleDelay_msec);
            Assert.IsNotNull(config.LogAddress);
            Assert.IsNotNull(config.CallAddresses);
            Assert.Greater(config.CallAddresses.Count, 0);
            foreach (var callAddress in config.CallAddresses)
            {
                Assert.IsNotNull(callAddress.Key);
                foreach (var singleAddress in callAddress.Value)
                    Assert.IsNotNull(singleAddress);
            }
        }

        [Test]
        public void InitializeMappings()
        {
            var config = new Config();
            config.Initialize();

            Assert.IsNotNull(config);
            Assert.IsNotNull(config.Mappings);
            Assert.Greater(config.Mappings.Count, 0);

            Assert.IsNotNull(config.Mappings[0].ControlPoint);
            Assert.IsNotNull(config.Mappings[0].Operation);
            Assert.IsNotNull(config.Mappings[0].Fields);

            Assert.IsNotNull(config.Mappings[0].Fields);
            Assert.Greater(config.Mappings[0].Fields.Count, 0);
            Assert.IsNotNull(config.Mappings[0].Fields.Keys);
            Assert.IsNotNull(config.Mappings[0].Fields.Values);

            foreach (var field in config.Mappings[0].Fields)
            {
                Assert.IsNotNull(field.Key);
                foreach (var xpath in field.Value)
                    Assert.IsNotNull(xpath);
            }
        }

        [Test]
        public void LastLogReportTimestamp()
        {
            var ts = DateTime.Now;
            var config = new Config();
            config.Initialize();
            config.LastLogReportTimestamp = ts;
            Assert.AreEqual(ts, config.LastLogReportTimestamp);
        }
        [Test]
        public void GetTargetOperations()
        {
            var config = new Config();
            config.Initialize();
            // list all control points in mapping:
            foreach (var mapping in config.Mappings)
                // output all controlPoint-Operation pairs:
                foreach (var operation in config.GetOperations(mapping.ControlPoint))
                    Console.WriteLine("{0,32} {1}", mapping.ControlPoint, operation);
        }
        [Test]
        /// <summary>
        ///     Used to create a template of config file in development time.
        /// </summary>
        private void WriteFiles()
        {
            var config = new Config();
            config.Initialize();
            //ListenCycleDelay_msec = 100;
            //InputEndpointAddress = @"ody:51.25.54.56/WebAPI";
            //ArchiveAddress = @"c:/temp/Archive";
            //LastLogReportTimestamp = DateTime.Now;
            //IntervalBetweenReports = TimeSpan.FromSeconds(1000);
            //LogAddress = @"c:/temp/Log";
            //CallAddresses = new Dictionary<string, IEnumerable<string>>()
            //{
            //    {"Petition", new[]  {@"http:Colverleaf.com/AddPetition", @"http:Colverleaf.com/AddCase1"}},
            //    {"AddMinute", new[]  {@"http:Colverleaf.com/AddMinute", @"http:Colverleaf.com/AddCase2"}}
            //};
            // Initialize(); // assing config params OR Initialize() before WriteFiles()

            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(ConfigFilePath, json);
            json = JsonConvert.SerializeObject(config.LastLogReportTimestamp, Formatting.Indented);
            File.WriteAllText(ConfigTempFilePath, json);
        }
    }
}