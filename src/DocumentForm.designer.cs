namespace DevPad
{
    using System.Drawing;
    using System.Windows.Forms;

    partial class DocumentForm
    {
        RichTextBox richTextBox;

        void InitializeComponents()
        {
            richTextBox = new RichTextBox();

            SuspendLayout();

            richTextBox.AcceptsTab = true;
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Font = new Font("Consolas", 10, FontStyle.Regular, GraphicsUnit.Point, 0);

            Controls.Add(richTextBox);

            Size = new Size(800, 600);
            WindowState = FormWindowState.Maximized;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
