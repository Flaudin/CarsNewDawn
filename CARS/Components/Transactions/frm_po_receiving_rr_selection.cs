using CARS.Controller.Transactions;
using CARS.Customized_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CARS.Components.Transactions
{
    public partial class frm_po_receiving_rr_selection : Form
    {
        ReceivingController receivingController;
        DataTable RRSelectionTable;
        public event Action<string,string,string,string> DataPassed;
        public frm_po_receiving_rr_selection()
        {
            InitializeComponent();
            receivingController = new ReceivingController();
            RRSelectionTable = new DataTable();
            dgvRRSelection.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.KeyPreview = true;
            txtRRSearch.KeyDown += new KeyEventHandler(txtRRSearch_KeyDown);
            dgvRRSelection.MultiSelect = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customRoundedButton1_Click(object sender, EventArgs e)
        {
            RRSelectionTable = receivingController.RRSelection(txtRRSearch.Textt.ToString().TrimEnd());
            if(RRSelectionTable.Rows.Count > 0)
            {
                dgvRRSelection.DataSource = RRSelectionTable;
                dgvRRSelection.Refresh();
            } else
            {
                MessageBox.Show("There are no RR found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(dgvRRSelection.Rows.Count > 0)
            {
                string selectedrr = dgvRRSelection.CurrentRow.Cells["RRNo"].Value.ToString();
                string supplierID = dgvRRSelection.CurrentRow.Cells["SupplierID"].Value.ToString().TrimEnd();
                string termID = dgvRRSelection.CurrentRow.Cells["TermID"].Value.ToString().TrimEnd();
                string termName = dgvRRSelection.CurrentRow.Cells["Term"].Value.ToString().TrimEnd();
                DataPassed?.Invoke(selectedrr, supplierID,termID,termName);
                this.Close();
            } else
            {
                MessageBox.Show("No RR No. selected", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
        }

        private void txtRRSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Call your action here
                customRoundedButton1_Click(sender,e);
                // Optionally, prevent the 'ding' sound
                e.SuppressKeyPress = true;
            }
        }
    }
}
