using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogAnalyzerTool
{
    public interface ILogAnalyzer
    {
        IDictionary<string, int> Analyze(string filePath);

       
        Task<IDictionary<string, int>> AnalyzeAsync(string filePath);
    }
}