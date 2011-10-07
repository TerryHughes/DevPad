namespace DevPad
{
    using System.Drawing;
    using System.Windows.Forms;

    partial class MainForm
    {
        RichTextBox richTextBox;
        MenuStrip menuStrip;
        ToolStripMenuItem saveToolStripMenuItem;
        ToolStripMenuItem openToolStripMenuItem;
        ToolStripMenuItem newToolStripMenuItem;

        void InitializeComponents()
        {
            richTextBox = new RichTextBox();
            menuStrip = new MenuStrip { Visible = false };
            saveToolStripMenuItem = new ToolStripMenuItem { Visible = false };
            openToolStripMenuItem = new ToolStripMenuItem { Visible = false };
            newToolStripMenuItem = new ToolStripMenuItem { Visible = false };

            menuStrip.SuspendLayout();
            SuspendLayout();

            richTextBox.AcceptsTab = true;
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Font = new Font("Consolas", 10, FontStyle.Regular, GraphicsUnit.Point, 0);

            menuStrip.Items.Add(saveToolStripMenuItem);
            menuStrip.Items.Add(openToolStripMenuItem);
            menuStrip.Items.Add(newToolStripMenuItem);

            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;

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
