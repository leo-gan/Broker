using System;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Broker.Test
{
    [TestFixture]
    public class ProcessorTest
    {
        private static readonly string sampleDir =
            @"C:\Projects\CMS\Integration\JubDelq\Apps\Broker\Broker.Test\Samples\";

        private static string EmptyDir = sampleDir + "Empty";
        private static readonly string DoNotChangeDir = sampleDir + "DoNotChange";
        private static string InputDir = sampleDir + "Input";
        private static string ArchiveDir = sampleDir + "Archive";

        private static readonly string oldestFile =
            @"Tst-SAVE-CR_All_SAVE-CR-CASE_16LJJD00005A_lganeline-2016-07-11T16-16-37-130.xml";

        [Test]
        public void CreateCallRequests()
        {
            // empty InputDir:
            var files = Directory.EnumerateFiles(InputDir);
            foreach (var file in files)
                File.Delete(file);
            // copy files from DoNotChangeDir into InputDir
            files = Directory.EnumerateFiles(DoNotChangeDir);
            foreach (var file in files)
                File.Copy(file, Path.Combine(InputDir, Path.GetFileName(file)));

            files = Directory.EnumerateFiles(InputDir);
            foreach (var file in files)
            {
                var rqs = Utils.CreateCallRequests(file);
                Assert.IsNotNull(rqs);
            }

            //remove files from InputDir:
            files = Directory.EnumerateFiles(InputDir);
            foreach (var file in files)
                File.Delete(file);
        }

        [Test]
        public void GetControlPoint()
        {
            Utils.GetControlPoint();
        }
    }
}