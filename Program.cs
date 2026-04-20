using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LogAnalyzerTool
{
    class Program
    {
        const string LogFilePath = "server_large_logs.txt";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("===  ĐUA TỐC ĐỘ: SYNC vs ASYNC vs PARALLEL ===\n");

            var logGenerator = new LogGenerator();
            var reportPrinter = new ReportPrinter();

            
            ILogAnalyzer syncAnalyzer = new SyncLogAnalyzer();
            ILogAnalyzer asyncAnalyzer = new AsyncLogAnalyzer();
            ILogAnalyzer parallelAnalyzer = new ParallelLogAnalyzer();

            await logGenerator.GenerateMockLogAsync(LogFilePath, 5_000_000);

            // --- VÒNG 1: SYNC ---
            var sw = Stopwatch.StartNew();
            var syncResult = syncAnalyzer.Analyze(LogFilePath);
            sw.Stop();
            reportPrinter.Print("1. CHẾ ĐỘ SYNC (Tuần tự, Chặn luồng)", syncResult, sw.ElapsedMilliseconds);

            // --- VÒNG 2: ASYNC ---
            sw.Restart();
            var asyncResult = await asyncAnalyzer.AnalyzeAsync(LogFilePath);
            sw.Stop();
            reportPrinter.Print("2. CHẾ ĐỘ ASYNC (Tuần tự, KHÔNG chặn luồng)", asyncResult, sw.ElapsedMilliseconds);

            // --- VÒNG 3: PARALLEL ---
            sw.Restart();
            var parallelResult = parallelAnalyzer.Analyze(LogFilePath);
            sw.Stop();
            reportPrinter.Print("3. CHẾ ĐỘ PARALLEL (Đa luồng, Tận dụng CPU)", parallelResult, sw.ElapsedMilliseconds);

            Console.WriteLine("\n🚀 KẾT LUẬN:");
            Console.WriteLine("- Sync & Async: Thời gian chạy thường ngang ngửa nhau (vì cùng dùng 1 nhân CPU). Nhưng Async giúp Web Server không bị sập khi có nhiều người truy cập.");
            Console.WriteLine("- Parallel: Thời gian chạy nhanh nhất (vắt kiệt CPU). Nhưng cân nhắc không dùng Parallel trên Web Server để tránh chiếm dụng CPU của các user khác!");
        }
    }
}