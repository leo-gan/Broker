using System;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Broker.Test
{
    [TestFixture]
    public class CallerTest
    {
        private static readonly string sampleDir =
            @"C:\Projects\CMS\Integration\JubDelq\Apps\Broker\Broker.Test\Samples\";

        private static string EmptyDir = sampleDir + "Empty";
        private static readonly string DoNotChangeDir = sampleDir + "DoNotChange";
        private static readonly string InputDir = sampleDir + "Input";
        private static string ArchiveDir = sampleDir + "Archive";

        private static readonly string oldestFile =
            @"Tst-SAVE-CR_All_SAVE-CR-CASE_16LJJD00005A_lganeline-2016-07-11T16-16-37-130.xml";

        private static Config config = new Config();

        [Test]
        public void Call()
        {
            config.Initialize();

            // empty InputDir:
            var files = Directory.EnumerateFiles(InputDir);
            foreach (var file in files)
                File.Delete(file);
            // copy a file from DoNotChangeDir into InputDir
             files = Directory.EnumerateFiles(DoNotChangeDir);
            foreach (var file in files)
                File.Copy(file, Path.Combine(InputDir, Path.GetFileName(file)));

            // 
            files = Directory.EnumerateFiles(InputDir);
            foreach (var file in files)
            {
                var msg = XDocument.Load(file);

                var controlPoint = Processor.GetControlPoint(msg);
                Console.WriteLine(controlPoint);
                var log = new Log(config.LogAddress);
                foreach (var targetOperation in config.GetOperations(controlPoint))
                {
                    var callAddress = config.CallAddresses[targetOperation];
                    var callRequest = Processor.CreateCallRequest(config, msg, controlPoint, targetOperation);
                    Console.WriteLine(
                        "Call to {0} with controlPoint {1} and operation {2}",
                        callAddress,
                        controlPoint,
                        targetOperation);
                    var res = Caller.Call(callAddress, callRequest, log);
                    Assert.AreEqual("Success", res);
                    Console.WriteLine(
                        " Successful Call to {0} with controlPoint {1}, operation {2}, and Request \n {3} ",
                        callAddress,
                        controlPoint,
                        targetOperation,
                        JsonConvert.SerializeObject(callRequest));
                }
            }
                // empty InputDir:
                files = Directory.EnumerateFiles(InputDir);
                foreach (var file in files)
                    File.Delete(file);
        }

        [Test]
        public void CallOneFile()
        {
            config.Initialize();
            // empty InputDir:
            var files = Directory.EnumerateFiles(InputDir);
            foreach (var file in files)
                File.Delete(file);
            // copy a file from DoNotChangeDir into InputDir
             files = Directory.EnumerateFiles(DoNotChangeDir);
            var testFile = "";
            foreach (var file in files)
            {
                testFile = Path.Combine(InputDir, Path.GetFileName(file));
                File.Copy(file, testFile);
                break; // only one!
            }
            var msg = XDocument.Load(testFile);

            var controlPoint = Processor.GetControlPoint(msg);
            var log = new Log(config.LogAddress);
            foreach (var targetOperation in config.GetOperations(controlPoint))
            {
                var callAddress = config.CallAddresses[targetOperation];
                var callRequest = Processor.CreateCallRequest(config, msg, controlPoint, targetOperation);
                Console.WriteLine(
                    "Call to {0} with controlPoint {1} and operation {2}",
                    callAddress,
                    controlPoint,
                    targetOperation);
                var res = Caller.Call(callAddress, callRequest, log);
                Assert.AreEqual("Success", res);
                Console.WriteLine(
                    " Successful Call to {0} with controlPoint {1}, operation {2}, and Request \n {3} ",
                    callAddress,
                    controlPoint,
                    targetOperation,
                    JsonConvert.SerializeObject(callRequest));
            }
            // empty InputDir:
            files = Directory.EnumerateFiles(InputDir);
            foreach (var file in files)
                File.Delete(file);

        }
    }
}