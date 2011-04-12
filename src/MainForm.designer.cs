namespace DevPad
{
    using System.Drawing;
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

            richTextBox.AcceptsTab = true;
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Font = new Font("Consolas", 10, FontStyle.Regular, GraphicsUnit.Point, 0);

            menuStrip.Items.Add(saveToolStripMenuItem);
            menuStrip.Items.Add(openToolStripMenuItem);
            menuStrip.Items.Add(newToolStripMenuItem);

            saveToolStripMenuItem.Text = "&Save";
            openToolStripMenuItem.Text = "&Open";
            newToolStripMenuItem.Text = "&New";

            Controls.Add(richTextBox);
            Controls.Add(menuStrip);

            Size = new Size(800, 600);
            Text = "DevPad";
            WindowState = FormWindowState.Maximized;

            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
