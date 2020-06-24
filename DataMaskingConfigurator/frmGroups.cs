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
    public partial class frmGroups : Form
    {
        enum GroupEnum
        {
            Product,
            Workflow
        }

        private bool HasRecordChanged;
        private MaskingSource MaskingSource { get; set; }
        private DataTable dtWorkflow { get { return MaskingSource.Tables["Workflow"]; } }
        private DataTable dtProduct { get { return MaskingSource.Tables["Product"]; } }
        private DataTable dtConfiguration { get { return MaskingSource.Tables["Configuration"]; } }

        private GroupEnum groupMode { get; set; }


        public frmGroups(MaskingSource maskingSource)
        {
            InitializeComponent();
            this.MaskingSource = maskingSource;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = HasRecordChanged == true ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }
        private void frmGroups_Load(object sender, EventArgs e)
        {
            SetMessageText(string.Empty);
            rdbWorkflow.Checked = true;
            LoadFormStartupData(GroupEnum.Workflow);
        }


        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string _message = string.Empty;
            if (IsValidFormData(out _message))
            {
                if (groupMode == GroupEnum.Workflow)
                {
                    DataRow dr = dtWorkflow.NewRow();
                    dr["WorkflowVal"] = txtGroupValue.Text.Trim();
                    dtWorkflow.Rows.Add(dr);
                    SetMessageText("Added new workflow");
                    HasRecordChanged = true;
                    LoadFormStartupData(GroupEnum.Workflow);
                }
                else if (groupMode == GroupEnum.Product)
                {
                    DataRow dr = dtProduct.NewRow();
                    dr["ProductVal"] = txtGroupValue.Text.Trim();
                    dtProduct.Rows.Add(dr);
                    SetMessageText("Added new product");
                    HasRecordChanged = true;
                    LoadFormStartupData(GroupEnum.Product);
                }
                this.DialogResult = DialogResult.None;
            }
            else
            {
                SetMessageText(_message, true);
                this.DialogResult = DialogResult.None;
            }
        }
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            DialogResult dr = MessageBox.Show("Are you sure you want to delete selected record(s)?", Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                if (groupMode == GroupEnum.Workflow)
                {
                    foreach (DataRowView itm in chkListGroup.CheckedItems.OfType<DataRowView>().ToList())
                    {
                        string item = Convert.ToString(itm["WorkflowVal"]);

                        if (dtConfiguration.Select("Workflow = '" + item + "'").FirstOrDefault() != null)
                        {
                            MessageBox.Show("The record " + item + " could not be deleted because of association. To delete this record you need to remove this from all associated record(s)", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (!item.ToLower().Equals("default"))
                            {
                                dtWorkflow.Select("WorkflowVal = '" + item + "'").FirstOrDefault().Delete();
                                HasRecordChanged = true;
                            }
                        }
                    }
                }
                else if (groupMode == GroupEnum.Product)
                {
                    foreach (DataRowView itm in chkListGroup.CheckedItems.OfType<DataRowView>().ToList())
                    {
                        string item = Convert.ToString(itm["ProductVal"]);
                        if (dtConfiguration.Select("Product = '" + item + "'").FirstOrDefault() != null)
                        {
                            MessageBox.Show("The record " + item + " could not be deleted because of association. To delete this record you need to remove this from all associated record(s)", Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (!item.ToLower().Equals("default"))
                            {
                                dtProduct.Select("ProductVal = '" + item + "'").FirstOrDefault().Delete();
                                HasRecordChanged = true;
                            }
                        }
                    }
                }
            }
            LoadFormStartupData(groupMode);
            this.DialogResult = DialogResult.None;
        }
        private void chkListGroup_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string val = string.Empty;
            if (groupMode == GroupEnum.Workflow)
            { val = Convert.ToString(((DataRowView)chkListGroup.Items[e.Index])["WorkflowVal"]); }
            else if (groupMode == GroupEnum.Product)
            { val = Convert.ToString(((DataRowView)chkListGroup.Items[e.Index])["ProductVal"]); }
            if (val.ToLower().Equals("default")) { e.NewValue = CheckState.Unchecked; }

            btnDeleteGroup.Enabled = (
                (chkListGroup.CheckedItems.Count <= 1 && e.NewValue == CheckState.Checked)
                || (chkListGroup.CheckedItems.Count > 1)
                ) ? true : false;
        }
        private void rdbWorkflow_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbWorkflow.Checked)
            {
                BindGroups(GroupEnum.Workflow);
            }
        }
        private void rdbProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProduct.Checked)
            {
                BindGroups(GroupEnum.Product);

            }
        }


        private void LoadFormStartupData(GroupEnum groupEnum)
        {
            txtGroupValue.Text = string.Empty;
            BindGroups(groupEnum);
        }
        private void SetMessageText(string Message, bool IsError = false)
        {
            lblMessage.ForeColor = IsError == true ? Color.Red : Color.Green;
            lblMessage.Text = Message;
        }
        private void BindGroups(GroupEnum groupEnum)
        {
            btnAddGroup.Text = string.Format("Add {0}", groupEnum.ToString());
            btnDeleteGroup.Text = string.Format(" Delete selected {0}(s)", groupEnum.ToString().ToLower());
            lblGroupValue.Text = string.Format("{0}", groupEnum.ToString());

            switch (groupEnum)
            {
                case GroupEnum.Product:
                    chkListGroup.DataSource = dtProduct;
                    chkListGroup.DisplayMember = "ProductVal";
                    chkListGroup.ValueMember = "ProductId";
                    break;
                case GroupEnum.Workflow:
                    chkListGroup.DataSource = dtWorkflow;
                    chkListGroup.DisplayMember = "WorkflowVal";
                    chkListGroup.ValueMember = "WorkflowId";
                    break;
                default:
                    break;
            }

            groupMode = groupEnum;
            chkListGroup.ClearSelected();
            btnDeleteGroup.Enabled = chkListGroup.CheckedItems.Count > 0 ? true : false;
        }
        private bool IsValidFormData(out string Message)
        {
            Message = string.Empty;
            if (string.IsNullOrEmpty(txtGroupValue.Text.Trim()))
            {
                Message += "Invalid value provided" + Environment.NewLine;
            }
            else
            {
                if (groupMode == GroupEnum.Workflow
                    && dtWorkflow.Select("WorkflowVal = '" + txtGroupValue.Text.Trim() + "'").Length > 0)
                {

                    Message += "Workflow already exists" + Environment.NewLine;
                }
                else if (groupMode == GroupEnum.Product
                    && dtProduct.Select("ProductVal = '" + txtGroupValue.Text.Trim() + "'").Length > 0)
                {
                    Message += "Product already exists" + Environment.NewLine;
                }
            }
            return string.IsNullOrEmpty(Message);
        }

    }
}
