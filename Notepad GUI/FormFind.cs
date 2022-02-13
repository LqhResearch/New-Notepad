using Notepad_Library;
using System;
using System.Windows.Forms;

namespace Notepad_GUI
{
    public partial class FormFind : Form
    {
        MainNotepad mainNotepad;
        EditNotepad editNotepad;
        FindNextSearch fns = new FindNextSearch();

        public RichTextBox Editor { get; internal set; }
        public FindNextSearch Fns { get; set; }

        public FormFind(MainNotepad mainNotepad)
        {
            InitializeComponent();
            this.mainNotepad = mainNotepad;
            rdbDown.Checked = true;
            btnFindNext.Enabled = false;
            editNotepad = mainNotepad.EditNote;
            fns.Success = false;
        }

        #region Common function
        public void UpdateSearchQuery()
        {
            fns.SearchString = txtFindWhat.Text;
            fns.Direction = rdbUp.Checked ? "UP" : "DOWN";
            fns.MatchCase = ckbMatchCase.Checked;
            fns.Content = Editor.Text;
            fns.Position = Editor.SelectionStart;
        }
        #endregion
        #region Button
        private void btnFindNext_Click(object sender, EventArgs e)
        {
            UpdateSearchQuery();
            FindNextResult result = editNotepad.FindNext(fns);
            if (result.SearchStatus)
                Editor.Select(result.SelectionStart, txtFindWhat.Text.Length);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void txtFindWhat_TextChanged(object sender, EventArgs e)
        {
            btnFindNext.Enabled = (txtFindWhat.Text.Length > 0) ? true : false;
            UpdateSearchQuery();
        }

        private void ckbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSearchQuery();
        }

        private void rdbUp_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSearchQuery();
        }

        private void FormFind_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
