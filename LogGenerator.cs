using System;
using System.IO;
using System.Threading.Tasks;

namespace LogAnalyzerTool
{
    public class LogGenerator
    {
        public async Task GenerateMockLogAsync(string filePath, int lines)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine($"-> File log '{filePath}' đã có sẵn. Bỏ qua bước tạo dữ liệu.\n");
                return;
            }

            Console.WriteLine($"-> Đang tự động sinh file log với {lines:N0} dòng...");

            string[] types = { "[INFO]", "[INFO]", "[INFO]", "[WARNING]", "[DEBUG]", "[ERROR]" };
            Random rnd = new Random();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines; i++)
                {
                    string type = types[rnd.Next(types.Length)];
                    await writer.WriteLineAsync($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {type} - Action at module Y (Line {i})");
                }
            }
            Console.WriteLine("-> Đã tạo xong file log!\n");
        }
    }
}