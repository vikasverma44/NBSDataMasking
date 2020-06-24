namespace DataMaskingConfigurator
{
    partial class frmSourceAddEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.txtRecordTypes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFileExtensions = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSaveClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxFileType = new System.Windows.Forms.ComboBox();
            this.txtSubType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(41, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuration Name :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtConfigName
            // 
            this.txtConfigName.Location = new System.Drawing.Point(237, 55);
            this.txtConfigName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtConfigName.MaxLength = 30;
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(441, 22);
            this.txtConfigName.TabIndex = 0;
            this.txtConfigName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfigName_KeyPress);
            // 
            // txtRecordTypes
            // 
            this.txtRecordTypes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRecordTypes.Location = new System.Drawing.Point(237, 212);
            this.txtRecordTypes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRecordTypes.MaxLength = 30;
            this.txtRecordTypes.Name = "txtRecordTypes";
            this.txtRecordTypes.Size = new System.Drawing.Size(441, 22);
            this.txtRecordTypes.TabIndex = 4;
          //  this.txtRecordTypes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecordTypes_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(41, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Record Type(s) :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFileExtensions
            // 
            this.txtFileExtensions.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFileExtensions.Location = new System.Drawing.Point(237, 95);
            this.txtFileExtensions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFileExtensions.MaxLength = 30;
            this.txtFileExtensions.Name = "txtFileExtensions";
            this.txtFileExtensions.Size = new System.Drawing.Size(441, 22);
            this.txtFileExtensions.TabIndex = 1;
            this.txtFileExtensions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFileExtensions_KeyPress);
            this.txtFileExtensions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFileExtensions_KeyUp);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(41, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "File Extension Target(s) :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveClose.Location = new System.Drawing.Point(385, 283);
            this.btnSaveClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(140, 44);
            this.btnSaveClose.TabIndex = 6;
            this.btnSaveClose.Text = "Save && Close";
            this.btnSaveClose.UseVisualStyleBackColor = true;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(536, 283);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 44);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(235, 283);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 44);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 350);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(664, 128);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(667, 30);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Message";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(41, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "File Type :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbxFileType
            // 
            this.cmbxFileType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbxFileType.FormattingEnabled = true;
            this.cmbxFileType.Location = new System.Drawing.Point(237, 169);
            this.cmbxFileType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbxFileType.Name = "cmbxFileType";
            this.cmbxFileType.Size = new System.Drawing.Size(441, 24);
            this.cmbxFileType.TabIndex = 3;
            this.cmbxFileType.SelectedIndexChanged += new System.EventHandler(this.cmbxFileType_SelectedIndexChanged);
            this.cmbxFileType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbxFileType_KeyDown);
            this.cmbxFileType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbxFileType_KeyPress);
            // 
            // txtSubType
            // 
            this.txtSubType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSubType.Location = new System.Drawing.Point(237, 132);
            this.txtSubType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSubType.MaxLength = 50;
            this.txtSubType.Name = "txtSubType";
            this.txtSubType.Size = new System.Drawing.Size(441, 22);
            this.txtSubType.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(41, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Sub Type :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmSourceAddEdit
            // 
            this.AcceptButton = this.btnSaveClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(747, 533);
            this.ControlBox = false;
            this.Controls.Add(this.txtSubType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbxFileType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.txtFileExtensions);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRecordTypes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtConfigName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(749, 488);
            this.Name = "frmSourceAddEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Masking Type Definition";
            this.Load += new System.EventHandler(this.frmAddEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConfigName;
        private System.Windows.Forms.TextBox txtRecordTypes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFileExtensions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxFileType;
        private System.Windows.Forms.TextBox txtSubType;
        private System.Windows.Forms.Label label5;
    }
}