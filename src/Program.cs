namespace DevPad
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (Directory.Exists(args[0]))
                {
                    var pathsToNotScan = new[]
                    {
                        Path.Combine(args[0], ".git"),
                        Path.Combine(args[0], "bin"),
                        Path.Combine(args[0], "obj"),
                    };

                    var oneHourAgoInUtc = DateTime.UtcNow.AddHours(-1);
                    var recentlyWrittenThresholdInUtc = oneHourAgoInUtc;
                    var recentlyWrittenFiles = Directory.EnumerateFiles(args[0], "*", SearchOption.AllDirectories)
                                                        .Where(f => pathsToNotScan.All(p => !f.StartsWith(p)))
                                                        .Where(f => new FileInfo(f).LastWriteTimeUtc > recentlyWrittenThresholdInUtc)
                                                        .OrderByDescending(f => new FileInfo(f).LastWriteTimeUtc)
                                                        .ToArray()
                        ;
                    Application.Run(new MainForm(recentlyWrittenFiles));
                }
                else
                {
                    Application.Run(new MainForm(args));
                }
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
