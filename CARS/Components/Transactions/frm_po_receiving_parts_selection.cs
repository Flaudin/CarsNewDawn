using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Masterfiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_po_receiving_parts_selection : Form
    {
        ReceivingController receivingController;
        DataTable partsTable = new DataTable();
        PartsModel partsModel = new PartsModel();
        string selectedParts = "";
        private frm_receiving _receiving;
        public frm_po_receiving_parts_selection(frm_receiving receiving)
        {
            InitializeComponent();
            receivingController = new ReceivingController();
            _receiving = receiving;
        }

        


        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            partsModel = new PartsModel { Description = txtDescription.Textt, Brand = txtBrand.Textt };
            partsTable = receivingController.PartsSelection(partsModel, txtSearch.Textt, rdbtnBsb.Checked, rdbtnCritItems.Checked);
            dgvParts.DataSource = partsTable;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to clear?","System Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtBrand.Textt = "";
                txtDescription.Textt = "";
                txtSearch.Textt = "";
                rdbtnBsb.Checked = false;
                rdbtnCritItems.Checked = false;
                partsTable.Clear();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(dgvParts.RowCount != 0)
            {
                selectedParts = dgvParts.CurrentRow.Cells["PartNo"].Value.ToString().Trim();
                _receiving.ReceivedData(selectedParts);
                this.Close();
            }
            else
            {
                Helper.Confirmator("Please select Part No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
