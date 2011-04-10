namespace DevPad
{
    using System.Windows.Forms;

    partial class MainForm
    {
        private RichTextBox richTextBox;

        private void InitializeComponents()
        {
            richTextBox = new RichTextBox();

            SuspendLayout();

            richTextBox.Dock = DockStyle.Fill;

            Controls.Add(richTextBox);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
