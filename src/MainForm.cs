namespace DevPad
{
    using System.Windows.Forms;

    partial class MainForm : Form
    {
        internal MainForm()
        {
            InitializeComponents();

            IsMdiContainer = true;

            saveToolStripMenuItem.Click += (s, e) => Save();
            closeToolStripMenuItem.Click += (s, e) => CloseDocument();
            openToolStripMenuItem.Click += (s, e) => Open();
            newToolStripMenuItem.Click += (s, e) => New();
        }

        internal MainForm(string[] fileNames) : this()
        {
            foreach (var fileName in fileNames)
            {
                var form = new DocumentForm(fileName)
                {
                    MdiParent = this,
                };
                form.Show();
            }
        }

        void Save()
        {
            var form = ActiveMdiChild as DocumentForm;
            if (form == null)
            {
                return;
            }
            
            form.Save();
        }

        void CloseDocument()
        {
            var form = ActiveMdiChild as DocumentForm;
            if (form == null)
            {
                return;
            }
            
            form.Close();
        }

        void Open()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var fileName in dialog.FileNames)
            {
                var form = new DocumentForm(fileName)
                {
                    MdiParent = this,
                };
                form.Show();
            }
        }

        void New()
        {
            var form = new DocumentForm()
            {
                MdiParent = this,
            };
            form.Show();
        }
    }
}
