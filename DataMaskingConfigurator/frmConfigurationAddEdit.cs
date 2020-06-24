using DataMaskingConfigurator.DataModel;
using NetCommonUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NetCommonUtilities.Enums;

namespace DataMaskingConfigurator
{
    public partial class frmConfigurationAddEdit : Form
    {
        private bool HasRecordChanged;
        private readonly int ParentSourceId;
        private readonly int ChildConfigurationId;
        private FileExtnType FileExtensionType;
        private readonly string FileExtensionTargets;
        private MaskingSource MaskingSource { get; set; }
        private DataTable dtSource { get { return MaskingSource.Tables["Source"]; } }
        private DataTable dtConfiguration { get { return MaskingSource.Tables["Configuration"]; } }
        private DataTable dtWorkflow { get { return MaskingSource.Tables["Workflow"]; } }
        private DataTable dtProduct { get { return MaskingSource.Tables["Product"]; } }
        private int SettingNameVal = 0;
        private Enums.FormMode Mode { get { return ChildConfigurationId > 0 ? Enums.FormMode.Edit : Enums.FormMode.Add; } }
       public frmConfigurationAddEdit(int parentSourceId, int EditConfigurationId, MaskingSource maskingSource, string FileExt)
        {
            this.MaskingSource = maskingSource;
            this.ParentSourceId = parentSourceId;
            this.ChildConfigurationId = EditConfigurationId;
            this.FileExtensionTargets = FileExt;
            InitializeComponent();
            txtVersion.Enabled = FileExtn.TXT.ToString() == FileExt ? false : true;
            this.Height = 550;
        }

        private void txtRecordTypes_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
            int lastCommaIndex = ((sender as TextBox).Text.LastIndexOf(',')) == -1 ? 0 : ((sender as TextBox).Text.LastIndexOf(','));
            if ((e.KeyChar == ',') && lastCommaIndex == ((sender as TextBox).Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void txtFileExtensions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
            int lastCommaIndex = ((sender as TextBox).Text.LastIndexOf(',')) == -1 ? 0 : ((sender as TextBox).Text.LastIndexOf(','));
            if ((e.KeyChar == ',') && lastCommaIndex == ((sender as TextBox).Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void txtConfigName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
            int lastSpaceIndex = ((sender as TextBox).Text.LastIndexOf((char)Keys.Space)) == -1 ? 0 : ((sender as TextBox).Text.LastIndexOf((char)Keys.Space));
            if ((e.KeyChar == (char)Keys.Space) && lastSpaceIndex == ((sender as TextBox).Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void txtSettingName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
            int lastSpaceIndex = ((sender as TextBox).Text.LastIndexOf((char)Keys.Space)) == -1 ? 0 : ((sender as TextBox).Text.LastIndexOf((char)Keys.Space));
            if ((e.KeyChar == (char)Keys.Space) && lastSpaceIndex == ((sender as TextBox).Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void txtStartPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }
        private void txtFieldLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            //{
            //    e.Handled = true;
            //}


        }
        private void txtDelimitedColumns_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
            if ((string.IsNullOrEmpty((sender as TextBox).Text) && (e.KeyChar == ',')) || ((sender as TextBox).Text.Length == 1 && (sender as TextBox).Text.Contains(',')))
                e.Handled = true;

            int lastCommaIndex = ((sender as TextBox).Text.LastIndexOf(',')) == -1 ? 0 : ((sender as TextBox).Text.LastIndexOf(','));
            if ((e.KeyChar == ',') && (sender as TextBox).Text.Length > 1 && lastCommaIndex == ((sender as TextBox).Text.Length - 1))
            {
                e.Handled = true;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = HasRecordChanged == true ? DialogResult.No : DialogResult.Cancel;
            this.Close();
        }
        public bool checkIfExist()
        {
            bool IsExist = false;
            foreach (DataRow dataRow in dtConfiguration.Rows)
            {
                if (ChildConfigurationId != 0 && ChildConfigurationId.ToString() != dataRow["ConfigurationId"].ToString())
                {
                    if (dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper())
                    {
                        SettingNameVal = (int)Enums.RecordType.First;
                        IsExist = true;
                    }
                    else if (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim())
                    {
                        SettingNameVal = (int)Enums.RecordType.Four;
                        IsExist = true;
                    }
                    if ((dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper()) && (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim()
                    && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim()))
                    {
                        SettingNameVal = (int)Enums.RecordType.Five;
                        IsExist = true;
                    }
                    else if (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim())
                    {
                        SettingNameVal = (int)Enums.RecordType.Three;
                        IsExist = true;
                    }
                    else if (dataRow["RowNo"].ToString().Contains(',') && txtRowNo.Text.Trim().Contains(','))
                    {
                        bool match = dataRow["RowNo"].ToString().Split(',').Intersect(txtRowNo.Text.Trim().Split(',')).Any();
                        if ((dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && match && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim()))
                        {
                            SettingNameVal = (int)Enums.RecordType.Three;
                            IsExist = true;
                        }
                    }
                    else if (dataRow["RowNo"].ToString().Contains(',') && txtRowNo.Text.Trim().Contains(','))
                    {
                        bool match = dataRow["RowNo"].ToString().Split(',').Intersect(txtRowNo.Text.Trim().Split(',')).Any();
                        if ((dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper()) && (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim()
                     && match && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim()))
                        {
                            SettingNameVal = (int)Enums.RecordType.Five;
                            IsExist = true;
                        }
                    }

                }
                else if (ChildConfigurationId == 0 && ChildConfigurationId.ToString() != dataRow["ConfigurationId"].ToString())
                {
                    if (dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper())
                    {
                        SettingNameVal = (int)Enums.RecordType.First;
                        IsExist = true;
                    }
                    else if (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim())
                    {
                        SettingNameVal = (int)Enums.RecordType.Four;
                        IsExist = true;
                    }
                    else if (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim())
                    {
                        SettingNameVal = (int)Enums.RecordType.Three;
                        IsExist = true;
                    }
                    else if (dataRow["RowNo"].ToString().Contains(',') && txtRowNo.Text.Trim().Contains(','))
                    {
                        bool match = dataRow["RowNo"].ToString().Split(',').Intersect(txtRowNo.Text.Trim().Split(',')).Any();
                        if ((dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && match && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim()))
                        {
                            SettingNameVal = (int)Enums.RecordType.Three;
                            IsExist = true;
                        }
                    }
                    else if (!dataRow["RowNo"].ToString().Contains(',') && txtRowNo.Text.Trim().Contains(','))
                    {
                        bool match = txtRowNo.Text.Trim().Split(',').Contains(dataRow["RowNo"].ToString());// dataRow["RowNo"].ToString().Split(',').Intersect(txtRowNo.Text.Trim().Split(',')).Any();
                        if ((dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && match && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim()))
                        {
                            SettingNameVal = (int)Enums.RecordType.Three;
                            IsExist = true;
                        }
                    }
                    if ((dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper()) && (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim()
                    && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim()))
                    {
                        SettingNameVal = (int)Enums.RecordType.Five;
                        IsExist = true;
                    }
                    else if (dataRow["RowNo"].ToString().Contains(',') && txtRowNo.Text.Trim().Contains(','))
                    {
                        bool match = dataRow["RowNo"].ToString().Split(',').Intersect(txtRowNo.Text.Trim().Split(',')).Any();
                        if ((dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper()) && (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim()
                     && match && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim()))
                        {
                            SettingNameVal = (int)Enums.RecordType.Five;
                            IsExist = true;
                        }
                    }
                }

            }
           
            return IsExist;
        }
        public bool checkDataIfExist()
        {
            bool IsExist = false;
            foreach (DataRow dataRow in dtConfiguration.Rows)
            {
                if (ChildConfigurationId != Convert.ToInt32(dataRow["ConfigurationId"]))
                {
                    if (dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper())
                    {
                        SettingNameVal = (int)Enums.RecordType.First;
                        IsExist = true;
                    }
                    else if (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim())
                    {
                        SettingNameVal = (int)Enums.RecordType.Four;
                        IsExist = true;
                    }
                    else if (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim() && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim())
                    {
                        SettingNameVal = (int)Enums.RecordType.Three;
                        IsExist = true;
                    }
                    else if (dataRow["RowNo"].ToString().Contains(',') && txtRowNo.Text.Trim().Contains(','))
                    {
                        bool match = dataRow["RowNo"].ToString().Split(',').Intersect(txtRowNo.Text.Trim().Split(',')).Any();
                        if ( (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim()  && match && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim()))
                        {
                            SettingNameVal = (int)Enums.RecordType.Three;
                            IsExist = true;
                        }
                    }
                    if ((dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper()) && (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim()
                    && dataRow["RowNo"].ToString().Contains(txtRowNo.Text.Trim()) && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim()))
                    {
                        SettingNameVal = (int)Enums.RecordType.Five;
                        IsExist = true;
                    }
                    else if (dataRow["RowNo"].ToString().Contains(',') && txtRowNo.Text.Trim().Contains(','))
                    {
                        bool match = dataRow["RowNo"].ToString().Split(',').Intersect(txtRowNo.Text.Trim().Split(',')).Any();
                        if ((dataRow["SettingName"].ToString().ToUpper() == txtSettingName.Text.Trim().ToUpper()) && (dataRow["PageNumber"].ToString() == txtPageNumber.Text.Trim()
                     && match && dataRow["StartPos"].ToString() == txtStartPos.Text.Trim() && dataRow["FieldLength"].ToString() == txtFieldLength.Text.Trim()))
                        {
                            SettingNameVal = (int)Enums.RecordType.Five;
                            IsExist = true;
                        }
                    }
                }
            }
                   
            return IsExist;
        }

        
        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            string _message = string.Empty;
            string rowNums = txtRowNo.Text.Trim() != "" ? string.Join(",", txtRowNo.Text.Trim().Split(',').Distinct().ToArray()):"0";
            if (rowNums.EndsWith(",")) { rowNums = rowNums.Remove(rowNums.Length - 1);  }
            int ConfigurationID = dtConfiguration.Rows.Count;
            DataRow dataRow = dtConfiguration.Rows.Find(ChildConfigurationId);
            if (IsValidFormData(out _message))
            {
                if (Mode == Enums.FormMode.Add && !checkIfExist())
                {
                    DataRow dr = dtConfiguration.NewRow();
                    dr["ConfigurationId"] = ConfigurationID + 1;
                    dr["SourceId"] = ParentSourceId;
                    dr["SettingName"] = txtSettingName.Text.Trim();
                    dr["PageNumber"] = txtPageNumber.Text.Trim();
                    dr["RowNo"] = rowNums;
                    dr["StartPos"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtStartPos.Text.Trim() : "0";
                    dr["FieldLength"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtFieldLength.Text.Trim() : "0";
                    dr["DelimitedColumns"] = txtDelimitedColumns.Text.Trim();
                    dr["RootPath"] = txtPath.Text.Trim();
                    dr["FieldPath"] = txtFieldName.Text.Trim();
                    dr["Workflow"] = ((KeyValuePair<int, string>)cmbxWorkflow.SelectedItem).Value;
                    dr["Product"] = ((KeyValuePair<int, string>)cmbxProduct.SelectedItem).Value;
                    dtConfiguration.Rows.Add(dr);
                    SetMessageText("The changes has been saved for Configuration {1}");
                }
                else if(dataRow != null && dataRow["SettingName"].ToString().ToUpper()== txtSettingName.Text.ToUpper().Trim()  && dataRow["ConfigurationId"].ToString()== ChildConfigurationId.ToString() && !checkDataIfExist())
                {
                    
                    dataRow["SourceId"] = ParentSourceId;
                    dataRow["ConfigurationId"] = ChildConfigurationId;
                    dataRow["SettingName"] = txtSettingName.Text.Trim();
                    dataRow["PageNumber"] = txtPageNumber.Text.Trim();
                    dataRow["RowNo"] = rowNums.TrimEnd(',');
                    dataRow["StartPos"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtStartPos.Text.Trim() : "0";
                    dataRow["FieldLength"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtFieldLength.Text.Trim() : "0";
                    dataRow["DelimitedColumns"] = txtDelimitedColumns.Text.Trim();
                    dataRow["RootPath"] = txtPath.Text.Trim();
                    dataRow["FieldPath"] = txtFieldName.Text.Trim();
                    dataRow["Workflow"] = ((KeyValuePair<int, string>)cmbxWorkflow.SelectedItem).Value;
                    dataRow["Product"] = ((KeyValuePair<int, string>)cmbxProduct.SelectedItem).Value;
                    SetMessageText("Changes have been updated");
                }
                else if (!checkIfExist() )
                {

                    dataRow["SourceId"] = ParentSourceId;
                    dataRow["ConfigurationId"] = ChildConfigurationId;
                    dataRow["SettingName"] = txtSettingName.Text.Trim();
                    dataRow["PageNumber"] = txtPageNumber.Text.Trim();
                    dataRow["RowNo"] = rowNums.TrimEnd(',');
                    dataRow["StartPos"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtStartPos.Text.Trim() : "0";
                    dataRow["FieldLength"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtFieldLength.Text.Trim() : "0";
                    dataRow["DelimitedColumns"] = txtDelimitedColumns.Text.Trim();
                    dataRow["RootPath"] = txtPath.Text.Trim();
                    dataRow["FieldPath"] = txtFieldName.Text.Trim();
                    dataRow["Workflow"] = ((KeyValuePair<int, string>)cmbxWorkflow.SelectedItem).Value;
                    dataRow["Product"] = ((KeyValuePair<int, string>)cmbxProduct.SelectedItem).Value;
                    SetMessageText("Changes have been updated");
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    SetDialogMessage();
                    return;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                SetMessageText(_message, true);
                this.DialogResult = DialogResult.None;
            }
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {

            string _message = string.Empty;
             string rowNums = txtRowNo.Text.Trim() != "" ? string.Join(",", txtRowNo.Text.Trim().Split(',').Distinct().ToArray()):"0";
            if (rowNums.EndsWith(",")) { rowNums = rowNums.Remove(rowNums.Length - 1); }
            int ConfigurationID = dtConfiguration.Rows.Count;
            if (IsValidFormData(out _message))
            {
                if (Mode == Enums.FormMode.Add && !checkIfExist())
                {
                    DataRow dr = dtConfiguration.NewRow();
                    dr["ConfigurationId"] = ConfigurationID + 1;
                    dr["SourceId"] = ParentSourceId;
                    dr["SettingName"] = txtSettingName.Text.Trim();
                    dr["PageNumber"] = txtPageNumber.Text.Trim();
                    dr["RowNo"] = rowNums;
                    dr["StartPos"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtStartPos.Text.Trim() : "0";
                    dr["FieldLength"] = (FileExtensionType == FileExtnType.PrintImageSD) ? txtFieldLength.Text.Trim() : "0";
                    dr["DelimitedColumns"] = txtDelimitedColumns.Text.Trim();
                    dr["RootPath"] = txtPath.Text.Trim();
                    dr["FieldPath"] = txtFieldName.Text.Trim();
                    dr["Workflow"] = ((KeyValuePair<int, string>)cmbxWorkflow.SelectedItem).Value;
                    dr["Product"] = ((KeyValuePair<int, string>)cmbxProduct.SelectedItem).Value;
                    dtConfiguration.Rows.Add(dr);
                    SetMessageText("The changes has been saved for Configuration {1}");
                    this.BindingContext[dtConfiguration].EndCurrentEdit();
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    SetDialogMessage();
                    return;
                }
                this.DialogResult = DialogResult.None;
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = string.Empty;
                    }
                }
                LoadFormStartupData();
                HasRecordChanged = true;
                }
            else
            {
                SetMessageText(_message, true);
                txtSettingName.Focus();
                this.DialogResult = DialogResult.None;
            }
           
        }
       
        public void SetDialogMessage()
        {
            if (SettingNameVal == (int)Enums.RecordType.First)
            {
                MessageBox.Show("Please enter a valid setting name.Setting Name already exist.The setting name can be alphanumeric and cannot exceed more than 30 characters.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (SettingNameVal == (int)Enums.RecordType.Three)
            {
                MessageBox.Show("Please enter different page number row number, start position. The entered values fall in the range of already defined values.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (SettingNameVal == (int)Enums.RecordType.Four)
            {
                MessageBox.Show("Please enter different page number row number, start and/or field length. The entered values fall in the range of already defined values.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (SettingNameVal == (int)Enums.RecordType.Five)
            {
                MessageBox.Show("Duplicate Settings Are Not Allowed", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            SetMessageText(string.Empty);
            LoadFormStartupData();
        }
        private void txtPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && (e.KeyChar != '\\') && (e.KeyChar != '_') &&
                (e.KeyChar != '-') && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != 22) && (e.KeyChar != 3) && (e.KeyChar != 1))
            {
                e.Handled = true;
            }
            int lastCommaIndex = ((sender as TextBox).Text.LastIndexOf('\\')) == -1 ? 0 : ((sender as TextBox).Text.LastIndexOf('\\'));
            if ((e.KeyChar == '\\') && lastCommaIndex == ((sender as TextBox).Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && (e.KeyChar != '-') && (e.KeyChar != '\\') &&
                (e.KeyChar != '_') && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != 22) && (e.KeyChar != 3) && (e.KeyChar != 1))
            {
                e.Handled = true;
            }
            int lastCommaIndex = ((sender as TextBox).Text.LastIndexOf('\\')) == -1 ? 0 : ((sender as TextBox).Text.LastIndexOf('\\'));
            if ((e.KeyChar == '\\') && lastCommaIndex == ((sender as TextBox).Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void cmbxWorkflow_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbxProduct_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void cmbxWorkflow_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbxProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private bool IsValidFormData(out string Message)
        {
            Message = string.Empty;
            string rowNums = txtRowNo.Text.Trim() != "" ? string.Join(",", txtRowNo.Text.Trim().Split(',').Distinct().ToArray()) : "0";
            if (string.IsNullOrEmpty(txtSettingName.Text) && string.IsNullOrEmpty(txtPageNumber.Text) && string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Field Name, Page Number, Row Number, Start Position, and Field Length." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtSettingName.Text) && !string.IsNullOrEmpty(txtPageNumber.Text) && !string.IsNullOrEmpty(txtRowNo.Text) && !string.IsNullOrEmpty(txtFieldLength.Text) && !string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid setting name. The setting name can be alphanumeric and cannot exceed more than 30 characters." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(txtSettingName.Text) && string.IsNullOrEmpty(txtPageNumber.Text) && !string.IsNullOrEmpty(txtRowNo.Text) && !string.IsNullOrEmpty(txtFieldLength.Text) && !string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Page Number.The Page Number can only be numeric." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(txtSettingName.Text) && !string.IsNullOrEmpty(txtPageNumber.Text) && !string.IsNullOrEmpty(txtRowNo.Text) && !string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid start position. The start position can only be numeric and cannot exceed more than 6 numerals." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtSettingName.Text) && !string.IsNullOrEmpty(txtPageNumber.Text) && !string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && !string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid setting name and field length.The field length can only be numeric and cannot exceed more than 3 numerals." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(txtSettingName.Text) && string.IsNullOrEmpty(txtPageNumber.Text) && string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Page Number, Row Number, Start Position, and Field Length." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtSettingName.Text) && !string.IsNullOrEmpty(txtPageNumber.Text) && string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Field Name, Row Number, Start Position, and  Field Length." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtSettingName.Text) && string.IsNullOrEmpty(txtPageNumber.Text) && !string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Field Name, Page Number, Start Position, and  Field Length." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtSettingName.Text) && string.IsNullOrEmpty(txtPageNumber.Text) && string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && !string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Field Name, Page Number, Row Number, and Field Length." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtSettingName.Text) && string.IsNullOrEmpty(txtPageNumber.Text) && string.IsNullOrEmpty(txtRowNo.Text) && !string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Field Name, Page Number, Row Number, and Start Position." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(txtSettingName.Text) && !string.IsNullOrEmpty(txtPageNumber.Text) && string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid  Row Number, Start Position  and Field Length." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(txtSettingName.Text) && !string.IsNullOrEmpty(txtPageNumber.Text) && !string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Start Position  and Field Length." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(txtSettingName.Text) && !string.IsNullOrEmpty(txtPageNumber.Text) && !string.IsNullOrEmpty(txtRowNo.Text) && string.IsNullOrEmpty(txtFieldLength.Text) && !string.IsNullOrEmpty(txtStartPos.Text))
            {
                Message += "Please enter a valid Field Length." + Environment.NewLine;
            }
            if (rowNums.Contains("18") && txtFieldLength.Text.Trim().ToUpper() == "EOL")
            {
              Message += "EOL field Length is Not Valid For Line Number 18" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtFileExtensions.Text))
            {
                Message += "Invalid File Extension Target(s)" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtRecordTypes.Text))
            {
                Message += "Invalid Record Type(s)" + Environment.NewLine;
            }
            return string.IsNullOrEmpty(Message);
        }
        private void SetMessageText(string Message, bool IsError = false)
        {
            lblMessage.ForeColor = IsError == true ? Color.Red : Color.Green;
            lblMessage.Text = Message;
        }
        private void LoadFormStartupData()
        {
            cmbxProduct.ContextMenu = new ContextMenu();   //disable right click
            cmbxWorkflow.ContextMenu = new ContextMenu();
            try
            {
                cmbxWorkflow.DataSource = new BindingSource(GetWorkflowDictionary(), null);
                cmbxWorkflow.DisplayMember = "Value";
                cmbxWorkflow.ValueMember = "Key";


                cmbxProduct.DataSource = new BindingSource(GetProductDictionary(), null);
                cmbxProduct.DisplayMember = "Value";
                cmbxProduct.ValueMember = "Key";
            }
            catch (Exception)
            {
            }
            txtSettingName.Text = string.Empty;
            txtStartPos.Text = string.Empty;
            txtFieldLength.Text = string.Empty;
            txtDelimitedColumns.Text = string.Empty;
            DataRow drSource = dtSource.Rows.Find(ParentSourceId);
            txtConfigName.Text = Convert.ToString(drSource["ConfigurationName"]);
            txtFileExtensions.Text = Convert.ToString(drSource["FileExtensionTargets"]);
            txtRecordTypes.Text = Convert.ToString(drSource["RecordType"]);
            FileExtensionType = (FileExtnType)GetFileExtensionTypeDictionary().FirstOrDefault(x => x.Value == Convert.ToString(drSource["FileExtnType"])).Key;
            txtFileExtnType.Text = FileExtensionType.ToString();
           if (FileExtensionType == FileExtnType.PrintImageSD)
            {
                txtStartPos.Enabled = true;
                txtFieldLength.Enabled = true;
                txtDelimitedColumns.Enabled = false;
                txtPath.Enabled = false;
                txtFieldName.Enabled = false;
                txtRowNo.Enabled = true;
                if (Mode == Enums.FormMode.Edit)
                {
                    btnSave.Visible = false;
                    DataRow drChild = dtConfiguration.Rows.Find(ChildConfigurationId);
                    txtSettingName.Text = Convert.ToString(drChild["SettingName"]);
                    txtPageNumber.Text = Convert.ToString(drChild["PageNumber"]);
                    txtRowNo.Text = Convert.ToString(drChild["RowNo"]);
                    txtStartPos.Text = Convert.ToString(drChild["StartPos"]);
                    txtFieldLength.Text = Convert.ToString(drChild["FieldLength"]);
                    txtDelimitedColumns.Text = string.Empty;
                    txtPath.Text = string.Empty;
                    txtFieldName.Text = string.Empty;
                    lblTitle.Text = "Edit record details for Configuration " + txtSettingName.Text;

                    KeyValuePair<int, string> selectedWorkflow = GetWorkflowDictionary().FirstOrDefault(x => x.Value.Trim().ToLower() == Convert.ToString(drChild["Workflow"]).Trim().ToLower());
                    cmbxWorkflow.SelectedItem = selectedWorkflow;
                    KeyValuePair<int, string> selectedProduct = GetProductDictionary().FirstOrDefault(x => x.Value.Trim().ToLower() == Convert.ToString(drChild["Product"]).Trim().ToLower());
                    cmbxProduct.SelectedItem = selectedProduct;
                }
                else
                {
                    lblTitle.Text = "Add new record for Data Masking Configuration.";
                    btnSave.Visible = true;
                }
            }
            else
            {
                txtStartPos.Enabled = true;
                txtFieldLength.Enabled = true;
                txtDelimitedColumns.Enabled = false;
                txtPath.Enabled = false;
                txtFieldName.Enabled = false;
                txtRowNo.Enabled = false;
                if (Mode == Enums.FormMode.Edit)
                {
                    btnSave.Visible = false;
                    DataRow drChild = dtConfiguration.Rows.Find(ChildConfigurationId);
                    txtSettingName.Text = Convert.ToString(drChild["SettingName"]);
                    txtStartPos.Text = Convert.ToString(drChild["StartPos"]);
                    txtFieldLength.Text = Convert.ToString(drChild["FieldLength"]);
                    txtDelimitedColumns.Text = string.Empty;
                    txtPath.Text = string.Empty;
                    txtFieldName.Text = string.Empty;
                    lblTitle.Text = "Editing Record for " + txtSettingName.Text;

                    KeyValuePair<int, string> selectedWorkflow = GetWorkflowDictionary().FirstOrDefault(x => x.Value.Trim().ToLower() == Convert.ToString(drChild["Workflow"]).Trim().ToLower());
                    cmbxWorkflow.SelectedItem = selectedWorkflow;
                    KeyValuePair<int, string> selectedProduct = GetProductDictionary().FirstOrDefault(x => x.Value.Trim().ToLower() == Convert.ToString(drChild["Product"]).Trim().ToLower());
                    cmbxProduct.SelectedItem = selectedProduct;
                }
                else
                {
                    lblTitle.Text = "Adding New Record";
                    btnSave.Visible = true;
                }
            }

            txtSettingName.Focus();
        }
        private Dictionary<int, string> GetWorkflowDictionary()
        {
            var drChild = dtWorkflow.Rows;
            Dictionary<int, string> WorkflowList = new Dictionary<int, string>();
            foreach (DataRow itm in drChild)
            {
                WorkflowList.Add((int)itm["WorkflowId"], itm["WorkflowVal"].ToString()); ;
            }
            return WorkflowList;
        }
        private Dictionary<int, string> GetProductDictionary()
        {
            var drChild = dtProduct.Rows;
            Dictionary<int, string> ProductList = new Dictionary<int, string>();
            foreach (DataRow itm in drChild)
            {
                ProductList.Add((int)itm["ProductId"], itm["ProductVal"].ToString()); ;
            }
            return ProductList;
        }

        private void txtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // && (e.KeyChar != '.')
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers // && ((sender as TextBox).Text.IndexOf('.') > -1)
            if ((e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void txtRowNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
            {
                e.Handled = false;
            }
           else if (!char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtPageNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        }
}
