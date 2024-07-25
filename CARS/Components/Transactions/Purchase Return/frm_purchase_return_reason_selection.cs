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

namespace CARS.Components.Transactions.Purchase_Return
{
    public partial class frm_purchase_return_reason_selection : Form
    {
        private PurchaseReturnController purchaseReturnController = new PurchaseReturnController();
        private DataTable ReasonTable = new DataTable();
        public event Action<string> SelectedReason;
        public frm_purchase_return_reason_selection()
        {
            InitializeComponent();
            ReasonTable = purchaseReturnController.ReasonDisplay();
            dgvReason.DataSource = ReasonTable;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Reason selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        { 
            if(dgvReason.Rows.Count > 0)
            {
            string selectedReason = dgvReason.CurrentRow.Cells["ReasonName"].Value.ToString();
            SelectedReason?.Invoke(selectedReason);
            this.Close();
            }
        }
    }
}
