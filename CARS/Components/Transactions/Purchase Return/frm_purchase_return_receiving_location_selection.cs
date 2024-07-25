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
    public partial class frm_purchase_return_receiving_location_selection : Form
    {
        DataTable LocationTable = new DataTable();
        PurchaseReturnController purchaseReturnController = new PurchaseReturnController();
        public frm_purchase_return_receiving_location_selection(string partno,string rrno)
        {
            InitializeComponent();
            LocationTable = purchaseReturnController.LocationSelectionDisplay(partno, rrno);
            dgvLocationSelection.DataSource = LocationTable;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if(Helper.Confirmator("Are you sure you want to close this location selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }
    }
}
