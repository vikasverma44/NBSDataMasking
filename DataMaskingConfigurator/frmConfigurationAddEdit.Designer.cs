namespace DataMaskingConfigurator
{
    partial class frmConfigurationAddEdit
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
            this.txtSettingName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFieldLength = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStartPos = new System.Windows.Forms.TextBox();
            this.txtDelimitedColumns = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFileExtnType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbxWorkflow = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbxProduct = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRowNo = new System.Windows.Forms.TextBox();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(46, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuration Name *:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtConfigName
            // 
            this.txtConfigName.Enabled = false;
            this.txtConfigName.Location = new System.Drawing.Point(267, 69);
            this.txtConfigName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtConfigName.MaxLength = 30;
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(497, 26);
            this.txtConfigName.TabIndex = 0;
            this.txtConfigName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfigName_KeyPress);
            // 
            // txtRecordTypes
            // 
            this.txtRecordTypes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRecordTypes.Enabled = false;
            this.txtRecordTypes.Location = new System.Drawing.Point(267, 138);
            this.txtRecordTypes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRecordTypes.MaxLength = 30;
            this.txtRecordTypes.Name = "txtRecordTypes";
            this.txtRecordTypes.Size = new System.Drawing.Size(496, 26);
            this.txtRecordTypes.TabIndex = 2;
            this.txtRecordTypes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecordTypes_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(46, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Record Type(s) *:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFileExtensions
            // 
            this.txtFileExtensions.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFileExtensions.Enabled = false;
            this.txtFileExtensions.Location = new System.Drawing.Point(267, 102);
            this.txtFileExtensions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFileExtensions.MaxLength = 30;
            this.txtFileExtensions.Name = "txtFileExtensions";
            this.txtFileExtensions.Size = new System.Drawing.Size(496, 26);
            this.txtFileExtensions.TabIndex = 1;
            this.txtFileExtensions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFileExtensions_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(46, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "File Extension Target(s) *:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveClose.Location = new System.Drawing.Point(434, 642);
            this.btnSaveClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(158, 55);
            this.btnSaveClose.TabIndex = 16;
            this.btnSaveClose.Text = "Save && Close";
            this.btnSaveClose.UseVisualStyleBackColor = true;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(609, 642);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(158, 55);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(270, 642);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(158, 55);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(46, 707);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(747, 130);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(14, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(750, 38);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Message";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(46, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Setting Name *:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSettingName
            // 
            this.txtSettingName.Location = new System.Drawing.Point(267, 212);
            this.txtSettingName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSettingName.MaxLength = 30;
            this.txtSettingName.Name = "txtSettingName";
            this.txtSettingName.Size = new System.Drawing.Size(496, 26);
            this.txtSettingName.TabIndex = 4;
            this.txtSettingName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSettingName_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(46, 361);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "Field Length *:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFieldLength
            // 
            this.txtFieldLength.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFieldLength.Location = new System.Drawing.Point(267, 358);
            this.txtFieldLength.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFieldLength.MaxLength = 3;
            this.txtFieldLength.Name = "txtFieldLength";
            this.txtFieldLength.Size = new System.Drawing.Size(496, 26);
            this.txtFieldLength.TabIndex = 8;
            this.txtFieldLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFieldLength_KeyPress);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(46, 328);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 22);
            this.label6.TabIndex = 6;
            this.label6.Text = "Start Position *:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartPos
            // 
            this.txtStartPos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartPos.Location = new System.Drawing.Point(267, 322);
            this.txtStartPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStartPos.MaxLength = 6;
            this.txtStartPos.Name = "txtStartPos";
            this.txtStartPos.Size = new System.Drawing.Size(496, 26);
            this.txtStartPos.TabIndex = 7;
            this.txtStartPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartPos_KeyPress);
            // 
            // txtDelimitedColumns
            // 
            this.txtDelimitedColumns.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDelimitedColumns.Location = new System.Drawing.Point(267, 395);
            this.txtDelimitedColumns.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDelimitedColumns.MaxLength = 100;
            this.txtDelimitedColumns.Name = "txtDelimitedColumns";
            this.txtDelimitedColumns.Size = new System.Drawing.Size(496, 26);
            this.txtDelimitedColumns.TabIndex = 9;
            this.txtDelimitedColumns.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDelimitedColumns_KeyPress);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(44, 398);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(213, 22);
            this.label7.TabIndex = 13;
            this.label7.Text = "Delimited Column(s) :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFileExtnType
            // 
            this.txtFileExtnType.Enabled = false;
            this.txtFileExtnType.Location = new System.Drawing.Point(267, 174);
            this.txtFileExtnType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFileExtnType.MaxLength = 30;
            this.txtFileExtnType.Name = "txtFileExtnType";
            this.txtFileExtnType.Size = new System.Drawing.Size(496, 26);
            this.txtFileExtnType.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(46, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(213, 22);
            this.label8.TabIndex = 16;
            this.label8.Text = "File Extension Type* :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbxWorkflow
            // 
            this.cmbxWorkflow.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbxWorkflow.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbxWorkflow.FormattingEnabled = true;
            this.cmbxWorkflow.Location = new System.Drawing.Point(266, 551);
            this.cmbxWorkflow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbxWorkflow.Name = "cmbxWorkflow";
            this.cmbxWorkflow.Size = new System.Drawing.Size(494, 28);
            this.cmbxWorkflow.TabIndex = 12;
            this.cmbxWorkflow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbxWorkflow_KeyDown);
            this.cmbxWorkflow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbxWorkflow_KeyPress);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(45, 554);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 22);
            this.label9.TabIndex = 18;
            this.label9.Text = "Workflow :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbxProduct
            // 
            this.cmbxProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbxProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbxProduct.FormattingEnabled = true;
            this.cmbxProduct.Location = new System.Drawing.Point(266, 592);
            this.cmbxProduct.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbxProduct.Name = "cmbxProduct";
            this.cmbxProduct.Size = new System.Drawing.Size(494, 28);
            this.cmbxProduct.TabIndex = 14;
            this.cmbxProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbxProduct_KeyDown);
            this.cmbxProduct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbxProduct_KeyPress);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(45, 595);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(213, 22);
            this.label10.TabIndex = 20;
            this.label10.Text = "Product :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(267, 466);
            this.txtFieldName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFieldName.MaxLength = 100;
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(496, 26);
            this.txtFieldName.TabIndex = 11;
            this.txtFieldName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFieldName_KeyPress);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(44, 469);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(213, 22);
            this.label11.TabIndex = 24;
            this.label11.Text = "Field Path :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(267, 431);
            this.txtPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPath.MaxLength = 500;
            this.txtPath.Multiline = true;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(496, 29);
            this.txtPath.TabIndex = 10;
            this.txtPath.WordWrap = false;
            this.txtPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPath_KeyPress);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(44, 434);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(213, 22);
            this.label12.TabIndex = 21;
            this.label12.Text = "Root Path :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(267, 506);
            this.txtVersion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtVersion.MaxLength = 100;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(496, 26);
            this.txtVersion.TabIndex = 12;
            this.txtVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVersion_KeyPress);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(44, 509);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(213, 22);
            this.label13.TabIndex = 26;
            this.label13.Text = "Version :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(46, 291);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(213, 22);
            this.label14.TabIndex = 28;
            this.label14.Text = "Row Number. *:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRowNo
            // 
            this.txtRowNo.Location = new System.Drawing.Point(267, 288);
            this.txtRowNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRowNo.MaxLength = 25;
            this.txtRowNo.Name = "txtRowNo";
            this.txtRowNo.Size = new System.Drawing.Size(496, 26);
            this.txtRowNo.TabIndex = 6;
            this.txtRowNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRowNo_KeyPress);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(267, 248);
            this.txtPageNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new System.Drawing.Size(496, 26);
            this.txtPageNumber.TabIndex = 5;
            this.txtPageNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPageNumber_KeyPress);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(46, 254);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(213, 22);
            this.label15.TabIndex = 31;
            this.label15.Text = "Page Number *:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmConfigurationAddEdit
            // 
            this.AcceptButton = this.btnSaveClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(927, 888);
            this.ControlBox = false;
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtPageNumber);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtRowNo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbxProduct);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbxWorkflow);
            this.Controls.Add(this.txtFileExtnType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDelimitedColumns);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.txtStartPos);
            this.Controls.Add(this.txtFileExtensions);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFieldLength);
            this.Controls.Add(this.txtRecordTypes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSettingName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConfigName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(840, 728);
            this.Name = "frmConfigurationAddEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Master Data ";
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
        private System.Windows.Forms.TextBox txtSettingName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFieldLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStartPos;
        private System.Windows.Forms.TextBox txtDelimitedColumns;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFileExtnType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbxWorkflow;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbxProduct;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRowNo;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.Label label15;
    }
}