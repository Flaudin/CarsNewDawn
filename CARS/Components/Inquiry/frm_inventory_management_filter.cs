using CARS.Controller.Inquiry;
using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Masterfiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Inquiry
{
    public partial class frm_inventory_management_filter : Form
    {
        private InventoryManagementController _InventoryManagementController = new InventoryManagementController();
        private DataTable PartsTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_inventory_management_filter()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Stock Adjustment Archive?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //PartsTable = _InventoryManagementController.PartsWithBegBalDataTable();
            //DataGridParts.DataSource = PartsTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                //TxtSANo.Textt = "";
                //DataTable dt = DataGridStockAdjustment.DataSource as DataTable;
                //if (dt != null)
                //{
                //    dt.Rows.Clear();
                //}

                //DataTable dtparts = DataGridParts.DataSource as DataTable;
                //if (dtparts != null)
                //{
                //    dtparts.Rows.Clear();
                //}
                //TxtSANo.Focus();
            }
        }

        private void DataGridStockAdjustment_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //PartsTable = _StockAdjustmentController.PartsDataTable(DataGridStockAdjustment.Rows[e.RowIndex].Cells["AdjNo"].Value.ToString());
            //DataGridParts.DataSource = PartsTable;
            //DataGridParts.ClearSelection();
        }

        private void frm_beginning_balance_archive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose.PerformClick();
            }
        }
    }
}
