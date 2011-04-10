namespace DevPad
{
    using System.Windows.Forms;

    partial class MainForm
    {
        private RichTextBox richTextBox;
        private MenuStrip menuStrip;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;

        private void InitializeComponents()
        {
            richTextBox = new RichTextBox();
            menuStrip = new MenuStrip();
            saveToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();

            menuStrip.SuspendLayout();
            SuspendLayout();

            richTextBox.Dock = DockStyle.Fill;

            menuStrip.Items.Add(saveToolStripMenuItem);
            menuStrip.Items.Add(openToolStripMenuItem);
            menuStrip.Items.Add(newToolStripMenuItem);

            saveToolStripMenuItem.Text = "&Save";
            openToolStripMenuItem.Text = "&Open";
            newToolStripMenuItem.Text = "&New";

            Controls.Add(richTextBox);
            Controls.Add(menuStrip);

            Text = "DevPad";

            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
