namespace DevPad
{
    using System.Drawing;
    using System.Windows.Forms;

    partial class MainForm
    {
        MenuStrip menuStrip;
        ToolStripMenuItem fileToolStripMenuItem;
        ToolStripMenuItem newToolStripMenuItem;
        ToolStripMenuItem openToolStripMenuItem;
        ToolStripMenuItem closeToolStripMenuItem;
        ToolStripMenuItem saveToolStripMenuItem;
        ToolStripMenuItem exitToolStripMenuItem;

        void InitializeComponents()
        {
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem("&File");
            newToolStripMenuItem = new ToolStripMenuItem("&New");
            openToolStripMenuItem = new ToolStripMenuItem("&Open");
            closeToolStripMenuItem = new ToolStripMenuItem("&Close");
            saveToolStripMenuItem = new ToolStripMenuItem("&Save");
            exitToolStripMenuItem = new ToolStripMenuItem("E&xit");

            menuStrip.SuspendLayout();
            SuspendLayout();

            menuStrip.Items.Add(fileToolStripMenuItem);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                newToolStripMenuItem,
                openToolStripMenuItem,
                new ToolStripSeparator(),
                closeToolStripMenuItem,
                new ToolStripSeparator(),
                saveToolStripMenuItem,
                new ToolStripSeparator(),
                exitToolStripMenuItem,
            });

            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            closeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;

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
