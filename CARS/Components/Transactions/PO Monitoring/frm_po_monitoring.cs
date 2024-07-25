using CARS.Components.Transactions.SupplierQuotation;
using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_po_monitoring : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        DataTable PODetTable = new DataTable();
        DataTable POItemTable = new DataTable();
        POMonitoring poController = new POMonitoring();

        public frm_po_monitoring(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlPOTable.BackColor = PnlPODetails.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblPOTable.ForeColor = LblPODetails.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            getPODetails();
            if (POItemTable.Rows.Count >= 0)
            {
                foreach (DataGridViewRow row in dgvPOItemDet.Rows)
                {
                    decimal discountPerce = Convert.ToDecimal(row.Cells["NetPrice"].Value) / Convert.ToDecimal(row.Cells["UnitPrice"]);
                    row.Cells["DiscountPercent"].Value = discountPerce.ToString();
                }
            }
            dgvPODetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPODetails.MultiSelect = false;
            dgvPOItemDet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPOItemDet.MultiSelect = false;
            dashboardCall = DashboardCall;
        }

        private void customRoundedButton3_Click(object sender, System.EventArgs e)
        {
            frm_create_po frm_Create_Po = new frm_create_po();
            frm_Create_Po.ShowDialog(this);
        }

        private void customRoundedButton6_Click(object sender, System.EventArgs e)
        {
            frm_supplier_quotation frm_Supplier_Quotation = new frm_supplier_quotation();
            frm_Supplier_Quotation.ShowDialog(this);
        }

        private void getPODetails()
        {
            POItemTable.Clear();
            PODetTable = poController.PoOrderDet();
            dgvPODetails.DataSource = PODetTable;
        }


        private void customRoundedButton4_Click(object sender, System.EventArgs e)
        {
            PODetTable.Rows.Clear();
            POItemTable.Rows.Clear();
            getPODetails();
            //if (POItemTable.Rows.Count >= 0)
            //{
            //    foreach (DataGridViewRow row in dgvPOItemDet.Rows)
            //    {
            //        decimal discountPerce = Convert.ToDecimal(row.Cells["NetPrice"].Value) / Convert.ToDecimal(row.Cells["UnitPrice"]);
            //        row.Cells["DiscountPercent"].Value = discountPerce.ToString();
            //    }
            //}
        }

        private void dgvPODetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //PODetTable.Rows.Clear();
            //POItemTable.Rows.Clear();
            string poNo = dgvPODetails.Rows[e.RowIndex].Cells["PONo"].Value.ToString().TrimEnd();
            POItemTable = poController.PoOrderItemDet(poNo);
            dgvPOItemDet.DataSource = POItemTable;
            dgvPOItemDet.ClearSelection();
        }

        private void btnClosePO_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close this PO?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                string msgResult = poController.ClosePO(dgvPODetails.CurrentRow.Cells["PONo"].Value.ToString());
                if (msgResult == "Successfully close PO")
                {
                    getPODetails();
                }
            }
        }

        private void btnCancelPO_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to cancel this PO?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                string msgResult = poController.CancelPO(dgvPODetails.CurrentRow.Cells["PONo"].Value.ToString());
                if (msgResult == "Successfully cancel PO")
                {
                    getPODetails();
                }
            }
        }

        private void dgvPOItemDet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var col = dgvPOItemDet.Columns["NetPrice"];
            if (col == null) return; // Handle case where the column is not found

            int sum = dgvPOItemDet.Rows
                         .Cast<DataGridViewRow>()
                         .Where(row => row.Cells[col.Index].Value != null)
                         .Sum(row => int.TryParse(row.Cells[col.Index].Value.ToString(), out int cellValue) ? cellValue : 0);

            txtPOAmt.Textt = sum.ToString();
        }

        private string statuses(int stats)
        {
            switch (stats)
            {
                case 1:
                    return "Active";
                case 2:
                    return "Save";
                case 8:
                    return "Close";
                case 9:
                    return "Cancel";
                default:
                    return "Unknown";
            }
        }

        //private Color statusColors(int stats)
        //{
        //    switch (stats)
        //    {
        //        case 1:
        //            return 
        //    }
        //}

        private void dgvPODetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPODetails.Columns[e.ColumnIndex].Name == "Status")
            {
                if (e.Value != null)
                {
                    int statusCode = Convert.ToInt32(e.Value);
                    e.Value = statuses(statusCode);
                    e.FormattingApplied = true;

                }
            }
        }

        private void dgvPODetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var col = dgvPOItemDet.Columns["NetPrice"];
            if (col == null) return; // Handle case where the column is not found

            int sum = dgvPOItemDet.Rows
                         .Cast<DataGridViewRow>()
                         .Where(row => row.Cells[col.Index].Value != null)
                         .Sum(row => int.TryParse(row.Cells[col.Index].Value.ToString(), out int cellValue) ? cellValue : 0);

            txtPOAmt.Textt = sum.ToString();
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
