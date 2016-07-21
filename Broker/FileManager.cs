using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Broker
{
    public class FileManager
    {
        /// <summary>
        ///     It listens a directory and returns the oldest file in it.
        ///     This simulates the ordered delivery.
        /// </summary>
        /// <param name="endpointAddress"></param>
        /// <returns>
        ///     Tuple: Item1:filePath, Item2:XDocument (file content)
        ///     Returns null if directory is empty.
        /// </returns>
        public static Tuple<string, XDocument> GetMessage(string endpointAddress)
        {
            if (Directory.GetFiles(endpointAddress).Length == 0) return null;
            var oldestFileInfo = new DirectoryInfo(endpointAddress).GetFileSystemInfos()
                .OrderBy(fi => fi.LastWriteTime).First();
            var filePath = oldestFileInfo.FullName;
            var xdoc = XDocument.Load(filePath);

            return new Tuple<string, XDocument>(filePath, xdoc);
        }

        public static void ArchiveMessage(string inputAddress, string archiveAddress, string fileName)
        {
            var fromFilePath = Path.Combine(inputAddress, fileName);
            var toFilePath = Path.Combine(archiveAddress, fileName);
            File.Move(fromFilePath, toFilePath);
        }
    }
}