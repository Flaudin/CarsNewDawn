using CARS.Components.Transactions.Purchase_Return;
using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Transactions;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CARS.Components.Transactions.PurchaseReturn
{
    public partial class frm_purchase_return : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        PurchaseReturnController purchaseReturnController = new PurchaseReturnController();
        private SortedDictionary<string, string> _supplierDictionary = new SortedDictionary<string, string>();
        DataTable ReceivingList = new DataTable();
        DataTable ReceivingDet = new DataTable();
        PurchaseReturnModel purchaseReturnModel = new PurchaseReturnModel();
        private string msgResult = "";
        public frm_purchase_return(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlReceivingTable.BackColor =  Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblReceivingTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);

            _supplierDictionary = purchaseReturnController.getSuppliers();
            cmbSupplier.DataSource = new BindingSource(_supplierDictionary, null);
            cmbSupplier.DisplayMember = "Key";
            cmbSupplier.ValueMember = "Value";
            dashboardCall = DashboardCall;
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
        }

        

        private void dgvReceivingItemDet_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvReceivingItemDet.Columns["Reason"].Index && e.RowIndex != -1)
            {
                
                    frm_purchase_return_reason_selection reasonSelection = new frm_purchase_return_reason_selection();
                    reasonSelection.SelectedReason += reasonDisplay;
                    reasonSelection.ShowDialog(this);
            }
        }

        private void reasonDisplay(string selectedReason)
        {
            dgvReceivingItemDet.CurrentRow.Cells["Reason"].Value = selectedReason;
        }

        private void rrdisplay(string rrno)
        {
            txtRRNo.Textt = rrno;
        }

        private void txtRRNo_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("a");
        }

        private void txtRRNo_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("b");
        }

        private void txtRRNo_MouseDown(object sender, MouseEventArgs e)
        {
            frm_purchase_return_receive_report_selection rrSelection = new frm_purchase_return_receive_report_selection(cmbSupplier.SelectedValue.ToString().TrimEnd());
            rrSelection.RRSelected += rrdisplay;
            rrSelection.ShowDialog(this);
        }

        private void txtRRNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("c");
            }
        }

        private void BtnAddParts_Click(object sender, System.EventArgs e)
        {
            if(txtRRNo.Textt != "")
            {
                List<string> PartData = new List<string>();
                foreach (DataGridViewRow row in dgvReceivingItemDet.Rows)
                {
                    PartData.Add(row.Cells[dgvReceivingItemDet.Columns["PartNo"].Index].Value.ToString());
                }
                frm_purchase_return_parts_selection selectedParts = new frm_purchase_return_parts_selection(PartData,txtRRNo.Textt);
                selectedParts.StringArraySent += ReceiveArrayFromChild;
                selectedParts.ShowDialog(this);
            }
            else
            {
                Helper.Confirmator("No RR selected", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReceiveArrayFromChild(List<dynamic[]> stringArray)
        {
            for (int i = 0; i != stringArray.Count; i++)
            {
                DataGridViewRow row = dgvReceivingItemDet.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["PartNo"].Value?.ToString() == stringArray[i][0]);
                if (row == null)
                {
                    dgvReceivingItemDet.Rows.Add(stringArray[i][0], stringArray[i][1], stringArray[i][2], stringArray[i][3],
                                                            stringArray[i][4], stringArray[i][5], stringArray[i][6], stringArray[i][7]);
                }
            }
        }

        private void BtnDeleteParts_Click(object sender, System.EventArgs e)
        {
            if(dgvReceivingItemDet.Rows.Count > 0)
            {
                if (Helper.Confirmator("Are you sure you want to deleted the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    dgvReceivingItemDet.Rows.RemoveAt(dgvReceivingItemDet.CurrentRow.Index);
                }
            }
            else
            {
                Helper.Confirmator("No records to be deleted", "System Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvReceivingItemDet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvReceivingItemDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvReceivingItemDet.Columns["Qty"].Index && e.RowIndex != -1)
            {
                decimal returnQty = Convert.ToDecimal(dgvReceivingItemDet.Rows[e.RowIndex].Cells["Qty"].Value);
                decimal totalAmt = Convert.ToDecimal(dgvReceivingItemDet.Rows[e.RowIndex].Cells["Amount"].Value);
                decimal unitPrice = Convert.ToDecimal(dgvReceivingItemDet.Rows[e.RowIndex].Cells["UnitPrice"].Value);

                if (returnQty != 0)
                {
                    totalAmt = returnQty * unitPrice;
                    dgvReceivingItemDet.Rows[e.RowIndex].Cells["Amount"].Value = totalAmt;
                }

            }

        }

        private void customRoundedButton4_Click(object sender, EventArgs e)
        {
            if(txtRRNo.Textt != "" && dgvReceivingItemDet.Rows.Count > 0)
            {
                bool reasonChecker = dgvReceivingItemDet.Rows.Cast<DataGridViewRow>().All(row => row.Cells["Reason"].Value != null && !string.IsNullOrEmpty(row.Cells["Reason"].Value.ToString()));
                bool qtyChecker = dgvReceivingItemDet.Rows.Cast<DataGridViewRow>().All(row => row.Cells["Qty"].Value != null && !string.IsNullOrEmpty(row.Cells["Qty"].Value.ToString()));
                if(!reasonChecker)
                {
                    Helper.Confirmator("Please select a reason.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else if (!qtyChecker)
                {
                    Helper.Confirmator("Please input qty.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    List<PurchaseReturnDet> purchaseReturnDets = new List<PurchaseReturnDet>();
                    foreach(DataGridViewRow row in dgvReceivingItemDet.Rows)
                    {
                        if (row.Cells["Reason"].Value != null)
                        {
                            PurchaseReturnDet returnDet = new PurchaseReturnDet
                            {
                                PartNo = row.Cells["PartNo"].Value.ToString(),
                                Qty = Convert.ToDecimal(row.Cells["Qty"].Value.ToString()),
                                Status = "1",
                                ReasonID = purchaseReturnController.GetReasonID(row.Cells["Reason"].Value.ToString()),
                            };
                                purchaseReturnDets.Add(returnDet);
                        
                            purchaseReturnModel = new PurchaseReturnModel
                            {
                                purchaseReturnMains = new PurchaseReturnMain
                                {
                                    RRNo = txtRRNo.Textt,
                                    SupplierID = cmbSupplier.SelectedValue.ToString(),
                                    Remarks = "",
                                    Status = "1",
                                    ReasonID = "PE24040002"
                                },
                                purchaseReturnDets = purchaseReturnDets
                            };
                            msgResult = purchaseReturnController.SavePurchaseReturn(purchaseReturnModel);
                            Helper.Confirmator(msgResult, "Purchase Return Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (msgResult == "Save Purchase Return Successfully")
                            {
                                dgvReceivingItemDet.Rows.Clear();
                                cmbSupplier.SelectedIndex = 0;
                                txtRRNo.Textt = "";
                            }
                        }
                        else
                        {
                            Helper.Confirmator("Please select a reason", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void getReasonID(int rowIndex)
        {
            string reasonId = purchaseReturnController.GetReasonID(dgvReceivingItemDet.Rows[rowIndex].Cells["ReasonName"].Value.ToString());
        }

        private void customRoundedButton2_Click(object sender, EventArgs e)
        {
            if(Helper.Confirmator("Are you sure you want to clear this records.?","System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dgvReceivingItemDet.Rows.Clear();
                cmbSupplier.SelectedIndex = 0;
                txtRRNo.Textt = "";
                txtRRDate.Textt = "";
            }
        }

        private void dgvReceivingItemDet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (Convert.ToDecimal(dgvReceivingItemDet.Rows[e.RowIndex].Cells["Qty"].Value) > Convert.ToDecimal(dgvReceivingItemDet.Rows[e.RowIndex].Cells["TTLQtyRcvd"].Value))
            {
                Helper.Confirmator("Qty entered is invalid", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            var qty = dgvReceivingItemDet.Columns["Qty"].Index;
            if(e.ColumnIndex == qty)
            {
                if(!decimal.TryParse(e.FormattedValue.ToString(), out _))
                {
                    MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void dgvReceivingItemDet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(cell_KeyPress);
            if(dgvReceivingItemDet.CurrentCell.ColumnIndex == dgvReceivingItemDet.Columns["Qty"].Index)
            {
                e.Control.KeyPress += new KeyPressEventHandler(cell_KeyPress);
            }
        }

        private void cell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }
    }
}
