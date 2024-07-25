using CARS.Controller.Transactions;
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
    public partial class frm_purchase_return_receive_report_selection : Form
    {
        PurchaseReturnController purchaseReturnController = new PurchaseReturnController();
        DataTable RRTable = new DataTable();
        public event Action<string> RRSelected;
        string selectedrr = "";
        public frm_purchase_return_receive_report_selection(string slid)
        {
            InitializeComponent();
            RRTable = purchaseReturnController.ReceivingListDisplay(slid);
            dgvRecevingList.DataSource = RRTable;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(dgvRecevingList.Rows.Count > 0)
            {
                selectedrr = dgvRecevingList.CurrentRow.Cells["RRNo"].Value.ToString();
            }
            RRSelected?.Invoke(selectedrr.ToString());
            this.Close();
        }
    }
}
