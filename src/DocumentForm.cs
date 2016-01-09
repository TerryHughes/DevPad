namespace DevPad
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    partial class DocumentForm : Form
    {
        readonly UnsavedChanges unsavedChanges;
        string fileName;

        internal DocumentForm()
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
        }

        internal DocumentForm(string fileName) : this()
        {
            this.fileName = fileName;
            Text = fileName;

            richTextBox.Text = File.ReadAllText(fileName, Encoding.Default);

            unsavedChanges.Reset();
        }

        internal void Save()
        {
            if ((fileName == string.Empty) && DialogCancelled(new SaveFileDialog()))
            {
                return;
            }

            File.WriteAllText(fileName, richTextBox.Text.Replace("\n", Environment.NewLine), Encoding.Default);

            unsavedChanges.Reset();
        }

        bool DialogCancelled(FileDialog dialog)
        {
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return true;
            }

            fileName = dialog.FileName;
            Text = fileName;

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
