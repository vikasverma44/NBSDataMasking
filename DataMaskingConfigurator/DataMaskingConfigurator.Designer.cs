namespace DataMaskingConfigurator
{
    partial class DataMaskingConfigurator
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
            this.btnCnfAdd = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.dgvSource = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvConfiguration = new System.Windows.Forms.DataGridView();
            this.btnCnfEdit = new System.Windows.Forms.Button();
            this.btnCnfDelete = new System.Windows.Forms.Button();
            this.btnSrcAdd = new System.Windows.Forms.Button();
            this.btnSrcEdit = new System.Windows.Forms.Button();
            this.btnSrcDelete = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnAcceptSaveChanges = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lnkShowAllSource = new System.Windows.Forms.LinkLabel();
            this.lblFilterSourceStatus = new System.Windows.Forms.Label();
            this.lblFilterConfigurationStatus = new System.Windows.Forms.Label();
            this.lnkShowAllConfiguration = new System.Windows.Forms.LinkLabel();
            this.btnMasterGroup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfiguration)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCnfAdd
            // 
            this.btnCnfAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCnfAdd.Location = new System.Drawing.Point(698, 174);
            this.btnCnfAdd.Name = "btnCnfAdd";
            this.btnCnfAdd.Size = new System.Drawing.Size(120, 44);
            this.btnCnfAdd.TabIndex = 3;
            this.btnCnfAdd.Text = "Add New";
            this.btnCnfAdd.UseVisualStyleBackColor = true;
            this.btnCnfAdd.Click += new System.EventHandler(this.btnCnfAdd_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Location = new System.Drawing.Point(920, 697);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(150, 44);
            this.btnQuit.TabIndex = 7;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // dgvSource
            // 
            this.dgvSource.AllowUserToAddRows = false;
            this.dgvSource.AllowUserToDeleteRows = false;
            this.dgvSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSource.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSource.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSource.Location = new System.Drawing.Point(12, 59);
            this.dgvSource.MultiSelect = false;
            this.dgvSource.Name = "dgvSource";
            this.dgvSource.ReadOnly = true;
            this.dgvSource.RowHeadersWidth = 51;
            this.dgvSource.RowTemplate.Height = 24;
            this.dgvSource.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSource.Size = new System.Drawing.Size(1058, 100);
            this.dgvSource.TabIndex = 1;
            this.dgvSource.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSource_CellMouseDown);
            this.dgvSource.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvSource_DataBindingComplete);
            this.dgvSource.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvSource_RowStateChanged);
            this.dgvSource.BindingContextChanged += new System.EventHandler(this.dgvSource_BindingContextChanged);
            this.dgvSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSource_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data Masking Type Definition";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Manage Master Data";
            // 
            // dgvConfiguration
            // 
            this.dgvConfiguration.AllowUserToAddRows = false;
            this.dgvConfiguration.AllowUserToDeleteRows = false;
            this.dgvConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConfiguration.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConfiguration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfiguration.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvConfiguration.Location = new System.Drawing.Point(12, 221);
            this.dgvConfiguration.MultiSelect = false;
            this.dgvConfiguration.Name = "dgvConfiguration";
            this.dgvConfiguration.ReadOnly = true;
            this.dgvConfiguration.RowHeadersWidth = 51;
            this.dgvConfiguration.RowTemplate.Height = 24;
            this.dgvConfiguration.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConfiguration.Size = new System.Drawing.Size(1058, 470);
            this.dgvConfiguration.TabIndex = 4;
            this.dgvConfiguration.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvConfiguration_CellMouseDown);
            this.dgvConfiguration.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvConfiguration_DataBindingComplete);
            this.dgvConfiguration.BindingContextChanged += new System.EventHandler(this.dgvConfiguration_BindingContextChanged);
            this.dgvConfiguration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvConfiguration_KeyDown);
            // 
            // btnCnfEdit
            // 
            this.btnCnfEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCnfEdit.Location = new System.Drawing.Point(824, 174);
            this.btnCnfEdit.Name = "btnCnfEdit";
            this.btnCnfEdit.Size = new System.Drawing.Size(120, 44);
            this.btnCnfEdit.TabIndex = 4;
            this.btnCnfEdit.Text = "Edit";
            this.btnCnfEdit.UseVisualStyleBackColor = true;
            this.btnCnfEdit.Click += new System.EventHandler(this.btnCnfEdit_Click);
            // 
            // btnCnfDelete
            // 
            this.btnCnfDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCnfDelete.Location = new System.Drawing.Point(950, 174);
            this.btnCnfDelete.Name = "btnCnfDelete";
            this.btnCnfDelete.Size = new System.Drawing.Size(120, 44);
            this.btnCnfDelete.TabIndex = 5;
            this.btnCnfDelete.Text = "Delete";
            this.btnCnfDelete.UseVisualStyleBackColor = true;
            this.btnCnfDelete.Click += new System.EventHandler(this.btnCnfDelete_Click);
            // 
            // btnSrcAdd
            // 
            this.btnSrcAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSrcAdd.Location = new System.Drawing.Point(698, 12);
            this.btnSrcAdd.Name = "btnSrcAdd";
            this.btnSrcAdd.Size = new System.Drawing.Size(120, 44);
            this.btnSrcAdd.TabIndex = 0;
            this.btnSrcAdd.Text = "Add New";
            this.btnSrcAdd.UseVisualStyleBackColor = true;
            this.btnSrcAdd.Click += new System.EventHandler(this.btnSrcAdd_Click);
            // 
            // btnSrcEdit
            // 
            this.btnSrcEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSrcEdit.Location = new System.Drawing.Point(824, 12);
            this.btnSrcEdit.Name = "btnSrcEdit";
            this.btnSrcEdit.Size = new System.Drawing.Size(120, 44);
            this.btnSrcEdit.TabIndex = 1;
            this.btnSrcEdit.Text = "Edit";
            this.btnSrcEdit.UseVisualStyleBackColor = true;
            this.btnSrcEdit.Click += new System.EventHandler(this.btnSrcEdit_Click);
            // 
            // btnSrcDelete
            // 
            this.btnSrcDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSrcDelete.Location = new System.Drawing.Point(950, 12);
            this.btnSrcDelete.Name = "btnSrcDelete";
            this.btnSrcDelete.Size = new System.Drawing.Size(120, 44);
            this.btnSrcDelete.TabIndex = 2;
            this.btnSrcDelete.Text = "Delete";
            this.btnSrcDelete.UseVisualStyleBackColor = true;
            this.btnSrcDelete.Click += new System.EventHandler(this.btnSrcDelete_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAbout.Location = new System.Drawing.Point(12, 697);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(150, 44);
            this.btnAbout.TabIndex = 9;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnAcceptSaveChanges
            // 
            this.btnAcceptSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAcceptSaveChanges.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAcceptSaveChanges.Location = new System.Drawing.Point(738, 697);
            this.btnAcceptSaveChanges.Name = "btnAcceptSaveChanges";
            this.btnAcceptSaveChanges.Size = new System.Drawing.Size(176, 44);
            this.btnAcceptSaveChanges.TabIndex = 6;
            this.btnAcceptSaveChanges.Text = "Accept && Save Changes";
            this.btnAcceptSaveChanges.UseVisualStyleBackColor = true;
            this.btnAcceptSaveChanges.Click += new System.EventHandler(this.btnAcceptSaveChanges_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExport.Location = new System.Drawing.Point(168, 697);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(176, 44);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export Configuration";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lnkShowAllSource
            // 
            this.lnkShowAllSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkShowAllSource.AutoSize = true;
            this.lnkShowAllSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkShowAllSource.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkShowAllSource.Location = new System.Drawing.Point(623, 33);
            this.lnkShowAllSource.Name = "lnkShowAllSource";
            this.lnkShowAllSource.Size = new System.Drawing.Size(69, 17);
            this.lnkShowAllSource.TabIndex = 10;
            this.lnkShowAllSource.TabStop = true;
            this.lnkShowAllSource.Text = "Show &All";
            this.lnkShowAllSource.Click += new System.EventHandler(this.lnkShowAllSource_Click);
            // 
            // lblFilterSourceStatus
            // 
            this.lblFilterSourceStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilterSourceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterSourceStatus.Location = new System.Drawing.Point(302, 33);
            this.lblFilterSourceStatus.Name = "lblFilterSourceStatus";
            this.lblFilterSourceStatus.Size = new System.Drawing.Size(315, 23);
            this.lblFilterSourceStatus.TabIndex = 11;
            this.lblFilterSourceStatus.Text = "lblFilterSourceStatus";
            this.lblFilterSourceStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFilterConfigurationStatus
            // 
            this.lblFilterConfigurationStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilterConfigurationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterConfigurationStatus.Location = new System.Drawing.Point(302, 195);
            this.lblFilterConfigurationStatus.Name = "lblFilterConfigurationStatus";
            this.lblFilterConfigurationStatus.Size = new System.Drawing.Size(315, 23);
            this.lblFilterConfigurationStatus.TabIndex = 13;
            this.lblFilterConfigurationStatus.Text = "lblFilterConfigurationStatus";
            this.lblFilterConfigurationStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lnkShowAllConfiguration
            // 
            this.lnkShowAllConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkShowAllConfiguration.AutoSize = true;
            this.lnkShowAllConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkShowAllConfiguration.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkShowAllConfiguration.Location = new System.Drawing.Point(623, 195);
            this.lnkShowAllConfiguration.Name = "lnkShowAllConfiguration";
            this.lnkShowAllConfiguration.Size = new System.Drawing.Size(69, 17);
            this.lnkShowAllConfiguration.TabIndex = 12;
            this.lnkShowAllConfiguration.TabStop = true;
            this.lnkShowAllConfiguration.Text = "Show &All";
            this.lnkShowAllConfiguration.Click += new System.EventHandler(this.lnkShowAllConfiguration_Click);
            // 
            // btnMasterGroup
            // 
            this.btnMasterGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMasterGroup.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMasterGroup.Location = new System.Drawing.Point(350, 697);
            this.btnMasterGroup.Name = "btnMasterGroup";
            this.btnMasterGroup.Size = new System.Drawing.Size(176, 44);
            this.btnMasterGroup.TabIndex = 14;
            this.btnMasterGroup.Text = "Manage Master Data";
            this.btnMasterGroup.UseVisualStyleBackColor = true;
            this.btnMasterGroup.Click += new System.EventHandler(this.btnMasterGroup_Click);
            // 
            // DataMaskingConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnQuit;
            this.ClientSize = new System.Drawing.Size(1082, 753);
            this.Controls.Add(this.btnMasterGroup);
            this.Controls.Add(this.lblFilterConfigurationStatus);
            this.Controls.Add(this.lnkShowAllConfiguration);
            this.Controls.Add(this.lblFilterSourceStatus);
            this.Controls.Add(this.lnkShowAllSource);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnAcceptSaveChanges);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvConfiguration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSource);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnSrcDelete);
            this.Controls.Add(this.btnSrcEdit);
            this.Controls.Add(this.btnCnfDelete);
            this.Controls.Add(this.btnSrcAdd);
            this.Controls.Add(this.btnCnfEdit);
            this.Controls.Add(this.btnCnfAdd);
            this.MinimumSize = new System.Drawing.Size(1100, 800);
            this.Name = "DataMaskingConfigurator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Masking Configurator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DataMaskingConfigurator_Load);
            this.Resize += new System.EventHandler(this.DataMaskingConfigurator_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfiguration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCnfAdd;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.DataGridView dgvSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvConfiguration;
        private System.Windows.Forms.Button btnCnfEdit;
        private System.Windows.Forms.Button btnCnfDelete;
        private System.Windows.Forms.Button btnSrcAdd;
        private System.Windows.Forms.Button btnSrcEdit;
        private System.Windows.Forms.Button btnSrcDelete;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnAcceptSaveChanges;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.LinkLabel lnkShowAllSource;
        private System.Windows.Forms.Label lblFilterSourceStatus;
        private System.Windows.Forms.Label lblFilterConfigurationStatus;
        private System.Windows.Forms.LinkLabel lnkShowAllConfiguration;
        private System.Windows.Forms.Button btnMasterGroup;
    }
}

