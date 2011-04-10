namespace DevPad
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    internal partial class MainForm : Form
    {
        private string fileName;

        internal MainForm()
        {
            InitializeComponents();

            fileName = String.Empty;

            saveToolStripMenuItem.Click += (s, e) => Save();
            openToolStripMenuItem.Click += (s, e) => Open();
            newToolStripMenuItem.Click += (s, e) => New();
        }

        private void Save()
        {
            if ((fileName == String.Empty) && DialogCancelled(new SaveFileDialog()))
            {
                return;
            }

            File.WriteAllText(fileName, richTextBox.Text.Replace("\n", Environment.NewLine), Encoding.Default);
        }

        private void Open()
        {
            if (DialogCancelled(new OpenFileDialog()))
            {
                return;
            }

            richTextBox.Text = File.ReadAllText(fileName, Encoding.Default);
        }

        private void New()
        {
            fileName = richTextBox.Text = String.Empty;
        }

        private bool DialogCancelled(FileDialog dialog)
        {
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return true;
            }

            fileName = dialog.FileName;

            return false;
        }
    }
}
