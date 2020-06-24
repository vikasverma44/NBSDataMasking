using DataGridViewAutoFilter;
using DataMaskingConfigurator.DataModel;
using NetCommonUtilities;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;
using static NetCommonUtilities.Enums;

namespace DataMaskingConfigurator
{
    public partial class DataMaskingConfigurator : Form
    {
        private readonly string MaskingConfigSourcePath;
        private readonly string MaskingSourceFileName;
        private readonly string MaskingSourceSchemaFileName;
        private MaskingSource maskingSource;
        private DataTable dtSource;
        private DataTable dtConfiguration;
        private DataTable dtWorkflow;
        private DataTable dtProduct;
        DataTable dtConfigurationCopy;
        DataTable dtSourceCopy;
        public int ConfigurationID;
        public int SourceID;
        #region Form Constructor & Events

        public DataMaskingConfigurator()
        {
            InitializeComponent();
            MaskingConfigSourcePath = Convert.ToString(ConfigurationManager.AppSettings["ConfigurationSourcePath"]);
            MaskingSourceFileName = string.Format("{0}{1}{2}", MaskingConfigSourcePath, Path.DirectorySeparatorChar, "MaskingSource.xml");
            MaskingSourceSchemaFileName = string.Format("{0}{1}{2}", MaskingConfigSourcePath, Path.DirectorySeparatorChar, "MaskingSourceSchema.xsd");

            maskingSource = new MaskingSource();
            dtSource = maskingSource.Tables["Source"];
            dtConfiguration = maskingSource.Tables["Configuration"];
            dtWorkflow = maskingSource.Tables["Workflow"];
            dtProduct = maskingSource.Tables["Product"];

        }
        private void DataMaskingConfigurator_Load(object sender, EventArgs e)
        {

            AssemblyName asm = Assembly.GetExecutingAssembly().GetName();
            string version = string.Format("{0}.{1}", asm.Version.Major, asm.Version.Minor);
            this.Text = string.Format("Data Masking Configurator (v{0})", version);

            lnkShowAllSource.Visible = false;
            lnkShowAllConfiguration.Visible = false;

            if (!File.Exists(MaskingSourceFileName))
            {
                SetDefaultMaskingSourceSchema(maskingSource);
            }
            maskingSource.ReadXml(MaskingSourceFileName);
          
            maskingSource.AcceptChanges();
            SaveAndReloadChangesToGrid(0, 0, false);
        }
        private void DataMaskingConfigurator_Resize(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dgvSource.Columns)
            {
                col.HeaderCell.Tag = col.Width / 2 <= 0 ? col.Width : col.Width / 2;
            }
            foreach (DataGridViewColumn col in dgvConfiguration.Columns)
            {
                col.HeaderCell.Tag = col.Width / 2 <= 0 ? col.Width : col.Width / 2;
            }
        }

        #endregion

        #region Form Control Events

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            if (maskingSource.HasChanges())
            {
                dr = MessageBox.Show("There are unsaved changes, Are you sure you want to quit application?", Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            else
            {
                dr = MessageBox.Show("Are you sure you want quit application?", Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }
        private void btnAcceptSaveChanges_Click(object sender, EventArgs e)
        {
            SaveAndReloadChangesToGrid();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string pathDestination = folderBrowserDialog1.SelectedPath;
                string destMaskingSourceFileName = Path.Combine(pathDestination, string.Format("MaskingSource_{0}.xml", DateTime.Now.ToString("ddMMMyyyyHHmm")));
                string destMaskingSourceSchemaFileName = Path.Combine(pathDestination, string.Format("MaskingSourceSchema_{0}.xsd", DateTime.Now.ToString("ddMMMyyyyHHmm")));
                File.Copy(MaskingSourceFileName, destMaskingSourceFileName, true);
                File.Copy(MaskingSourceSchemaFileName, destMaskingSourceSchemaFileName, true);
                MessageBox.Show("Files have been exported to destination folder path.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnMasterGroup_Click(object sender, EventArgs e)
        {
            using (frmGroups frmAddEdit = new frmGroups(maskingSource))
            {
                DialogResult dR = frmAddEdit.ShowDialog();
                if (dR == DialogResult.OK)
                {
                    SaveAndReloadChangesToGrid();
                }
            }
        }

        private void btnSrcDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvSource.SelectedRows.Count <= 0)
                MessageBox.Show("Please select any record to delete.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete selected record setting and its configuration?", Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    dtConfigurationCopy = dtConfiguration.Copy();
                    dtSourceCopy = dtSource.Copy();
                    DataGridViewRow row = this.dgvSource.SelectedRows[0];
                    int SourceId = Convert.ToInt32(row.Cells["SourceId"].Value);
                    SourceID = SourceId;
                    foreach (DataRow dataRow in dtConfiguration.Rows)
                    {
                        if (dataRow.RowState != DataRowState.Deleted)
                        {
                            if (Convert.ToInt32(dataRow["SourceId"]) == SourceId)
                            {
                                
                                dataRow.Delete();
                            }
                        }
                    }
                    dtSource.Rows[row.Index].Delete();
                    foreach (DataRow dataRow in dtSource.Rows)
                    {
                       
                        dataRow.Delete();
                    }
                    dtSource.AcceptChanges();
                    SetSourceID();
                    SaveAndReloadChangesToGrid();
                }
            }
        }
        private void btnCnfDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvConfiguration.SelectedRows.Count <= 0)
                MessageBox.Show("Please select any record to delete.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete selected configuration?", Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                   
                    dtConfigurationCopy = dtConfiguration.Copy();
                    int sourceId = Convert.ToInt32(this.dgvConfiguration.SelectedRows[0].Cells["SourceId"].Value);
                    int configurationId = Convert.ToInt32(this.dgvConfiguration.SelectedRows[0].Cells["ConfigurationId"].Value);
                    foreach (DataRow dataRow in dtConfiguration.Rows)
                    {
                        if (dataRow.RowState != DataRowState.Deleted &&
                            Convert.ToInt32(dataRow["ConfigurationId"]) == configurationId)
                        {
                            ConfigurationID = configurationId;
                         
                        }
                    }
                    
                    foreach (DataRow dataRow in dtConfiguration.Rows)
                    {
                      dataRow.Delete();
                    }
                    dtConfiguration.AcceptChanges();
                    SetConfigutaionID();
                    SaveAndReloadChangesToGrid(sourceId);
                }
            }
        }
        
        private void btnSrcAdd_Click(object sender, EventArgs e)
        {
            using (frmSourceAddEdit frmAddEdit = new frmSourceAddEdit(0, maskingSource,""))
            {
                DialogResult dR = frmAddEdit.ShowDialog();
                if (dR == DialogResult.OK)
                {
                    SaveAndReloadChangesToGrid();
                    
                }
                else if (dR == DialogResult.No)
                {
                    this.BindingContext[dtConfiguration].EndCurrentEdit();
                    SaveAndReloadChangesToGrid();
                   
                }
            }
        }
        private void btnCnfAdd_Click(object sender, EventArgs e)
        {
            if (this.dgvSource.SelectedRows.Count <= 0)
                MessageBox.Show("Please select any record in data masking type definition to add configuration.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                int SourceId = Convert.ToInt32(dgvSource.SelectedRows[0].Cells["SourceId"].Value);
                string FileExt = Convert.ToString(dgvSource.SelectedRows[0].Cells["FileExtensionTargets"].Value).ToUpper();
                using (frmConfigurationAddEdit frmConfigurationAddEdit = new frmConfigurationAddEdit(SourceId, 0, maskingSource, FileExt))
                {
                    DialogResult dR = frmConfigurationAddEdit.ShowDialog();
                    if (dR == DialogResult.OK)
                    {
                        SaveAndReloadChangesToGrid(SourceId);
                        dgvConfiguration.Rows[dgvConfiguration.Rows.Count - 1].Selected = true;
                    }
                    else if (dR == DialogResult.No)
                    {
                        this.BindingContext[dtConfiguration].EndCurrentEdit();
                        SaveAndReloadChangesToGrid(SourceId);
                        dgvConfiguration.Rows[dgvConfiguration.Rows.Count - 1].Selected = true;
                    }
                 }
            }
        }
        private void btnSrcEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvSource.SelectedRows.Count <= 0)
                MessageBox.Show("Please select any record to edit.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                int SourceId = Convert.ToInt32(dgvSource.SelectedRows[0].Cells["SourceId"].Value);
                string SubType= Convert.ToString(dgvSource.SelectedRows[0].Cells["SubType"].Value);
                int configurationId = dgvConfiguration.SelectedRows.Count > 0 ?
                    Convert.ToInt32(dgvConfiguration.SelectedRows[0].Cells["ConfigurationId"].Value) : 0;
                using (frmSourceAddEdit frmAddEdit = new frmSourceAddEdit(SourceId, maskingSource, SubType))
                {
                    DialogResult dR = frmAddEdit.ShowDialog();
                    if (dR == DialogResult.OK)
                    {
                        SaveAndReloadChangesToGrid(SourceId, configurationId);
                    }
                }
            }
        }
        private void btnCnfEdit_Click(object sender, EventArgs e)
        {
            string FileExt = string.Empty;

            if (this.dgvConfiguration.SelectedRows.Count <= 0)
                MessageBox.Show("Please select any record to edit.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                int sourceId = Convert.ToInt32(this.dgvConfiguration.SelectedRows[0].Cells["SourceId"].Value);
                int configurationId = Convert.ToInt32(dgvConfiguration.SelectedRows[0].Cells["ConfigurationId"].Value);
                if (this.dgvSource.SelectedRows.Count > 0)
                {
                    FileExt = Convert.ToString(dgvSource.SelectedRows[0].Cells["FileExtensionTargets"].Value).ToUpper();
                }
                using (frmConfigurationAddEdit frmConfigurationAddEdit = new frmConfigurationAddEdit(sourceId, configurationId, maskingSource, FileExt))
                {
                    DialogResult dR = frmConfigurationAddEdit.ShowDialog();
                    if (dR == DialogResult.OK)
                    {
                        SaveAndReloadChangesToGrid(sourceId, configurationId);
                    }
                }
            }
        }
        private void lnkShowAllSource_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dgvSource);
        }
        private void lnkShowAllConfiguration_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dgvConfiguration);
        }
        private void dgvSource_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvSource.SelectedRows.Count > 0)
            {
                DataGridViewRow row = this.dgvSource.SelectedRows[0];
                int SourceId = Convert.ToInt32(row.Cells["SourceId"].Value);
                int FileExtnType = Convert.ToInt32((FileExtnType)Enum.Parse(typeof(FileExtnType), Convert.ToString(row.Cells["FileExtnType"].Value)));
                string FileExt = Convert.ToString(row.Cells["FileExtensionTargets"].Value).ToUpper();
                BindSourceConfigurationGrid(SourceId, FileExtnType, FileExt);
                SetControlsVisibility();
            }
        }
        private void dgvSource_BindingContextChanged(object sender, EventArgs e)
        {
            if (dgvSource.DataSource == null)
            {
                return;
            }
            foreach (DataGridViewColumn col in dgvSource.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }
            dgvSource.AutoResizeColumns();

            dgvConfiguration_BindingContextChanged(dgvConfiguration, e);
        }
        private void dgvSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dgvSource.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }
        private void dgvSource_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dgvSource);
            if (String.IsNullOrEmpty(filterStatus))
            {
                lnkShowAllSource.Visible = false;
                lblFilterSourceStatus.Visible = false;
            }
            else
            {
                lnkShowAllSource.Visible = true;
                lblFilterSourceStatus.Visible = true;
                lblFilterSourceStatus.Text = filterStatus;
            }
            foreach (DataGridViewColumn col in dgvSource.Columns)
            {
                col.HeaderCell.Tag = col.Width / 2 <= 0 ? col.Width : col.Width / 2;
            }
        }
        private void dgvSource_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Button == MouseButtons.Left)
                {
                    dgvSource.ClearSelection();
                }
            }
        }

        private void dgvConfiguration_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Button == MouseButtons.Left)
                {
                    dgvConfiguration.ClearSelection();
                }
            }
        }
        private void dgvConfiguration_BindingContextChanged(object sender, EventArgs e)
        {
            if (dgvConfiguration.DataSource == null)
            {
                return;
            }
            foreach (DataGridViewColumn col in dgvConfiguration.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }
            dgvConfiguration.AutoResizeColumns();
        }
        private void dgvConfiguration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dgvConfiguration.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }
        private void dgvConfiguration_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dgvConfiguration);
            if (String.IsNullOrEmpty(filterStatus))
            {
                lnkShowAllConfiguration.Visible = false;
                lblFilterConfigurationStatus.Visible = false;
            }
            else
            {
                lnkShowAllConfiguration.Visible = true;
                lblFilterConfigurationStatus.Visible = true;
                lblFilterConfigurationStatus.Text = filterStatus;
            }
            foreach (DataGridViewColumn col in dgvConfiguration.Columns)
            {
                col.HeaderCell.Tag = col.Width / 2 <= 0 ? col.Width : col.Width / 2;
            }
        }


        #endregion

        #region Private Methods

        private void BindSourceGrid()
        {
            DataView defaultView = dtSource.DefaultView;
            BindingSource dataSource = new BindingSource(defaultView, null);
            dgvSource.DataSource = dataSource;
        }
        private void BindSourceConfigurationGrid(int SourceId, int sourceFileType, string FileExt)
        {
            DataRow parentRow = dtSource.Rows.Find(SourceId);
            DataRow[] childRows = parentRow.GetChildRows(dtSource.ChildRelations[0], DataRowVersion.Current);

            if (childRows.Length > 0)
            {
                DataTable dtRecords = childRows.CopyToDataTable();
                DataView defaultView = dtRecords.DefaultView;
               
                BindingSource dataConfiguration = new BindingSource(defaultView, null);
                dgvConfiguration.DataSource = dataConfiguration;
                dgvConfiguration.Columns["SourceId"].Visible = false;
                dgvConfiguration.Columns["Version"].Visible = false;
                dgvConfiguration.Columns["RowNo"].Visible = false;
                if (sourceFileType == (int)FileExtnType.PrintImageSD)
                {
                    dgvConfiguration.Columns["SettingName"].Visible = true;
                    dgvConfiguration.Columns["StartPos"].Visible = true;
                    dgvConfiguration.Columns["PageNumber"].Visible = true;
                    dgvConfiguration.Columns["FieldLength"].Visible = true;
                    dgvConfiguration.Columns["DelimitedColumns"].Visible = false;
                    dgvConfiguration.Columns["RootPath"].Visible = false;
                    dgvConfiguration.Columns["FieldPath"].Visible = false;
                    dgvConfiguration.Columns["RowNo"].Visible = true;
                    dgvConfiguration.Columns["RowNo"].Width = 100;
                }
                else
                {
                    dgvConfiguration.Columns["SettingName"].Visible = true;
                    dgvConfiguration.Columns["StartPos"].Visible = true;
                    dgvConfiguration.Columns["FieldLength"].Visible = true;
                    dgvConfiguration.Columns["DelimitedColumns"].Visible = false;
                    dgvConfiguration.Columns["RootPath"].Visible = false;
                    dgvConfiguration.Columns["FieldPath"].Visible = false;
                }
            }
            else
            {
                dgvConfiguration.DataSource = null;
                dgvConfiguration.AutoGenerateColumns = true;
                dgvConfiguration.ClearSelection();
            }
        }
        private void AcceptAndPersistDataChanges()
        {
            maskingSource.WriteXmlSchema(MaskingSourceSchemaFileName);
            maskingSource.WriteXml(MaskingSourceFileName);
            maskingSource.AcceptChanges();
            MessageBox.Show("The action(s) has been performed successfully.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SaveAndReloadChangesToGrid(int SourceId = 0, int configurationId = 0, bool SaveChanges = true)
        {
            if (SaveChanges) { AcceptAndPersistDataChanges(); }
            BindSourceGrid();
            if (SourceId > 0)
            {
                dgvSource.ClearSelection();
                DataGridViewRow row = dgvSource.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["SourceId"].Value.ToString().Equals(SourceId.ToString()))
                    .First();
                dgvSource.Rows[row.Index].Selected = true;
            }
            if (configurationId > 0)
            {
                dgvConfiguration.ClearSelection();
                DataGridViewRow row = dgvConfiguration.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["ConfigurationId"].Value.ToString().Equals(configurationId.ToString()))
                    .First();
                dgvConfiguration.Rows[row.Index].Selected = true;
            }
            SetControlsVisibility();
        }
        private void SetControlsVisibility()
        {
            btnSrcDelete.Enabled = dgvSource.Rows.Count > 0 ? true : false;
            btnCnfDelete.Enabled = dgvConfiguration.Rows.Count > 0 ? true : false;

            btnSrcEdit.Enabled = dgvSource.Rows.Count > 0 ? true : false;
            btnCnfEdit.Enabled = dgvConfiguration.Rows.Count > 0 ? true : false;

            btnAcceptSaveChanges.Enabled = true;// maskingSource.HasChanges();
            btnAcceptSaveChanges.Visible = false;
        }
        private void SetDefaultMaskingSourceSchema(MaskingSource maskingSource)
        {
            if (File.Exists(MaskingSourceFileName))
            { File.Delete(MaskingSourceFileName); }

            try
            {
                // Workflow
                DataRow drWF = dtWorkflow.NewRow();
                drWF["WorkflowId"] = "1";
                drWF["WorkflowVal"] = "Default";
                dtWorkflow.Rows.Add(drWF);

               // Product 
                DataRow drPN = dtProduct.NewRow();
                drPN["ProductId"] = "1";
                drPN["ProductVal"] = "Default";
                dtProduct.Rows.Add(drPN);
              }
            catch (Exception ex)
            {

                throw;
            }

            //Configuration - 1 for TXT -Rec Type = 0

            DataRow drMS = dtSource.NewRow();
            drMS["SourceId"] = "1";
            drMS["ConfigurationName"] = "Print ImageSD Configuration ";
            drMS["RecordType"] = "N/A";
            drMS["FileExtensionTargets"] = "TXT";
            drMS["SubType"] = "GEFLEET";
            drMS["FileExtnType"] = FileExtnType.PrintImageSD.ToString();
            dtSource.Rows.Add(drMS);

            DataRow drMC = dtConfiguration.NewRow();
            drMC["SourceId"] = "1";
            drMC["ConfigurationId"] = "1";
            drMC["SettingName"] = "AccountNo";
            drMC["PageNumber"] = "2";
            drMC["RowNo"] = "8";
            drMC["StartPos"] = "119";
            drMC["FieldLength"] = "10";
            drMC["Workflow"] = "Default";
            drMC["Product"] = "Default";
            dtConfiguration.Rows.Add(drMC);

            drMC = dtConfiguration.NewRow();
            drMC["SourceId"] = "1";
            drMC["ConfigurationId"] = "2";
            drMC["SettingName"] = "AddressLine1";
            drMC["PageNumber"] = "1";
            drMC["RowNo"] = "18";
            drMC["StartPos"] = "14";
            drMC["FieldLength"] = "30";
            drMC["Workflow"] = "Default";
            drMC["Product"] = "Default";
            dtConfiguration.Rows.Add(drMC);

            drMC = dtConfiguration.NewRow();
            drMC["SourceId"] = "1";
            drMC["ConfigurationId"] = "3";
            drMC["SettingName"] = "Address Lines";
            drMC["PageNumber"] = "1";
            drMC["RowNo"] = "20,22,24,26";
            drMC["StartPos"] = "14";
            drMC["FieldLength"] = "30";
            drMC["Workflow"] = "Default";
            drMC["Product"] = "Default";
            dtConfiguration.Rows.Add(drMC);


            maskingSource.WriteXmlSchema(MaskingSourceSchemaFileName);
            maskingSource.WriteXml(MaskingSourceFileName);
            maskingSource.AcceptChanges();
            maskingSource.Clear();
        }

        #endregion
        private void SetSourceID()
        {
            int i = 1;
            foreach (DataRow dataRow in dtSourceCopy.Rows)
            {

                if (SourceID != Convert.ToInt32(dataRow["SourceId"]))
                {

                    DataRow dr = dtSource.NewRow();
                    dr["SourceId"] = i;
                    dr["ConfigurationName"] = dataRow["ConfigurationName"];
                    dr["FileExtensionTargets"] = dataRow["FileExtensionTargets"];
                    dr["SubType"] = dataRow["SubType"];
                    dr["RecordType"] = dataRow["RecordType"]; ;
                    dr["FileExtnType"] = dataRow["FileExtnType"]; ;
                    dtSource.Rows.Add(dr);
                    dtSource.AcceptChanges();
                    i = i + 1;
                }
            }

           
            foreach (DataRow dataRow in dtConfigurationCopy.Rows)
            {

                if (SourceID != Convert.ToInt32(dataRow["SourceId"]))
                {

                    DataRow dr = dtConfiguration.NewRow();
                    dr["ConfigurationId"] = dataRow["ConfigurationId"];
                    dr["SourceId"] = dataRow["SourceId"];
                    dr["SettingName"] = dataRow["SettingName"];
                    dr["PageNumber"] = dataRow["PageNumber"];
                    dr["RowNo"] = dataRow["RowNo"];
                    dr["StartPos"] = dataRow["StartPos"];
                    dr["FieldLength"] = dataRow["FieldLength"];
                    dr["DelimitedColumns"] = dataRow["DelimitedColumns"];
                    dr["RootPath"] = dataRow["RootPath"];
                    dr["FieldPath"] = dataRow["FieldPath"];
                    dr["Version"] = dataRow["Version"];
                    dr["Workflow"] = dataRow["Workflow"];
                    dr["Product"] = dataRow["Product"];
                    dtConfiguration.Rows.Add(dr);
                    dtConfiguration.AcceptChanges();
                   
                }
            }
        }
        private void SetConfigutaionID()
        {
                 
            int i = 1;
            foreach (DataRow dataRow in dtConfigurationCopy.Rows)
            {
              
                if (ConfigurationID != Convert.ToInt32(dataRow["ConfigurationId"]))
                {
                   
                    DataRow dr = dtConfiguration.NewRow();
                    dr["ConfigurationId"] = i;
                    dr["SourceId"] = dataRow["SourceId"];
                    dr["SettingName"] = dataRow["SettingName"];
                    dr["PageNumber"] = dataRow["PageNumber"];
                    dr["RowNo"] = dataRow["RowNo"];
                    dr["StartPos"] = dataRow["StartPos"];
                    dr["FieldLength"] = dataRow["FieldLength"];
                    dr["DelimitedColumns"] = dataRow["DelimitedColumns"];
                    dr["RootPath"] = dataRow["RootPath"];
                    dr["FieldPath"] = dataRow["FieldPath"];
                    dr["Version"] = dataRow["Version"];
                    dr["Workflow"] = dataRow["Workflow"];
                    dr["Product"] = dataRow["Product"];
                    dtConfiguration.Rows.Add(dr);
                    dtConfiguration.AcceptChanges();
                    i = i + 1;
                }
            }
        }
    }
}
