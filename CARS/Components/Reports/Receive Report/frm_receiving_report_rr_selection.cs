using CARS.Controller.Reports;
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

namespace CARS.Components.Reports.Receive_Report
{
    public partial class frm_receiving_report_rr_selection : Form
    {
        DataTable RRTable = new DataTable();
        ReceiveReportController receiveReportController;
        public event Action<string> SelectedRR;
        public frm_receiving_report_rr_selection()
        {
            InitializeComponent();
            receiveReportController = new ReceiveReportController();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
           if( Helper.Confirmator("Are you sure you want to close RR Selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            };
        }

        private void btnRRSearch_Click(object sender, EventArgs e)
        {
            RRTable = receiveReportController.RRSelection();
            dgvRRList.DataSource = RRTable;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgvRRList.Rows.Count > 0)
            {
                string selectedReason = dgvRRList.CurrentRow.Cells[0].Value.ToString();
                SelectedRR?.Invoke(selectedReason);
                this.Close();
            }
        }
    }
}
