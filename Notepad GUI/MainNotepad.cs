using Microsoft.VisualBasic;
using Notepad_Library;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad_GUI
{
    public partial class MainNotepad : Form
    {
        FileNotepad fileNotepad;
        EditNotepad editNotepad;
        FormFind frmFind;
        FormReplace frmReplace;

        public EditNotepad EditNote
        {
            get { return editNotepad; }
            set { editNotepad = value; }
        }

        public MainNotepad()
        {
            InitializeComponent();
            fileNotepad = new FileNotepad();
            this.editNotepad = new EditNotepad();
            fileNotepad.InitializeNewFile();
            this.Text = fileNotepad.FileName + " - Notepad";

            rtxtDocument.HideSelection = false;
        }

        #region Default setting
        private void MainNotepad_Load(object sender, EventArgs e)
        {
            rtxtDocument.WordWrap = wordWrapToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Checked = !wordWrapToolStripMenuItem.Checked;
            if (statusBarToolStripMenuItem.Enabled)
                statusBarToolStripMenuItem.Checked = true;
            statusContent.Visible = statusBarToolStripMenuItem.Checked;

            // set line number
            rtxtLineNumber.Font = rtxtDocument.Font;
            rtxtDocument.Select();
            AddLineNumbers();
        }
        #endregion
        #region Common function
        private void UpdateView()
        {
            this.Text = !fileNotepad.IsFileSaved ? fileNotepad.FileName + "* - Notepad" : fileNotepad.FileName + " - Notepad";
        }
        private void UpdateStatus()
        {
            int pos = rtxtDocument.SelectionStart;
            int line = rtxtDocument.GetLineFromCharIndex(pos) + 1;
            int col = pos - rtxtDocument.GetFirstCharIndexOfCurrentLine() + 1;

            statusLocation.Text = "Ln " + line + ", Col " + col;
        }
        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Text documents|*.txt| All files|*.*",
                ValidateNames = true
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fileNotepad.SaveFile(sfd.FileName, rtxtDocument.Lines);
                UpdateView();
            }
        }
        #endregion
        #region File tools
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileNotepad.IsFileSaved)
            {
                rtxtDocument.Text = "";
                fileNotepad.InitializeNewFile();
                UpdateView();
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you need save changes to " + fileNotepad.FileName, "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (fileNotepad.FileName.Contains("Untitled.txt"))
                    {
                        SaveFileDialog sfd = new SaveFileDialog()
                        {
                            Filter = "Text documents|*.txt| All files|*.*",
                            ValidateNames = true
                        };
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            // File is to be saved for the first time.
                            fileNotepad.SaveFile(sfd.FileName, rtxtDocument.Lines);
                            UpdateView();
                        }
                        else
                        {
                            // File already saved. So use name from notepad
                            fileNotepad.SaveFile(fileNotepad.FileLocation, rtxtDocument.Lines);
                            UpdateView();
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    // When user select not to use, initialize a new file
                    rtxtDocument.Text = "";
                    fileNotepad.InitializeNewFile();
                }
            }
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Notepad GUI.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "New window error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Text documents|*.txt| All files|*.*",
                ValidateNames = true,
                Multiselect = false,
                InitialDirectory = "D:",
                Title = "Open File"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                rtxtDocument.TextChanged -= rtxtDocument_TextChanged;
                rtxtDocument.Text = fileNotepad.OpenFile(ofd.FileName);
                rtxtDocument.TextChanged += rtxtDocument_TextChanged;
                UpdateView();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fileNotepad.IsFileSaved)
            {
                if (this.Text.Contains("Untitled.txt"))
                    SaveFile();
                else
                {
                    fileNotepad.SaveFile(fileNotepad.FileLocation, rtxtDocument.Lines);
                    UpdateView();
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(rtxtDocument.Text, rtxtDocument.Font, Brushes.Black, 12, 10);
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDocument.Print();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion
        #region Edit tools
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem.Enabled = rtxtDocument.SelectedText.Length > 0 ? true : false;
            copyToolStripMenuItem.Enabled = rtxtDocument.SelectedText.Length > 0 ? true : false;
            deleteToolStripMenuItem.Enabled = rtxtDocument.SelectedText.Length > 0 ? true : false;
            pasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text);
        }

        private void editToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editToolStripMenuItem_Click(sender, e);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtDocument.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtDocument.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtDocument.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                rtxtDocument.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtDocument.Text = rtxtDocument.Text.Remove(rtxtDocument.SelectionStart, rtxtDocument.SelectionLength);
            rtxtDocument.SelectedText = "";
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmFind == null)
            {
                frmFind = new FormFind(this);
                frmFind.Editor = rtxtDocument;
            }
            frmFind.Show();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmReplace == null)
            {
                frmReplace = new FormReplace();
                frmReplace.Editor = rtxtDocument;
                frmReplace.editNotepad = editNotepad;
            }
            frmReplace.Show();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Line number", "Go to", "1");
            try
            {
                int line = Convert.ToInt32(input);
                if (line > rtxtDocument.Lines.Length)
                    MessageBox.Show("Total lines in the file is " + rtxtDocument.Lines.Length);
                else
                {
                    string[] lines = rtxtDocument.Lines;
                    int len = 0;
                    for (int i = 0; i < line - 1; i++)
                        len = len + lines[i].Length + 1;
                    rtxtDocument.Focus();
                    rtxtDocument.Select(len, 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter a valid Integer", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtDocument.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtDocument.Text = rtxtDocument.Text.Insert(rtxtDocument.SelectionStart, EditNote.DateTime_Now());
        }

        #endregion
        #region Format tools
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtDocument.WordWrap = wordWrapToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Enabled = !wordWrapToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Checked = true;
            statusContent.Visible = statusBarToolStripMenuItem.Enabled;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog()
            {
                ShowColor = true,
                Font = rtxtDocument.Font,
                Color = rtxtDocument.ForeColor
            };
            if (font.ShowDialog() == DialogResult.OK)
            {
                rtxtDocument.Font = font.Font;
                rtxtDocument.ForeColor = font.Color;
            }
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                rtxtDocument.ForeColor = cd.Color;
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                rtxtDocument.BackColor = cd.Color;
        }
        #endregion
        #region View tools
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusContent.Visible = statusBarToolStripMenuItem.Checked;
        }
        #endregion
        #region About tools
        private void abountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.Show();
        }

        #endregion
        #region Menu tool strip
        private void newFileToolStripButton_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(null, null);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(null, null);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(null, null);
        }

        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(null, null);
        }

        private void pageSetuptoolStripButton_Click(object sender, EventArgs e)
        {
            pageSetupToolStripMenuItem_Click(null, null);
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            printToolStripMenuItem_Click(null, null);
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(null, null);
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(null, null);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(null, null);
        }

        private void boldToolStripButton_Click(object sender, EventArgs e)
        {
            rtxtDocument.SelectionFont = new Font(rtxtDocument.SelectionFont, rtxtDocument.SelectionFont.Style | FontStyle.Bold);
        }

        private void italicToolStripButton_Click(object sender, EventArgs e)
        {
            rtxtDocument.SelectionFont = new Font(rtxtDocument.SelectionFont, rtxtDocument.SelectionFont.Style | FontStyle.Italic);
        }

        private void underlineToolStripButton_Click(object sender, EventArgs e)
        {
            rtxtDocument.SelectionFont = new Font(rtxtDocument.SelectionFont, rtxtDocument.SelectionFont.Style | FontStyle.Underline);
        }

        private void strikeThroughToolStripButton_Click(object sender, EventArgs e)
        {
            rtxtDocument.SelectionFont = new Font(rtxtDocument.SelectionFont, rtxtDocument.SelectionFont.Style | FontStyle.Strikeout);
        }

        private void normalToolStripButton_Click(object sender, EventArgs e)
        {
            rtxtDocument.SelectionFont = new Font(rtxtDocument.Font, FontStyle.Regular);
        }

        private void textColorToolStripButton_Click(object sender, EventArgs e)
        {
            textColorToolStripMenuItem_Click(null, null);
        }

        private void backgroundToolStripButton_Click(object sender, EventArgs e)
        {
            backgroundColorToolStripMenuItem_Click(null, null);
        }

        private void selectedTextColorToolStripButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                rtxtDocument.SelectionColor = cd.Color;
        }

        private void highlighToolStripButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                rtxtDocument.SelectionBackColor = cd.Color;
        }
        #endregion

        private void rtxtDocument_TextChanged(object sender, EventArgs e)
        {
            fileNotepad.IsFileSaved = false;
            if (rtxtDocument.Text.Length > 0)
                findToolStripMenuItem.Enabled = replaceToolStripMenuItem.Enabled = true;
            else
                findToolStripMenuItem.Enabled = replaceToolStripMenuItem.Enabled = false;
            UpdateView();
            if (rtxtDocument.Text == "")
                AddLineNumbers();
        }

        private void rtxtDocument_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus();
            Point pt = rtxtDocument.GetPositionFromCharIndex(rtxtDocument.SelectionStart);
            if (pt.X == 1)
                AddLineNumbers();
        }

        #region Dark mode
        private void darkModeToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (darkModeToolStripButton.Checked)
            {
                rtxtDocument.ForeColor = Color.White;
                rtxtDocument.BackColor = Color.Black;

                rtxtLineNumber.ForeColor = Color.FromArgb(51, 153, 51);
                rtxtLineNumber.BackColor = Color.Black;

                pnlHeightLine.BackColor = Color.FromArgb(51, 153, 51);
            }
            else
            {
                rtxtDocument.ForeColor = Color.Black;
                rtxtDocument.BackColor = Color.White;

                rtxtLineNumber.ForeColor = Color.FromArgb(9, 161, 237);
                rtxtLineNumber.BackColor = Color.White;

                pnlHeightLine.BackColor = Color.FromArgb(9, 161, 237);
            }
        }
        #endregion
        #region Line number
        private int getWidth()
        {
            int width = 25;
            // Get total lines of rtxtDocument
            int line = rtxtDocument.Lines.Length;
            if (line <= 99)
                width = 30 + (int)rtxtDocument.Font.Size;
            else if (line <= 999)
                width = 40 + (int)rtxtDocument.Font.Size;
            else if (line <= 99999)
                width = 50 + (int)rtxtDocument.Font.Size;
            else
                width = 60 + (int)rtxtDocument.Font.Size;
            return width;
        }

        public void AddLineNumbers()
        {
            Point pt = new Point(0, 0);

            // Get first index & first line from rtxtDocument
            int firstIndex = rtxtDocument.GetCharIndexFromPosition(pt);
            int firstLine = rtxtDocument.GetLineFromCharIndex(firstIndex);

            // Set X & Y coordinates of Point pt to ClientRectangle width & height respectively
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;

            // get last index & last line from rtxtDocument
            int lastIndex = rtxtDocument.GetCharIndexFromPosition(pt);
            int lastLine = rtxtDocument.GetLineFromCharIndex(lastIndex);

            // set Center alignment to rtxtLineNumber
            rtxtLineNumber.SelectionAlignment = HorizontalAlignment.Center;

            // set rtxtLineNumber text to null & width to getWidth() function value
            rtxtLineNumber.Text = "";
            rtxtLineNumber.Width = getWidth();

            // now add each line number to rtxtLineNumber upto last line
            for (int i = firstLine; i <= lastLine + 1; i++)
            {
                if (i < 9)
                    rtxtLineNumber.Text += "0" + (i + 1) + ".\n";
                else
                    rtxtLineNumber.Text += (i + 1) + ".\n";
            }
            ResizeDocumentControl();
        }

        private void rtxtDocument_FontChanged(object sender, EventArgs e)
        {
            rtxtLineNumber.Font = rtxtDocument.Font;
            rtxtDocument.Select();
            AddLineNumbers();
        }

        private void rtxtDocument_VScroll(object sender, EventArgs e)
        {
            rtxtLineNumber.Text = "";
            AddLineNumbers();
            rtxtLineNumber.Invalidate();
        }

        private void rtxtLineNumber_MouseDown(object sender, MouseEventArgs e)
        {
            rtxtDocument.Select();
            rtxtLineNumber.DeselectAll();
        }
        #endregion
        #region Size rtxtDocument
        private void ResizeDocumentControl()
        {
            rtxtDocument.Location = new Point(rtxtLineNumber.Width + 6, rtxtDocument.Location.Y);
            rtxtDocument.Size = new Size(this.Width - rtxtLineNumber.Width - 24, this.Height - 117);

            rtxtLineNumber.Size = new Size(rtxtLineNumber.Width, rtxtDocument.Height);

            pnlHeightLine.Location = new Point(rtxtLineNumber.Width, pnlHeightLine.Location.Y);
            pnlHeightLine.Size = new Size(6, rtxtDocument.Height);
        }

        private void MainNotepad_SizeChanged(object sender, EventArgs e)
        {
            ResizeDocumentControl();
        }

        private void rtxtLineNumber_Resize(object sender, EventArgs e)
        {
            ResizeDocumentControl();
        }
        #endregion
        #region ContextMenuStrip
        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem_Click(null, null);
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(null, null);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(null, null);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(null, null);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(null, null);
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            selectAllToolStripMenuItem_Click(null, null);
        }
        #endregion
    }
}