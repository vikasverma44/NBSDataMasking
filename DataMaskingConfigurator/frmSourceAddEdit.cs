using DataMaskingConfigurator.DataModel;
using NetCommonUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NetCommonUtilities.Enums;

namespace DataMaskingConfigurator
{
    public partial class frmSourceAddEdit : Form
    {
        private bool HasRecordChanged;
        private readonly List<string> AllowedFileExtensions;
        private readonly int EditSourceId;
        private readonly string SubType;
        private MaskingSource MaskingSource { get; set; }
        private DataTable dtSource { get { return MaskingSource.Tables["Source"]; } }
        private Enums.FormMode Mode { get { return EditSourceId > 0 ? Enums.FormMode.Edit : Enums.FormMode.Add; } }
        private readonly Dictionary<int, string> FileExtensionTypeDictionary;

        public frmSourceAddEdit(int EditSourceId, MaskingSource maskingSource, string subType)
        {
            AllowedFileExtensions = Enum.GetNames(typeof(FileExtn)).ToList().ConvertAll(d => d.ToUpper());
            this.MaskingSource = maskingSource;
            this.EditSourceId = EditSourceId;
            this.SubType = subType;
            InitializeComponent();
           FileExtensionTypeDictionary = GetFileExtensionTypeDictionary();
        }
       
        private void txtFileExtensions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '_') && (e.KeyChar != (char)Keys.Back))
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = HasRecordChanged == true ? DialogResult.No : DialogResult.Cancel;
            this.Close();
        }
        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            int SourceID = dtSource.Rows.Count;

            string _message = string.Empty;
            string subtype = txtSubType.Text.Trim();
            if (validateSubType(subtype))
            {
                if (IsValidFormData(out _message))
                {
                    if (Mode == Enums.FormMode.Add)
                    {
                        DataRow dr = dtSource.NewRow();
                        dr["SourceId"] = SourceID + 1;
                        dr["ConfigurationName"] = txtConfigName.Text.Trim();
                        dr["FileExtensionTargets"] = txtFileExtensions.Text.Trim();
                        dr["SubType"] = IsValidSubType() ? txtSubType.Text.Trim() : "N/A";
                        dr["RecordType"] = txtRecordTypes.Text.Trim();
                        dr["FileExtnType"] = ((KeyValuePair<int, string>)cmbxFileType.SelectedItem).Value;
                        dtSource.Rows.Add(dr);
                        SetMessageText("The changes has been saved for Configuration {1}");
                    }
                    else
                    {
                        DataRow dr = dtSource.Rows.Find(EditSourceId);
                        dr["SourceId"] = EditSourceId;
                        dr["ConfigurationName"] = txtConfigName.Text.Trim();
                        dr["FileExtensionTargets"] = txtFileExtensions.Text.Trim();
                        dr["SubType"] = IsValidSubType() ? txtSubType.Text.Trim() : "N/A";
                        dr["RecordType"] = txtRecordTypes.Text.Trim();
                        dr["FileExtnType"] = ((KeyValuePair<int, string>)cmbxFileType.SelectedItem).Value;
                        SetMessageText("Changes have been updated");
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
            else
            {
                SetMessageText("1 Configuration name already exists. Please enter a unique configuration name. 2 File extension with sub type and record type already exists. Please enter a unique record type or choose different combination.");
                this.DialogResult = DialogResult.None;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string _message = string.Empty;
            string subtype = txtSubType.Text.Trim();
            int SourceID = dtSource.Rows.Count;
            if (validateSubType(subtype))
            {
                if (IsValidFormData(out _message))
                {
                    if (Mode == Enums.FormMode.Add)
                    {
                        DataRow dr = dtSource.NewRow();
                        dr["SourceId"] = SourceID + 1;
                        dr["ConfigurationName"] = txtConfigName.Text.Trim();
                        dr["FileExtensionTargets"] = txtFileExtensions.Text.Trim();
                        dr["SubType"] = IsValidSubType() ? txtSubType.Text.Trim() : "N/A";
                        dr["RecordType"] = txtRecordTypes.Text.Trim();
                        dr["FileExtnType"] = ((KeyValuePair<int, string>)cmbxFileType.SelectedItem).Value;
                        dtSource.Rows.Add(dr);
                        SetMessageText("The changes has been saved for Configuration {1}");
                        this.BindingContext[dtSource].EndCurrentEdit();
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
                    this.txtConfigName.Focus();
                    this.DialogResult = DialogResult.None;
                }
            }
            else
            {
                SetMessageText("1 Configuration name already exists. Please enter a unique configuration name. 2 File extension with sub type and record type already exists. Please enter a unique record type or choose different combination.");
                this.DialogResult = DialogResult.None;
            }

        }
        private bool validateSubType(string subType)
        {
            bool retValue = true;
            if (Mode == Enums.FormMode.Add)
            {
                foreach (DataRow drrow in dtSource.Rows)
                {
                    if (txtSubType.Text.ToUpper() == drrow["SubType"].ToString().ToUpper())
                    {
                        retValue = false;
                        return retValue;
                    }
                }
            }
            else
            {
                if (SubType != txtSubType.Text.Trim())
                {
                    foreach (DataRow drrow in dtSource.Rows)
                    {
                        if (txtSubType.Text.ToUpper() == drrow["SubType"].ToString().ToUpper())
                        {
                            SetMessageText("Cannot add this configuration.SubType already exit");
                            retValue = false;
                            return retValue;
                        }
                    }
                }
            }
            retValue = true;
            return retValue;

        }
        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            SetMessageText(string.Empty);
            LoadFormStartupData();
        }
        private void cmbxFileType_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void cmbxFileType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtFileExtensions_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFileExtensions.Text.ToString().Length >= 3 && txtFileExtensions.Text.ToString().ToUpper() != Convert.ToString(FileExtn.TXT))
            {
                MessageBox.Show("Other than TXT No other file type is allowed.", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSaveClose.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                btnSaveClose.Enabled = true;
                btnSave.Enabled = true;
            }
            if ((txtFileExtensions.Text.ToString().Length < 3 || txtFileExtensions.Text.ToString().Length > 3) || txtFileExtensions.Text.ToString().ToUpper() != Convert.ToString(FileExtn.TXT))
            {
                btnSaveClose.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                btnSaveClose.Enabled = true;
                btnSave.Enabled = true;
            }
            if (IsValidSubType())
            {
                txtSubType.Text = "";
                txtSubType.Enabled = true;
                lblMessage.Text = "";
            }
            else
            {
                txtSubType.Enabled = false;
                txtSubType.Text = "N/A";
               
            }
        }
        private void cmbxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxFileType.Text == Convert.ToString(FileExtnType.PrintImageSD))
            {
                txtRecordTypes.Enabled = false;
                txtRecordTypes.Text = "N/A";
            }
            else
            {
                txtRecordTypes.Enabled = true;
                if (txtRecordTypes.Text == "N/A") txtRecordTypes.Text = "";
            }
        }

        private void LoadFormStartupData()
        {
            cmbxFileType.DataSource = new BindingSource(FileExtensionTypeDictionary, null);
            cmbxFileType.DisplayMember = "Value";
            cmbxFileType.ValueMember = "Key";
            cmbxFileType.ContextMenu = new ContextMenu();   //disable right click
            if (Mode == Enums.FormMode.Edit)
            {
                btnSave.Visible = false;
                DataRow dr = dtSource.Rows.Find(EditSourceId);
                KeyValuePair<int, string> selectedRecord = FileExtensionTypeDictionary.FirstOrDefault(x => x.Key == Convert.ToInt32((FileExtnType)Enum.Parse(typeof(FileExtnType), Convert.ToString(dr["FileExtnType"]))));
                txtConfigName.Text = Convert.ToString(dr["ConfigurationName"]);
                txtFileExtensions.Text = Convert.ToString(dr["FileExtensionTargets"]);
                txtSubType.Text = Convert.ToString(dr["SubType"]);
                txtRecordTypes.Text = Convert.ToString(dr["RecordType"]);
                cmbxFileType.SelectedItem = selectedRecord;
                lblTitle.Text = "Edit record details for Configuration " + txtConfigName.Text;
                txtSubType.Enabled = IsValidSubType();
            }
            else
            {
                lblTitle.Text = "Add new record for Data Masking Type Definition.";
                btnSave.Visible = true;
            }
            txtConfigName.Focus();
        }
        private bool IsValidFormData(out string Message)
        {
            Message = string.Empty;
            string valRecTypes = txtRecordTypes.Text.Trim();
            if (valRecTypes.StartsWith(",")) { valRecTypes = valRecTypes.Remove(0, 1); }
            if (valRecTypes.EndsWith(",")) { valRecTypes = valRecTypes.Remove(valRecTypes.Length - 1, 1); }
            txtRecordTypes.Text = valRecTypes;
            if (string.IsNullOrEmpty(txtConfigName.Text))
            {
                Message += "Please enter a valid configuration name. The configuration name can be alphanumeric and cannot exceed more than 30 characters." + Environment.NewLine;
            }
            else
            {
                DataRow[] drc;
                if (Mode == Enums.FormMode.Add) { drc = dtSource.Select("ConfigurationName = '" + txtConfigName.Text.Trim() + "'"); }
                else { drc = dtSource.Select("SourceId <> " + EditSourceId + " And ConfigurationName = '" + txtConfigName.Text.Trim() + "'"); }
                if (drc.Count() > 0)
                {
                    Message += "Configuration name already exists. Please enter a unique configuration name." + Environment.NewLine;
                }
            }
            if (string.IsNullOrEmpty(txtFileExtensions.Text))
            {
                Message += "Please enter a relevant file extension pertaining to your configuration." + Environment.NewLine;
            }
            if (IsValidSubType() && string.IsNullOrEmpty(txtSubType.Text))
            {
                Message += "Please enter a relevant sub type matching with the metadata file." + Environment.NewLine;
            }
           
            if (!string.IsNullOrEmpty(txtFileExtensions.Text))
            {
                List<string> inputExtensions = Convert.ToString(txtFileExtensions.Text).Split(',').Select(p => p.Trim().ToUpper()).ToList();
                foreach (string item in inputExtensions)
                {
                    if (!AllowedFileExtensions.Contains(item.ToUpper()))
                    {
                        Message += "File Extension Target (" + item + ") is invalid. TXT  is allowed." + Environment.NewLine;
                        break;
                    }
                }
            }
           
              if (txtFileExtensions.Text == Convert.ToString(FileExtn.TXT))
            {
                if (string.IsNullOrEmpty(txtRecordTypes.Text))
                {
                    Message += "Invalid Record Type(s)" + Environment.NewLine;
                }
                else
                {
                 
                        if (!string.IsNullOrEmpty(txtFileExtensions.Text) && !string.IsNullOrEmpty(txtRecordTypes.Text))
                        {
                            //var _allowedFileExtensions = AllowedFileExtensions.Where(e => !e.ToString().Contains(Convert.ToString(FileExtn.ALCHEME))).Where(e => !e.ToString().Contains(Convert.ToString(FileExtn.FISERV))).ToList();
                            var _allowedFileExtensions = AllowedFileExtensions.ToList();
                            SortedList srcExtnList = new SortedList();
                            foreach (string ext in _allowedFileExtensions)
                            {
                                List<int> srcExtnRecTypes = new List<int>();
                                DataRow[] drc;
                                if (Mode == Enums.FormMode.Add)
                                {
                                drc = dtSource.Select("FileExtensionTargets like '%" + ext.ToUpper() + "%'");
                                 }
                                else
                                {
                                  drc = dtSource.Select("FileExtensionTargets like '%" + ext.ToUpper() + "%' And SourceId <> " + EditSourceId);
                                }
                                if (txtFileExtensions.Text != Convert.ToString(FileExtn.TXT))
                                {
                                    foreach (DataRow dr in drc)
                                    {
                                        srcExtnRecTypes.AddRange(Convert.ToString(dr["RecordType"]).Split(',').Select(p => Convert.ToInt32(p.Trim().ToUpper())).ToList());
                                    }
                                    srcExtnList.Add(ext, srcExtnRecTypes);
                                }
                            }

                            List<string> inputExtensions = Convert.ToString(txtFileExtensions.Text).Split(',').Select(p => p.Trim()).ToList();

                            var inputRecordArray = Convert.ToString(txtRecordTypes.Text).Trim().Split(',').Select(p => p).ToList();

                            List<int> inputRecordTypes = new List<int>();
                            if (cmbxFileType.Text != Convert.ToString(FileExtnType.PrintImageSD))
                            {
                                foreach (var e in inputRecordArray)
                                {
                                    if (int.TryParse(e, out int value))
                                    {
                                        inputRecordTypes.Add(value);
                                    }
                                    else
                                    {
                                        Message += "Invalid Record Type Input" + Environment.NewLine;
                                    }
                                }
                            }
                            SortedList inputExtnList = new SortedList();
                            foreach (string ext in inputExtensions)
                            {
                                inputExtnList.Add(ext, inputRecordTypes);
                            }
                            foreach (DictionaryEntry ext in inputExtnList)
                            {
                                List<int> inputRec = (List<int>)inputExtnList[ext.Key];
                                foreach (int recType in inputRec)
                                {
                                    List<int> srcRec = (List<int>)srcExtnList[ext.Key.ToString().ToUpper()];
                                    if (srcRec != null && srcRec.Contains(recType))
                                    {
                                      Message += string.Format("File extension ({0}) with record type ({1}) is already defined.{2}", ext.Key, recType, Environment.NewLine);
                                        break;
                                    }
                                }
                            }

                        }
                    
                }
            }

            return string.IsNullOrEmpty(Message);
        }
        private void SetMessageText(string Message, bool IsError = false)
        {
            lblMessage.ForeColor = IsError == true ? Color.Red : Color.Green;
            lblMessage.Text = Message;
        }
        private bool IsValidSubType()
        {
         
            return (txtFileExtensions.Text.Trim().ToUpper() == Convert.ToString(FileExtn.TXT));
        }


    }
}
