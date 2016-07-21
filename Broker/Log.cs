using System;

namespace Broker
{
    public class Log
    {
        public Log(string logAddress)
        {
            Console.WriteLine("Log constructor is not implemented");
        }

        public void CreateConditionalReport(DateTime lastLogReportTimestamp, TimeSpan intervalBetweenReports)
        {
            throw new NotImplementedException();
        }
    }
}