
namespace Notepad_GUI
{
    partial class FormFind
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFind));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.ckbMatchCase = new System.Windows.Forms.CheckBox();
            this.txtFindWhat = new System.Windows.Forms.TextBox();
            this.lblFindWhat = new System.Windows.Forms.Label();
            this.grbDirection = new System.Windows.Forms.GroupBox();
            this.rdbDown = new System.Windows.Forms.RadioButton();
            this.rdbUp = new System.Windows.Forms.RadioButton();
            this.grbDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(287, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(287, 12);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(75, 23);
            this.btnFindNext.TabIndex = 12;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // ckbMatchCase
            // 
            this.ckbMatchCase.AutoSize = true;
            this.ckbMatchCase.Location = new System.Drawing.Point(15, 69);
            this.ckbMatchCase.Name = "ckbMatchCase";
            this.ckbMatchCase.Size = new System.Drawing.Size(83, 17);
            this.ckbMatchCase.TabIndex = 8;
            this.ckbMatchCase.Text = "Match case";
            this.ckbMatchCase.UseVisualStyleBackColor = true;
            this.ckbMatchCase.CheckedChanged += new System.EventHandler(this.ckbMatchCase_CheckedChanged);
            // 
            // txtFindWhat
            // 
            this.txtFindWhat.Location = new System.Drawing.Point(93, 12);
            this.txtFindWhat.Name = "txtFindWhat";
            this.txtFindWhat.Size = new System.Drawing.Size(188, 22);
            this.txtFindWhat.TabIndex = 7;
            this.txtFindWhat.TextChanged += new System.EventHandler(this.txtFindWhat_TextChanged);
            // 
            // lblFindWhat
            // 
            this.lblFindWhat.AutoSize = true;
            this.lblFindWhat.Location = new System.Drawing.Point(12, 17);
            this.lblFindWhat.Name = "lblFindWhat";
            this.lblFindWhat.Size = new System.Drawing.Size(65, 13);
            this.lblFindWhat.TabIndex = 5;
            this.lblFindWhat.Text = "Find what: ";
            // 
            // grbDirection
            // 
            this.grbDirection.Controls.Add(this.rdbDown);
            this.grbDirection.Controls.Add(this.rdbUp);
            this.grbDirection.Location = new System.Drawing.Point(169, 40);
            this.grbDirection.Name = "grbDirection";
            this.grbDirection.Size = new System.Drawing.Size(112, 46);
            this.grbDirection.TabIndex = 13;
            this.grbDirection.TabStop = false;
            this.grbDirection.Text = "Direction";
            // 
            // rdbDown
            // 
            this.rdbDown.AutoSize = true;
            this.rdbDown.Location = new System.Drawing.Point(52, 21);
            this.rdbDown.Name = "rdbDown";
            this.rdbDown.Size = new System.Drawing.Size(56, 17);
            this.rdbDown.TabIndex = 0;
            this.rdbDown.TabStop = true;
            this.rdbDown.Text = "Down";
            this.rdbDown.UseVisualStyleBackColor = true;
            // 
            // rdbUp
            // 
            this.rdbUp.AutoSize = true;
            this.rdbUp.Location = new System.Drawing.Point(6, 21);
            this.rdbUp.Name = "rdbUp";
            this.rdbUp.Size = new System.Drawing.Size(40, 17);
            this.rdbUp.TabIndex = 0;
            this.rdbUp.TabStop = true;
            this.rdbUp.Text = "Up";
            this.rdbUp.UseVisualStyleBackColor = true;
            this.rdbUp.CheckedChanged += new System.EventHandler(this.rdbUp_CheckedChanged);
            // 
            // FormFind
            // 
            this.AcceptButton = this.btnFindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 96);
            this.Controls.Add(this.grbDirection);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.ckbMatchCase);
            this.Controls.Add(this.txtFindWhat);
            this.Controls.Add(this.lblFindWhat);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormFind";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFind_FormClosing);
            this.grbDirection.ResumeLayout(false);
            this.grbDirection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.CheckBox ckbMatchCase;
        private System.Windows.Forms.TextBox txtFindWhat;
        private System.Windows.Forms.Label lblFindWhat;
        private System.Windows.Forms.GroupBox grbDirection;
        private System.Windows.Forms.RadioButton rdbDown;
        private System.Windows.Forms.RadioButton rdbUp;
    }
}