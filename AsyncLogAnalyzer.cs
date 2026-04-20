using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LogAnalyzerTool
{
    public class AsyncLogAnalyzer : ILogAnalyzer
    {
        public IDictionary<string, int> Analyze(string filePath)
            => AnalyzeAsync(filePath).GetAwaiter().GetResult();

        public async Task<IDictionary<string, int>> AnalyzeAsync(string filePath)
        {
            var stats = new Dictionary<string, int>();
            await foreach (var line in File.ReadLinesAsync(filePath))
            {
                string logType = LogParser.ExtractLogType(line);
                if (logType != "UNKNOWN")
                {
                    if (!stats.ContainsKey(logType)) stats[logType] = 1;
                    else stats[logType]++;
                }
            }
            return stats;
        }
    }
}