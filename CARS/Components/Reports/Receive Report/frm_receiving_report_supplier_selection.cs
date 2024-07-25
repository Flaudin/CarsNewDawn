using CARS.Controller.Reports;
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
    public partial class frm_receiving_report_supplier_selection : Form
    {
        DataTable RRTable = new DataTable();
        ReceiveReportController receiveReportController;
        public frm_receiving_report_supplier_selection()
        {
            InitializeComponent();
            receiveReportController = new ReceiveReportController();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
