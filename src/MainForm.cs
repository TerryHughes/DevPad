namespace DevPad
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    partial class MainForm : Form
    {
        internal MainForm()
        {
            InitializeComponents();

            IsMdiContainer = true;

            newToolStripMenuItem.Click += (s, e) => New();
            openToolStripMenuItem.Click += (s, e) => Open();
            closeToolStripMenuItem.Click += (s, e) => CloseForm();
            saveToolStripMenuItem.Click += (s, e) => Save();
            exitToolStripMenuItem.Click += (s, e) => Exit();
        }

        internal MainForm(string[] fileNames) : this()
        {
            if (fileNames.Length == 1 && Directory.Exists(fileNames[0]))
            {
                var currentDirectory = fileNames[0];
                var binDirectory = Path.Combine(currentDirectory, "bin");
                var objDirectory = Path.Combine(currentDirectory, "obj");
                var oneHourAgoUtc = DateTime.UtcNow.AddHours(-1);

                fileNames = Directory.EnumerateFiles(currentDirectory, "*", SearchOption.AllDirectories)
                                     .Where(f => !f.StartsWith(binDirectory) && !f.StartsWith(objDirectory))
                                     .Where(f => new FileInfo(f).LastWriteTimeUtc > oneHourAgoUtc)
                                     .ToArray()
                    ;
            }

            foreach (var fileName in fileNames)
            {
                var form = new DocumentForm(fileName);
                form.MdiParent = this;
                form.Show();
            }

            /*
            if (fileNames.Length > 1)
                LayoutMdi(MdiLayout.Cascade);
            */
        }

        void New()
        {
            var form = new DocumentForm();
            form.MdiParent = this;
            form.Show();
        }

        void Open()
        {
            var dialog = new OpenFileDialog() { Multiselect = true };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var fileName in dialog.FileNames)
            {
                var form = new DocumentForm(fileName);
                form.MdiParent = this;
                form.Show();
            }
        }

        void CloseForm()
        {
            ((DocumentForm)ActiveMdiChild).Close();
        }

        void Save()
        {
            ((DocumentForm)ActiveMdiChild).Save();
        }

        void Exit()
        {
            Close();
        }
    }
}
