namespace DevPad
{
    using System.Drawing;
    using System.Windows.Forms;

    partial class MainForm
    {
        ToolStripMenuItem saveToolStripMenuItem;
        ToolStripMenuItem closeToolStripMenuItem;
        ToolStripMenuItem openToolStripMenuItem;
        ToolStripMenuItem newToolStripMenuItem;

        void InitializeComponents()
        {
            var menuStrip = new MenuStrip();
            saveToolStripMenuItem = new ToolStripMenuItem { Visible = false };
            closeToolStripMenuItem = new ToolStripMenuItem { Visible = false };
            openToolStripMenuItem = new ToolStripMenuItem { Visible = false };
            newToolStripMenuItem = new ToolStripMenuItem { Visible = false };

            menuStrip.SuspendLayout();
            SuspendLayout();

            menuStrip.Items.Add(saveToolStripMenuItem);
            menuStrip.Items.Add(closeToolStripMenuItem);
            menuStrip.Items.Add(openToolStripMenuItem);
            menuStrip.Items.Add(newToolStripMenuItem);

            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            closeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;

            Controls.Add(menuStrip);

            Size = new Size(800, 600);
            Text = "DevPad";
            WindowState = FormWindowState.Maximized;

            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();

            ResumeLayout(false);
            PerformLayout();

            MainMenuStrip = menuStrip;
        }
    }
}
