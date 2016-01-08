namespace DevPad
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    partial class MainForm : Form
    {
        readonly UnsavedChanges unsavedChanges;
        string fileName;

        internal MainForm()
        {
            InitializeComponents();

            unsavedChanges = new UnsavedChanges(this, Save);
            fileName = string.Empty;

            FormClosing += (s, e) =>
            {
                if (unsavedChanges.WereNotHandled())
                {
                    e.Cancel = true;
                }
            };

            richTextBox.TextChanged += (s, e) =>
            {
                var indicator = " *";

                if ((fileName != string.Empty) && !Text.EndsWith(fileName + indicator))
                {
                    Text += indicator;
                }

                unsavedChanges.HaveOccurred();
            };

            richTextBox.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Tab)
                {
                    InsertSpacesForTab();
                    e.Handled = true;
                }

                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    IndentNewLine();
                    e.Handled = true;
                }
            };

            saveToolStripMenuItem.Click += (s, e) => Save();
            openToolStripMenuItem.Click += (s, e) => Open();
            newToolStripMenuItem.Click += (s, e) => New();
        }

        internal MainForm(string fileName) : this()
        {
            this.fileName = fileName;
            Text = "DevPad - " + fileName;

            richTextBox.Text = File.ReadAllText(fileName, Encoding.Default);

            unsavedChanges.Reset();
        }

        void Save()
        {
            if ((fileName == string.Empty) && DialogCancelled(new SaveFileDialog()))
            {
                return;
            }

            File.WriteAllText(fileName, richTextBox.Text.Replace("\n", Environment.NewLine), Encoding.Default);

            unsavedChanges.Reset();

            Text = "DevPad - " + fileName;
        }

        void Open()
        {
            if (unsavedChanges.WereNotHandled() || DialogCancelled(new OpenFileDialog()))
            {
                return;
            }

            richTextBox.Text = File.ReadAllText(fileName, Encoding.Default);

            unsavedChanges.Reset();

            Text = "DevPad - " + fileName;
        }

        void New()
        {
            if (unsavedChanges.WereNotHandled())
            {
                return;
            }

            fileName = richTextBox.Text = string.Empty;
            Text = "DevPad";

            unsavedChanges.Reset();
        }

        bool DialogCancelled(FileDialog dialog)
        {
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return true;
            }

            fileName = dialog.FileName;
            Text = "DevPad - " + fileName;

            return false;
        }

        void InsertSpacesForTab()
        {
            var startOfLine = richTextBox.GetFirstCharIndexOfCurrentLine();
            var caret = richTextBox.SelectionStart;

            const int spacesPerTabStop = 4;
            var charactersPastPreviousTabStop = (caret - startOfLine) % spacesPerTabStop;
            var spacesToNextTabStop = new string(Enumerable.Range(0, spacesPerTabStop - charactersPastPreviousTabStop).Select(i => ' ').ToArray());

            richTextBox.SelectedText = spacesToNextTabStop;
        }

        void IndentNewLine()
        {
            var lineNumberOfPreviousLine = richTextBox.GetLineFromCharIndex(richTextBox.GetFirstCharIndexOfCurrentLine()) - 1;
            var previousLineText = richTextBox.Lines[lineNumberOfPreviousLine];

            var leadingSpaces = new string(previousLineText.TakeWhile(c => c == ' ').ToArray());

            if (previousLineText == leadingSpaces)
            {
                var caret = richTextBox.SelectionStart;

                richTextBox.SelectionStart = richTextBox.GetFirstCharIndexFromLine(lineNumberOfPreviousLine);
                richTextBox.SelectionLength = previousLineText.Length;
                richTextBox.SelectedText = string.Empty;

                richTextBox.SelectionStart = caret - previousLineText.Length;
            }

            richTextBox.SelectedText = leadingSpaces;
        }
    }
}
