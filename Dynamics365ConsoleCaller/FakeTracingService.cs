using Microsoft.Xrm.Sdk;
using System;
using System.IO;

namespace Dynamics365ConsoleCaller
{
    class FakeTracingService : ITracingService
    {
        public string TraceTextFile { get; set; }

        public FakeTracingService() { }

        public FakeTracingService(string traceTextFile)
        {
            TraceTextFile = traceTextFile + ".txt";
            File.AppendAllText(TraceTextFile, string.Format(
                "Fake Tracing Service Instance Created at {1}{0}https://github.com/AshV/Dynamics365ConsoleCaller {0}",
                Environment.NewLine,
                DateTime.Now));
        }

        public void Trace(string format, params object[] args)
        {
            if (string.IsNullOrEmpty(TraceTextFile))
            {
                Console.WriteLine(string.Format(format, args));
            }
            else
            {
                File.AppendAllText(TraceTextFile, $"{Environment.NewLine}{DateTime.Now} -> {string.Format(format, args)}");
            }
        }
    }
}