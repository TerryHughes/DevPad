namespace DevPad
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    internal partial class MainForm : Form
    {
        private readonly UnsavedChanges unsavedChanges;
        private string fileName;

        internal MainForm()
        {
            InitializeComponents();

            unsavedChanges = new UnsavedChanges(this, Save);
            fileName = String.Empty;

            FormClosing += (s, e) =>
            {
                if (unsavedChanges.WereNotHandled())
                {
                    e.Cancel = true;
                }
            };

            richTextBox.TextChanged += (s, e) => unsavedChanges.HaveOccurred();
            richTextBox.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Tab)
                {
                    InsertSpacesForTab();
                    e.Handled = true;
                }
            };

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

            unsavedChanges.Reset();
        }

        private void Open()
        {
            if (unsavedChanges.WereNotHandled() || DialogCancelled(new OpenFileDialog()))
            {
                return;
            }

            richTextBox.Text = File.ReadAllText(fileName, Encoding.Default);

            unsavedChanges.Reset();
        }

        private void New()
        {
            if (unsavedChanges.WereNotHandled())
            {
                return;
            }

            fileName = richTextBox.Text = String.Empty;
            Text = "DevPad";

            unsavedChanges.Reset();
        }

        private bool DialogCancelled(FileDialog dialog)
        {
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return true;
            }

            fileName = dialog.FileName;
            Text = "DevPad - " + fileName;

            return false;
        }

        private void InsertSpacesForTab()
        {
            var startOfLine = richTextBox.GetFirstCharIndexOfCurrentLine();
            var caret = richTextBox.SelectionStart;

            const int spacesPerTabStop = 4;
            var charactersPastPreviousTabStop = (caret - startOfLine) % spacesPerTabStop;
            var spacesToNextTabStop = new String(Enumerable.Range(0, spacesPerTabStop - charactersPastPreviousTabStop).Select(i => ' ').ToArray());

            richTextBox.SelectedText = spacesToNextTabStop;
        }
    }
}
