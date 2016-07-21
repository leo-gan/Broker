using System;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using System.Xml;
using Contracts;
using System.Xml.Serialization;

namespace WebServiceStub
{
    public class Service : IService
    {
        public string PetitionAddUpdate(CallRequest request)
        {
            Service.Print(request);
            return "Success";
        }

         public string AddMinute(CallRequest request)
        {
            Service.Print(request);
            return "Success";
        } 
        
        private static void Print(CallRequest request)
        {
            var serializer = new XmlSerializer(typeof(CallRequest));
            var settitngs = new XmlWriterSettings {Indent = true};
            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw,settitngs))
            {
                serializer.Serialize(xw,request);
                File.WriteAllText(Service.GenerateFilePath(request.Operation), sw.ToString());
            }
        }

        private static string GenerateFilePath(string operation)
        {
            var archiveFolder = ConfigurationManager.AppSettings["WebServiceStub.Service.ArchiveFolder"];
            return archiveFolder + operation + "." + Guid.NewGuid() + ".xml";
        }
    }
}