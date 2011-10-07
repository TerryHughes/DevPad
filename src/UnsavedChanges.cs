namespace DevPad
{
    using System;
    using System.Windows.Forms;

    class UnsavedChanges
    {
        readonly IWin32Window window;
        readonly Action action;
        bool unsavedChangesHaveOccurred;

        internal UnsavedChanges(IWin32Window window, Action action)
        {
            this.window = window;
            this.action = action;
        }

        bool UnsavedChangesHaveNotOccurred
        {
            get { return !unsavedChangesHaveOccurred; }
        }

        internal void HaveOccurred()
        {
            unsavedChangesHaveOccurred = true;
        }

        internal void Reset()
        {
            unsavedChangesHaveOccurred = false;
        }

        internal bool WereNotHandled()
        {
            return !UnsavedChangesWereHandled();
        }

        bool UnsavedChangesWereHandled()
        {
            if (UnsavedChangesHaveNotOccurred)
            {
                return true;
            }

            var result = MessageBox.Show(window, "Do you want to save changes?", "DevPad", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Cancel)
            {
                return false;
            }

            if (result == DialogResult.Yes)
            {
                action();

                return UnsavedChangesHaveNotOccurred;
            }

            return true;
        }
    }
}
