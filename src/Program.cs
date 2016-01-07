namespace DevPad
{
    using System;
    using System.Windows.Forms;

    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new MainForm(args));
        }
    }
}
