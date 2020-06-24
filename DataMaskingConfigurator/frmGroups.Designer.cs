namespace DataMaskingConfigurator
{
    partial class frmGroups
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
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.rdbWorkflow = new System.Windows.Forms.RadioButton();
            this.rdbProduct = new System.Windows.Forms.RadioButton();
            this.chkListGroup = new System.Windows.Forms.CheckedListBox();
            this.txtGroupValue = new System.Windows.Forms.TextBox();
            this.lblGroupValue = new System.Windows.Forms.Label();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAddGroup.Location = new System.Drawing.Point(431, 188);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(206, 44);
            this.btnAddGroup.TabIndex = 5;
            this.btnAddGroup.Text = "Add Workflow";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(431, 288);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(206, 44);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(434, 347);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(283, 128);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.rdbWorkflow);
            this.groupBox.Controls.Add(this.rdbProduct);
            this.groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox.Location = new System.Drawing.Point(431, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(286, 84);
            this.groupBox.TabIndex = 13;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Select group to manage";
            // 
            // rdbWorkflow
            // 
            this.rdbWorkflow.AutoSize = true;
            this.rdbWorkflow.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbWorkflow.Location = new System.Drawing.Point(27, 39);
            this.rdbWorkflow.Name = "rdbWorkflow";
            this.rdbWorkflow.Size = new System.Drawing.Size(86, 21);
            this.rdbWorkflow.TabIndex = 0;
            this.rdbWorkflow.TabStop = true;
            this.rdbWorkflow.Text = "Workflow";
            this.rdbWorkflow.UseVisualStyleBackColor = true;
            this.rdbWorkflow.CheckedChanged += new System.EventHandler(this.rdbWorkflow_CheckedChanged);
            // 
            // rdbProduct
            // 
            this.rdbProduct.AutoSize = true;
            this.rdbProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbProduct.Location = new System.Drawing.Point(128, 39);
            this.rdbProduct.Name = "rdbProduct";
            this.rdbProduct.Size = new System.Drawing.Size(78, 21);
            this.rdbProduct.TabIndex = 0;
            this.rdbProduct.TabStop = true;
            this.rdbProduct.Text = "Product";
            this.rdbProduct.UseVisualStyleBackColor = true;
            this.rdbProduct.CheckedChanged += new System.EventHandler(this.rdbProduct_CheckedChanged);
            // 
            // chkListGroup
            // 
            this.chkListGroup.FormattingEnabled = true;
            this.chkListGroup.Location = new System.Drawing.Point(15, 12);
            this.chkListGroup.Name = "chkListGroup";
            this.chkListGroup.Size = new System.Drawing.Size(401, 463);
            this.chkListGroup.TabIndex = 15;
            this.chkListGroup.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListGroup_ItemCheck);
            // 
            // txtGroupValue
            // 
            this.txtGroupValue.Location = new System.Drawing.Point(431, 150);
            this.txtGroupValue.MaxLength = 20;
            this.txtGroupValue.Name = "txtGroupValue";
            this.txtGroupValue.Size = new System.Drawing.Size(286, 22);
            this.txtGroupValue.TabIndex = 16;
            // 
            // lblGroupValue
            // 
            this.lblGroupValue.AutoSize = true;
            this.lblGroupValue.Location = new System.Drawing.Point(431, 127);
            this.lblGroupValue.Name = "lblGroupValue";
            this.lblGroupValue.Size = new System.Drawing.Size(44, 17);
            this.lblGroupValue.TabIndex = 17;
            this.lblGroupValue.Text = "Value";
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDeleteGroup.Location = new System.Drawing.Point(431, 238);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(206, 44);
            this.btnDeleteGroup.TabIndex = 18;
            this.btnDeleteGroup.Text = "Delete Selected Product(s)";
            this.btnDeleteGroup.UseVisualStyleBackColor = true;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // frmGroups
            // 
            this.AcceptButton = this.btnAddGroup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(756, 525);
            this.ControlBox = false;
            this.Controls.Add(this.btnDeleteGroup);
            this.Controls.Add(this.lblGroupValue);
            this.Controls.Add(this.txtGroupValue);
            this.Controls.Add(this.chkListGroup);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(762, 531);
            this.Name = "frmGroups";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Masking Groups Management";
            this.Load += new System.EventHandler(this.frmGroups_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton rdbWorkflow;
        private System.Windows.Forms.RadioButton rdbProduct;
        private System.Windows.Forms.CheckedListBox chkListGroup;
        private System.Windows.Forms.TextBox txtGroupValue;
        private System.Windows.Forms.Label lblGroupValue;
        private System.Windows.Forms.Button btnDeleteGroup;
    }
}