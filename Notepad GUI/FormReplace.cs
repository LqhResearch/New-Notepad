using Notepad_Library;
using System;
using System.Windows.Forms;

namespace Notepad_GUI
{
    public partial class FormReplace : Form
    {
        FindNextSearch fns = new FindNextSearch();
        public RichTextBox Editor;
        public EditNotepad editNotepad;

        public FindNextSearch Fns { get; internal set; }

        public FormReplace()
        {
            InitializeComponent();
        }

        #region Common function
        private void DisableButtons()
        {
            if (txtFindWhat.Text.Length == 0)
                btnFindNext.Enabled = btnReplace.Enabled = btnReplaceAll.Enabled = false;
            else
                btnFindNext.Enabled = btnReplace.Enabled = btnReplaceAll.Enabled = true;
        }

        public void UpdateSearchQuery()
        {
            fns.SearchString = txtFindWhat.Text;
            fns.Direction = rdbUp.Checked ? "UP" : "DOWN";
            fns.MatchCase = ckbMatchCase.Checked;
            fns.Content = Editor.Text;
            fns.Position = Editor.SelectionStart;
        }
        #endregion
        #region Default setting
        private void FormReplace_Load(object sender, EventArgs e)
        {
            DisableButtons();
            rdbDown.Checked = true;
        }


        #endregion
        #region Button
        private void btnFindNext_Click(object sender, EventArgs e)
        {
            UpdateSearchQuery();
            FindNextResult result = editNotepad.FindNext(fns);
            if (result.SearchStatus)
                this.Editor.Select(result.SelectionStart, txtFindWhat.Text.Length);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (Editor.SelectionLength == 0)
                btnFindNext.PerformClick();
            else
                Editor.SelectedText = txtReplaceWith.Text;
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            Editor.Text = Editor.Text.Replace(txtFindWhat.Text, txtReplaceWith.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        private void txtFindWhat_TextChanged(object sender, EventArgs e)
        {
            DisableButtons();
        }
    }
}
