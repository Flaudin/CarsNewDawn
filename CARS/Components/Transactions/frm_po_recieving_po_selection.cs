using CARS.Controller.Transactions;
using CARS.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions.PORecieving
{
    public partial class frm_po_recieving_po_selection : Form
    {
        DataTable POTable = new DataTable();
        POMonitoring poController = new POMonitoring();
        private string selectedPoNo = "";
        private frm_receiving _recieving;

        public frm_po_recieving_po_selection(frm_receiving recieving)
        {
            InitializeComponent();
            _recieving = recieving;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtSearch.Textt = "";
                txtPOSearch.Textt = "";
                POTable.Clear();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            POTable.Clear();
            POTable = poController.PoOrderDet();
            dgvPO.DataSource = POTable;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(dgvPO.RowCount != 0)
            {
            selectedPoNo = dgvPO.CurrentRow.Cells["PONo"].Value.ToString().Trim();
                _recieving.ReceivedData(selectedPoNo);
                this.Close();
            }
            else
            {
                Helper.Confirmator("Please search PO first.", "No PO found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
