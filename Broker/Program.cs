using System.Threading;

namespace Broker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new Config();
            config.Initialize(); 
            Program.Loop(config);
        }

        public static void Loop(Config config)
        {
            var log = new Log(config.LogAddress);
            while (true) // infinite loop
            {
                var msg = FileManager.GetMessage(config.InputEndpointAddress);
                if (msg != null)
                {
                    var controlPoint = Processor.GetControlPoint(msg.Item2);
                    foreach (var targetOperation in config.GetOperations(controlPoint))
                    {
                        var callAddress = config.CallAddresses[targetOperation];
                        var callRequest = Processor.CreateCallRequest(config, msg.Item2, controlPoint, targetOperation);
                        var res = Caller.Call(callAddress, callRequest, log);
                        FileManager.ArchiveMessage(config.InputEndpointAddress, config.ArchiveAddress, msg.Item1);
                    }
                }
                else // no new message found
                    Thread.Sleep(config.ListenCycleDelay_msec);
                log.CreateConditionalReport(config.LastLogReportTimestamp, config.IntervalBetweenReports);
            }
        }
    }
}