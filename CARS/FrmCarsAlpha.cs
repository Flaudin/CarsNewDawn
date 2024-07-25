using CARS.Components.Masterfiles;
using CARS.Components.Transactions;
using CARS.Components.Transactions.BeginningBalance;
using CARS.Components.Transactions.PurchaseReturn;
using CARS.Components.Transactions.SalesOrder;
using CARS.Components.Transactions.SalesReturn;
using CARS.Components.Transactions.StockAdjustment;
using CARS.Components.Transactions.StockTransfer;
using CARS.Components.Utilities;
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

namespace CARS
{
    public partial class FrmCarsAlpha : Form
    {
        public FrmCarsAlpha()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm() {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (button.Name)
            {
                case "btn_transactions":
                    DropdownEvent(flow_transaction, timer_transactions);
                    //frm_transaction frm = new frm_transaction();
                    //frm.TopLevel = false;
                    //pnl_body.Controls.Add(frm);
                    //frm.Show();
                    break;
                case "btn_masterfiles":
                    DropdownEvent(flow_masterfiles, timer_masterfiles);
                    break;
                case "btn_inquiry":
                    DropdownEvent(flow_inquiry, timer_inquiry);
                    break;
                case "btn_reports":
                    DropdownEvent(flow_reports, timer_reports);
                    break;
                case "btn_utilities":
                    DropdownEvent(flow_utilities, timer_utilities);
                    break;
                case "btn_help":
                    DropdownEvent(flow_help, timer_help);
                    break;
            }
        }

        private void timer_transactions_Tick(object sender, EventArgs e)
        {
            DropdownTimer(timer_transactions, flow_transaction, 510, 50);
        }

        private void timer_masterfiles_Tick(object sender, EventArgs e)
        {
            DropdownTimer(timer_masterfiles, flow_masterfiles, 420, 62);
        }

        private void timer_inquiry_Tick(object sender, EventArgs e)
        {
            DropdownTimer(timer_inquiry, flow_inquiry, 110, 22);
        }

        private void timer_reports_Tick(object sender, EventArgs e)
        {
            DropdownTimer(timer_reports, flow_reports, 210, 70);
        }

        private void timer_utilities_Tick(object sender, EventArgs e)
        {
            DropdownTimer(timer_utilities, flow_utilities, 110, 50);
        }

        private void timer_help_Tick(object sender, EventArgs e)
        {
            DropdownTimer(timer_help, flow_help, 80, 20);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //foreach (Control ctrl in flow_menu.Controls)
            //{
            //    if (ctrl.GetType() == typeof(FlowLayoutPanel) && ctrl.Size.Height > 10)
            //    {
            //        DropdownTimer2(ctrl.Tag as Timer, ctrl as FlowLayoutPanel, 20);
            //    }
            //}
        }

        //Functions
        private static void DropdownEvent(FlowLayoutPanel flow, Timer tmr)
        {
            if (flow.Size.Height != 10)
            {
                flow.Size = new Size(267, 10);
                flow.Visible = false;
            }
            else
            {
                flow.Visible = true;
                tmr.Start();
            }
        }

        private static void DropdownTimer(Timer tmr, FlowLayoutPanel flow, int total, int difference)
        {
            if (flow.Size.Height < total)
            {
                flow.Size = new Size(flow.Size.Width, flow.Size.Height + difference);
            }
            else
            {
                tmr.Stop();
            }
        }

        private void Transaction_SubMenu_Click(object sender, EventArgs e)
        {
            pnl_body.Controls.Clear();
            var button = sender as Button;
            switch (button.Name)
            {
                case "btn_po_monitoring":
                    frm_po_monitoring po_Monitoring = new frm_po_monitoring(DashboardCall);
                    CallSubForm(po_Monitoring, pnl_body);
                    break;
                case "btn_transac_po_generate":
                    frm_po_generation po_Generation = new frm_po_generation();
                    CallSubForm(po_Generation, pnl_body);
                    break;
                case "btnRecieving":
                    frm_receiving receiving = new frm_receiving(DashboardCall);
                    CallSubForm(receiving, pnl_body);
                    break;
                case "btnPurchaseRet":
                    frm_purchase_return purchase_Return = new frm_purchase_return(DashboardCall);
                    CallSubForm(purchase_Return, pnl_body);
                    break;
                case "btnPriceManage":
                    frm_price_management price_Management = new frm_price_management(DashboardCall);
                    CallSubForm(price_Management, pnl_body);
                    break;
                case "btnSOEntry":
                    frm_sales_order_entry soEntry = new frm_sales_order_entry(DashboardCall);
                    CallSubForm(soEntry, pnl_body);
                    break;
                case "btnReceiptGen":
                    frm_receipt_generation receipt_Generation = new frm_receipt_generation(DashboardCall);
                    CallSubForm(receipt_Generation, pnl_body);
                    break;
                case "btnSalesRet":
                    frm_sales_return sales_Return = new frm_sales_return(DashboardCall);
                    CallSubForm(sales_Return, pnl_body);
                    break;
                case "btnBeginBal":
                    frm_beginning_balance beginning_Balance = new frm_beginning_balance(DashboardCall);
                    CallSubForm(beginning_Balance, pnl_body);
                    break;
                case "btnStockTrans":
                    frm_stock_transfer stock_Transfer = new frm_stock_transfer(DashboardCall);
                    CallSubForm(stock_Transfer, pnl_body);
                    break;
                case "btnStockAdj":
                    frm_stock_adjustment stock_Adjustment = new frm_stock_adjustment(DashboardCall);
                    CallSubForm(stock_Adjustment, pnl_body);
                    break;
                case "btnDelRecVer":
                    frm_delivery_receipt_verification delivery_Receipt_Verification = new frm_delivery_receipt_verification();
                    CallSubForm(delivery_Receipt_Verification, pnl_body);
                    break;
                case "btnPayEntry":
                    frm_payment_entry payment_Entry = new frm_payment_entry();
                    CallSubForm(payment_Entry, pnl_body);
                    break;
                case "btnCustDepo":
                    frm_customer_deposits customer_Deposits = new frm_customer_deposits();
                    CallSubForm(customer_Deposits, pnl_body);
                    break;
                case "btnBillState":
                    frm_billing_statement billing_Statement = new frm_billing_statement();
                    CallSubForm(billing_Statement, pnl_body);
                    break;
                case "btnCorFinAdj":
                    frm_corporation_finance_adjustment corporation_Finance_Adjustment = new frm_corporation_finance_adjustment();
                    CallSubForm(corporation_Finance_Adjustment, pnl_body);
                    break;
                case "btnCusBegBal":
                    frm_customer_beginning_balance customer_Beginning_Balance = new frm_customer_beginning_balance();
                    CallSubForm(customer_Beginning_Balance, pnl_body);
                    break;
            }
        }

        private void Masterfiles_SubMenu_Click(object sender, EventArgs e)
        {
            pnl_body.Controls.Clear();
            var button = sender as Button;
            switch (button.Name)
            {
                case "BtnWarehouseMaster":
                    frm_warehouse warehouse_master = new frm_warehouse(DashboardCall);
                    CallSubForm(warehouse_master, pnl_body);
                    break;
                case "BtnBinLocationMaster":
                    frm_bin_location bin_master = new frm_bin_location(DashboardCall);
                    CallSubForm(bin_master, pnl_body);
                    break;
                case "BtnCustomerMaster":
                    frm_customer customer_master = new frm_customer(DashboardCall);
                    CallSubForm(customer_master, pnl_body);
                    break;
                case "BtnSupplierMaster":
                    frm_supplier supplier_master = new frm_supplier(DashboardCall);
                    CallSubForm(supplier_master, pnl_body);
                    break;
                case "BtnSukiMaster":
                    frm_suki_card suki_master = new frm_suki_card();
                    CallSubForm(suki_master, pnl_body);
                    break;
                case "BtnPositionMaster":
                    //SALESMAN IS CHANGED TO POSITION
                    //frm_salesman salesman_master = new frm_salesman();
                    //CallSubForm(salesman_master, pnl_body);
                    break;
                case "BtnReasonMaster":
                    frm_reason reason_master = new frm_reason(DashboardCall);
                    CallSubForm(reason_master, pnl_body);
                    break;
                case "BtnRegionMaster":
                    frm_region region_master = new frm_region(DashboardCall);
                    CallSubForm(region_master, pnl_body);
                    break;
                case "BtnProvinceMaster":
                    frm_province province_master = new frm_province(DashboardCall);
                    CallSubForm(province_master, pnl_body);
                    break;
                case "BtnCityMaster":
                    frm_city city_master = new frm_city(DashboardCall);
                    CallSubForm(city_master, pnl_body);
                    break;
                case "BtnCreditTermMaster":
                    frm_term term_master = new frm_term(DashboardCall);
                    CallSubForm(term_master, pnl_body);
                    break;
                case "BtnPartsMaster":
                    frm_parts parts_master = new frm_parts(DashboardCall);
                    CallSubForm(parts_master, pnl_body);
                    break;
                case "BtnBrandMaster":
                    frm_brand brand_master = new frm_brand(DashboardCall);
                    CallSubForm(brand_master, pnl_body);
                    break;
                case "BtnDescriptionMaster":
                    frm_description description_master = new frm_description(DashboardCall);
                    CallSubForm(description_master, pnl_body);
                    break;
                case "BtnMeasurementMaster":
                    frm_unit_measurement measurement_master = new frm_unit_measurement(DashboardCall);
                    CallSubForm(measurement_master, pnl_body);
                    break;
            }
        }

        private void Inquiry_SubMenu_Click(object sender, EventArgs e)
        {
            pnl_body.Controls.Clear();
            var button = sender as Button;
            switch (button.Name)
            {
                case "BtnPartsInquiry":
                    frm_parts_inquiry parts_inquiry = new frm_parts_inquiry(DashboardCall);
                    CallSubForm(parts_inquiry, pnl_body);
                    break;
                case "BtnInventoryManagement":
                    frm_inventory_management inventory_management = new frm_inventory_management(DashboardCall);
                    CallSubForm(inventory_management, pnl_body);
                    break;
                case "BtnInventoryAuditTrail":
                    //frm_customer_inquiry customer_inquiry = new frm_customer_inquiry();
                    //CallSubForm(customer_inquiry, pnl_body);
                    //break;
                case "BtnSalesOrderArchive":
                    frm_sales_order_archive so_archive = new frm_sales_order_archive(DashboardCall);
                    CallSubForm(so_archive, pnl_body);
                    break;
            }
        }

        private void Reports_SubMenu_Click(object sender, EventArgs e)
        {
            pnl_body.Controls.Clear();
            var button = sender as Button;
            switch (button.Name)
            {
                case "BtnSalesReports":
                    frm_sales_report sales_report = new frm_sales_report(DashboardCall);
                    CallSubForm(sales_report, pnl_body);
                    break;
                case "BtnSalesReturnReports":
                    frm_sales_return_report sales_return_report = new frm_sales_return_report(DashboardCall);
                    CallSubForm(sales_return_report, pnl_body);
                    break;
                case "BtnInventoryReports":
                    frm_inventory_report inventory_report = new frm_inventory_report();
                    CallSubForm(inventory_report, pnl_body);
                    break;
                case "BtnReceivingReports":
                    frm_receiving_report receiving_report = new frm_receiving_report();
                    CallSubForm(receiving_report, pnl_body);
                    break;
                case "BtnMonthlyInventoryReports":
                    frm_monthly_inventory_report inventory_monthly_report = new frm_monthly_inventory_report();
                    CallSubForm(inventory_monthly_report, pnl_body);
                    break;
                case "BtnTransmittalReports":
                    frm_transmittal transmittal = new frm_transmittal();
                    CallSubForm(transmittal, pnl_body);
                    break;
                case "BtnDailyTransactionReports":
                    frm_daily_transaction_report daily_transaction_report = new frm_daily_transaction_report(DashboardCall);
                    CallSubForm(daily_transaction_report, pnl_body);
                    break;
                //case "btn_cf_sales_reports":
                //    frm_cf_sales_report cf_report = new frm_cf_sales_report();
                //    CallSubForm(cf_report, pnl_body);
                //    break;
            }
        }

        private void Utilities_SubMenu_Click(object sender, EventArgs e)
        {
            pnl_body.Controls.Clear();
            var button = sender as Button;
            switch (button.Name)
            {
                case "BtnUserProfile":
                    frm_user_profile user_profile = new frm_user_profile(DashboardCall);
                    CallSubForm(user_profile, pnl_body);
                    break;
                case "BtnCountSheet":
                    frm_count_sheet count_sheet = new frm_count_sheet();
                    CallSubForm(count_sheet, pnl_body);
                    break;
            }
        }

        private static void CallSubForm(Form frm, Panel pnl)
        {
            frm.TopLevel = false;
            pnl.Controls.Add(frm);
            frm.Show();
        }

        private void DashboardCall()
        {
            FrmDashBoard dash = new FrmDashBoard();
            CallSubForm(dash, pnl_body);
        }

        private void SubmenuMouseEnter(object sender, EventArgs e)
        {
            var submenu = sender as Button;
            submenu.ForeColor = Color.FromArgb(5, 33, 66);
        }

        private void SubmenuMouseLeave(object sender, EventArgs e)
        {
            var submenu = sender as Button;
            submenu.ForeColor = Color.White;
        }
    }
}
