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

                    var lastWrittenFile = Directory.EnumerateFiles(args[0], "*", SearchOption.AllDirectories)
                                                   .Where(f => pathsToNotScan.All(p => !f.StartsWith(p)))
                                                   .OrderByDescending(f => new FileInfo(f).LastWriteTimeUtc)
                                                   .First()
                        ;
                    Application.Run(new MainForm(lastWrittenFile));
                }
                else
                {
                    Application.Run(new MainForm(args[0]));
                }
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
