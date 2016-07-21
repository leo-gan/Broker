using System;
using System.IO;
using NUnit.Framework;

namespace Broker.Test
{
    [TestFixture]
    public class FileManagerTest
    {
        [SetUp]
        protected void SetUp()
        {
            config.Initialize();
            Array.ForEach(Directory.GetFiles(InputDir), File.Delete);
            Array.ForEach(Directory.GetFiles(ArchiveDir), File.Delete);
            Array.ForEach(Directory.GetFiles(EmptyDir), File.Delete);
        }

        private readonly Config config = new Config();

        private static readonly string sampleDir =
            @"C:\Projects\CMS\Integration\JubDelq\Apps\Broker\Broker.Test\Samples\";

        private static readonly string EmptyDir = sampleDir + "Empty";
        private static readonly string DoNotChangeDir = sampleDir + "DoNotChange";
        private static readonly string InputDir = sampleDir + "Input";
        private static readonly string ArchiveDir = sampleDir + "Archive";

        private static readonly string oldestFile =
            @"Tst-SAVE-CR_All_SAVE-CR-CASE_16LJJD00005A_lganeline-2016-07-11T16-16-37-130.xml";

        [Test]
        public void ArchiveMessage()
        {
            // prepare a test. Copy a file to Input dir
            File.Copy(DoNotChangeDir + "\\" + oldestFile, InputDir + "\\" + oldestFile);
            Assert.AreEqual(Directory.GetFiles(InputDir).Length, 1);
            Assert.AreEqual(Directory.GetFiles(ArchiveDir).Length, 0);

            // archive a file:
            FileManager.ArchiveMessage(InputDir, ArchiveDir, oldestFile);
            Assert.AreEqual(Directory.GetFiles(InputDir).Length, 0);
            Assert.AreEqual(Directory.GetFiles(ArchiveDir).Length, 1);

            // archive file back
            FileManager.ArchiveMessage(ArchiveDir, InputDir, oldestFile);
            Assert.AreEqual(Directory.GetFiles(InputDir).Length, 1);
            Assert.AreEqual(Directory.GetFiles(ArchiveDir).Length, 0);

            // clean up after test
            File.Delete(InputDir + "\\" + oldestFile);
            Assert.AreEqual(Directory.GetFiles(InputDir).Length, 0);
        }

        [Test]
        public void GetMessage()
        {
            var msg = FileManager.GetMessage(DoNotChangeDir);
            Assert.IsNotNull(msg);
            Assert.IsNotNull(msg.Item1);
            Assert.IsNotNull(msg.Item2);
            // is the oldest file!
            Assert.AreEqual(DoNotChangeDir + "\\" + oldestFile, msg.Item1);
        }

        [Test]
        public void GetMessage_EmptyDirectory()
        {
            var msg = FileManager.GetMessage(EmptyDir);
            Assert.IsNull(msg);
        }

        [Test]
        public void MessageOrder()
        {
            // prepare a test. Copy all files into Input dir
            Assert.AreEqual(Directory.GetFiles(InputDir).Length, 0);
            var files = Directory.GetFiles(DoNotChangeDir);
            foreach (var file in files)
                File.Copy(file, InputDir + "\\" + Path.GetFileName(file));
            Assert.AreEqual(Directory.GetFiles(InputDir).Length, Directory.GetFiles(DoNotChangeDir).Length);
            Assert.AreNotEqual(Directory.GetFiles(InputDir).Length, 0);

            // get list of files with operations
            Console.WriteLine("Please, manually verify the files processed in the right order!!");
            Console.WriteLine("Operation:        File: ");
            while (true) // infinite loop
            {
                var msg = FileManager.GetMessage(InputDir);
                if (msg == null) break;
                var op = Processor.GetControlPoint(msg.Item2);
                Console.WriteLine("{0:N20} {1}", op, Path.GetFileName(msg.Item1));
                FileManager.ArchiveMessage(InputDir, ArchiveDir, Path.GetFileName(msg.Item1));
            }

            // clean up:
            Assert.AreEqual(Directory.GetFiles(ArchiveDir).Length, Directory.GetFiles(DoNotChangeDir).Length);
            Array.ForEach(Directory.GetFiles(ArchiveDir), File.Delete);
            Assert.AreEqual(Directory.GetFiles(ArchiveDir).Length, 0);
        }
    }
}