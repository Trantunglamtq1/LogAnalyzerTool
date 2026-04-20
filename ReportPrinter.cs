using System;
using System.Collections.Generic;
using System.Linq;

namespace LogAnalyzerTool
{
    public class ReportPrinter
    {
        public void Print(string title, IDictionary<string, int> stats, long timeMs)
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"[ {title} ] - Hoàn thành trong: {timeMs} ms");

            var sortedStats = stats.OrderByDescending(x => x.Value);
            foreach (var kvp in sortedStats)
            {
                Console.WriteLine($"  {kvp.Key,-10}: {kvp.Value:N0} records");
            }
        }
    }
}