using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LogAnalyzerTool
{
    public class ParallelLogAnalyzer : ILogAnalyzer
    {
        public IDictionary<string, int> Analyze(string filePath)
        {
            
            var stats = new ConcurrentDictionary<string, int>();

          
            Parallel.ForEach(File.ReadLines(filePath), line =>
            {
                string logType = LogParser.ExtractLogType(line);
                if (logType != "UNKNOWN")
                {
                    stats.AddOrUpdate(logType, 1, (key, oldValue) => oldValue + 1);
                }
            });

            return stats;
        }

        public Task<IDictionary<string, int>> AnalyzeAsync(string filePath)
            => Task.Run(() => Analyze(filePath)); 
    }
}