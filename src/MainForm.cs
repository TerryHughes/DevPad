namespace DevPad
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    internal partial class MainForm : Form
    {
        internal MainForm()
        {
            InitializeComponents();

            saveToolStripMenuItem.Click += (s, e) => Save();
            openToolStripMenuItem.Click += (s, e) => Open();
            newToolStripMenuItem.Click += (s, e) => New();
        }

        private void Save()
        {
            var dialog = new SaveFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            File.WriteAllText(dialog.FileName, richTextBox.Text.Replace("\n", Environment.NewLine), Encoding.Default);
        }

        private void Open()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            richTextBox.Text = File.ReadAllText(dialog.FileName, Encoding.Default);
        }

        private void New()
        {
            richTextBox.Text = String.Empty;
        }
    }
}
