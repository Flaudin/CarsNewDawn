using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Transactions;
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
    public partial class frm_po_receiving_loc_selection : Form
    {
        ReceivingController receivingController;
        DataTable LocationsTable;
        private string partN = "";
        public event EventHandler<DataRowEventArgs> RowSelected;
        public string whereclause = "";
        public frm_po_receiving_loc_selection(string partNo, string listbinloc)
        {
            InitializeComponent();
            receivingController = new ReceivingController();
            LocationsTable = new DataTable();
            whereclause = listbinloc;
            //LocationsTable = receivingController.PODisplay(partNo);
            dgvLocaitonSelector.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocaitonSelector.MultiSelect = false;
            dgvLocaitonSelector.DataSource = LocationsTable;
            partN = partNo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void customRoundedButton1_Click(object sender, EventArgs e)
        {
            LocationsTable = receivingController.SearchLocation(partN, txtsearchLoc.Textt, whereclause);
            dgvLocaitonSelector.DataSource = LocationsTable;
            dgvLocaitonSelector.Refresh();
        }

        private void txtsearchLoc_Enter(object sender, EventArgs e)
        {
           //LocationsTable = receivingController.SearchLocation();
           //dgvLocaitonSelector.DataSource = LocationsTable;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //string selectedLoc = "";
            if(dgvLocaitonSelector.SelectedRows.Count > 0)
            {
                DataRowView datarowview = (DataRowView)dgvLocaitonSelector.SelectedRows[0].DataBoundItem;
                DataRow selectedLoc = datarowview.Row;
                /*string binid = dgvLocaitonSelector.CurrentRow.Cells["RRNo"].Value.ToString();
                string binname = dgvLocaitonSelector.CurrentRow.Cells["SupplierID"].Value.ToString().TrimEnd();
                string warehouseid = dgvLocaitonSelector.CurrentRow.Cells["TermID"].Value.ToString().TrimEnd();
                string warehousename = dgvLocaitonSelector.CurrentRow.Cells["TermName"].Value.ToString().TrimEnd();*/
                //DataPassed?.Invoke(selectedrr, supplierID, termID, termName);
                //this.Close();
                RowSelected?.Invoke(this, new DataRowEventArgs(selectedLoc));
                this.Close();
            }
            //foreach (DataGridViewRow rows in dgvLocaitonSelector.Rows)
            //{
            //    if (rows.Selected)
            //    {
            //        selectedLoc = rows.Cells["Location"].Value.ToString();
            //    }
            //}
            //List<dynamic[]> sssz = new List<dynamic[]>();
            //foreach(DataRow row in LocationsTable.Rows)
            //{
            //    if (dgvLocaitonSelector.CurrentRow.) { }
            //}
        }

    }
}
