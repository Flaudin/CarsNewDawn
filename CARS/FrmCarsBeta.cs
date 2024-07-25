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
using CARS.Model;
using CARS.Model.Utilities;
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
    public partial class FrmCarsBeta : Form
    {
        private ColorManager _ColorManager = new ColorManager();

        public FrmCarsBeta()
        {
            InitializeComponent();
            InitializeForm();
            PnlMenuBar.BackColor = PnlVersion.BackColor = Menu.BackColor = Color.FromArgb(_ColorManager.HeaderRGB[0], _ColorManager.HeaderRGB[1], _ColorManager.HeaderRGB[2]);
            PnlBanner.BackColor = LblUser.ForeColor = BtnTransaction.ForeColor = BtnInquiry.ForeColor = BtnMasterfile.ForeColor = BtnReport.ForeColor =
                BtnUtility.ForeColor = BtnMinimize.ForeColor = BtnExit.ForeColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            var allItems = Menu.Items.OfType<ToolStripMenuItem>()
                           .SelectMany(item => item.DropDownItems.OfType<ToolStripItem>());

            foreach (var subItem in allItems)
            {
                subItem.ForeColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Update the label text with the current date
            LblUser.Text = Universal<ColorManager>.Name01;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            DashboardCall();
        }

        private void InitializeForm()
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the label text with the current time every second
            labelTime.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy hh:mm:ss tt");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to exit CARS?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.FrmCarsBeta_FormClosing);
                Application.Exit();
            }
        }

        private void Transaction_SubMenu_Click(object sender, EventArgs e)
        {
            if (Helper.MenuConfirmator(pnl_body.Controls.Count, pnl_body.Controls.Find("PictureDashboard", true).Count(), "Are you sure you want to switch modules?\nAll unsaved changes will be discarded.", "System Confirmation"))
            {
                pnl_body.Controls.Clear();
                var button = sender as ToolStripMenuItem;
                switch (button.Name)
                {
                    case "btn_transact_order_taking":
                        frm_order_taking order_Taking = new frm_order_taking(DashboardCall);
                        CallSubForm(order_Taking, pnl_body, true);
                        break;
                    case "btn_transact_order_consolidation":
                        frm_order_consolidation order_Consolidation = new frm_order_consolidation(DashboardCall);
                        CallSubForm(order_Consolidation, pnl_body, true);
                        break;
                    case "btn_po_monitoring":
                        frm_po_monitoring po_Monitoring = new frm_po_monitoring(DashboardCall);
                        CallSubForm(po_Monitoring, pnl_body, true);
                        break;
                    case "btnRecieving":
                        frm_receiving receiving = new frm_receiving(DashboardCall);
                        CallSubForm(receiving, pnl_body, true);
                        break;
                    case "btnPurchaseRet":
                        frm_purchase_return purchase_Return = new frm_purchase_return(DashboardCall);
                        CallSubForm(purchase_Return, pnl_body, true);
                        break;
                    case "btnPriceManage":
                        frm_price_management price_Management = new frm_price_management(DashboardCall);
                        CallSubForm(price_Management, pnl_body, true);
                        break;
                    case "btnSOEntry":
                        frm_sales_order_entry soEntry = new frm_sales_order_entry(DashboardCall);
                        CallSubForm(soEntry, pnl_body, true);
                        break;
                    case "btnReceiptGen":
                        frm_receipt_generation receipt_Generation = new frm_receipt_generation(DashboardCall);
                        CallSubForm(receipt_Generation, pnl_body, true);
                        break;
                    case "btnSalesRet":
                        frm_sales_return sales_Return = new frm_sales_return(DashboardCall);
                        CallSubForm(sales_Return, pnl_body, true);
                        break;
                    case "btnBeginBal":
                        frm_beginning_balance beginning_Balance = new frm_beginning_balance(DashboardCall);
                        CallSubForm(beginning_Balance, pnl_body, true);
                        break;
                    case "btnStockTrans":
                        frm_stock_transfer stock_Transfer = new frm_stock_transfer(DashboardCall);
                        CallSubForm(stock_Transfer, pnl_body, true);
                        break;
                    case "btnStockAdj":
                        frm_stock_adjustment stock_Adjustment = new frm_stock_adjustment(DashboardCall);
                        CallSubForm(stock_Adjustment, pnl_body, true);
                        break;
                    case "btnDelRecVer":
                        frm_delivery_receipt_verification delivery_Receipt_Verification = new frm_delivery_receipt_verification();
                        CallSubForm(delivery_Receipt_Verification, pnl_body, true);
                        break;
                    case "btnPayEntry":
                        frm_payment_entry payment_Entry = new frm_payment_entry();
                        CallSubForm(payment_Entry, pnl_body, true);
                        break;
                    case "btnCustDepo":
                        frm_customer_deposits customer_Deposits = new frm_customer_deposits();
                        CallSubForm(customer_Deposits, pnl_body, true);
                        break;
                    case "btnBillState":
                        frm_billing_statement billing_Statement = new frm_billing_statement();
                        CallSubForm(billing_Statement, pnl_body, true);
                        break;
                    case "btnCorFinAdj":
                        frm_corporation_finance_adjustment corporation_Finance_Adjustment = new frm_corporation_finance_adjustment();
                        CallSubForm(corporation_Finance_Adjustment, pnl_body, true);
                        break;
                    case "btnCusBegBal":
                        frm_customer_beginning_balance customer_Beginning_Balance = new frm_customer_beginning_balance();
                        CallSubForm(customer_Beginning_Balance, pnl_body, true);
                        break;
                }
            }
        }

        private void Masterfiles_SubMenu_Click(object sender, EventArgs e)
        {
            if (Helper.MenuConfirmator(pnl_body.Controls.Count, pnl_body.Controls.Find("PictureDashboard", true).Count(), "Are you sure you want to switch modules?\nAll unsaved changes will be discarded.", "System Confirmation"))
            {
                pnl_body.Controls.Clear();
                var button = sender as ToolStripMenuItem;
                switch (button.Name)
                {
                    case "BtnWarehouseMaster":
                        frm_warehouse warehouse_master = new frm_warehouse(DashboardCall);
                        CallSubForm(warehouse_master, pnl_body, true);
                        break;
                    case "BtnBinLocationMaster":
                        frm_bin_location bin_master = new frm_bin_location(DashboardCall);
                        CallSubForm(bin_master, pnl_body, true);
                        break;
                    case "BtnCustomerMaster":
                        frm_customer customer_master = new frm_customer(DashboardCall);
                        CallSubForm(customer_master, pnl_body, true);
                        break;
                    case "BtnSupplierMaster":
                        frm_supplier supplier_master = new frm_supplier(DashboardCall);
                        CallSubForm(supplier_master, pnl_body, true);
                        break;
                    case "BtnSukiMaster":
                        frm_suki_card suki_master = new frm_suki_card();
                        CallSubForm(suki_master, pnl_body, true);
                        break;
                    case "BtnReasonMaster":
                        frm_reason reason_master = new frm_reason(DashboardCall);
                        CallSubForm(reason_master, pnl_body, true);
                        break;
                    case "BtnRegionMaster":
                        frm_region region_master = new frm_region(DashboardCall);
                        CallSubForm(region_master, pnl_body, true);
                        break;
                    case "BtnProvinceMaster":
                        frm_province province_master = new frm_province(DashboardCall);
                        CallSubForm(province_master, pnl_body, true);
                        break;
                    case "BtnCityMaster":
                        frm_city city_master = new frm_city(DashboardCall);
                        CallSubForm(city_master, pnl_body, true);
                        break;
                    case "BtnCreditTermMaster":
                        frm_term term_master = new frm_term(DashboardCall);
                        CallSubForm(term_master, pnl_body, true);
                        break;
                    case "BtnPartsMaster":
                        frm_parts parts_master = new frm_parts(DashboardCall);
                        CallSubForm(parts_master, pnl_body, true);
                        break;
                    case "BtnBrandMaster":
                        frm_brand brand_master = new frm_brand(DashboardCall);
                        CallSubForm(brand_master, pnl_body, true);
                        break;
                    case "BtnDescriptionMaster":
                        frm_description description_master = new frm_description(DashboardCall);
                        CallSubForm(description_master, pnl_body, true);
                        break;
                    case "BtnMeasurementMaster":
                        frm_unit_measurement measurement_master = new frm_unit_measurement(DashboardCall);
                        CallSubForm(measurement_master, pnl_body, true);
                        break;
                    case "BtnEmployeeMaster":
                        frm_employee employee_master = new frm_employee(DashboardCall);
                        CallSubForm(employee_master, pnl_body, true);
                        break;
                    case "BtnDepartmentMaster":
                        frm_department department_master = new frm_department(DashboardCall);
                        CallSubForm(department_master, pnl_body, true);
                        break;
                    case "BtnPositionMaster":
                        frm_position position_master = new frm_position(DashboardCall);
                        CallSubForm(position_master, pnl_body, true);
                        break;
                    case "BtnOemMaster":
                        frm_oem oem_mater = new frm_oem(DashboardCall);
                        CallSubForm(oem_mater, pnl_body, true);
                        break;
                    case "BtnVehicleMakeMaster":
                        frm_vehicle_make make_master = new frm_vehicle_make(DashboardCall);
                        CallSubForm(make_master, pnl_body, true);
                        break;
                    case "BtnPromoMaster":
                        frm_promo promo_master = new frm_promo(DashboardCall);
                        CallSubForm(promo_master, pnl_body, true);
                        break;
                }
            }
        }

        private void Inquiry_SubMenu_Click(object sender, EventArgs e)
        {
            if (Helper.MenuConfirmator(pnl_body.Controls.Count, pnl_body.Controls.Find("PictureDashboard", true).Count(), "Are you sure you want to switch modules?\nAll unsaved changes will be discarded.", "System Confirmation"))
            {
                pnl_body.Controls.Clear();
                var button = sender as ToolStripMenuItem;
                switch (button.Name)
                {
                    case "BtnPartsInquiry":
                        frm_parts_inquiry parts_inquiry = new frm_parts_inquiry(DashboardCall);
                        CallSubForm(parts_inquiry, pnl_body, true);
                        break;
                    case "BtnInventoryManagement":
                        frm_inventory_management inventory_management = new frm_inventory_management(DashboardCall);
                        CallSubForm(inventory_management, pnl_body, true);
                        break;
                    case "BtnInventoryAuditTrail":
                        frm_audit_trail audit_inquiry = new frm_audit_trail();
                        CallSubForm(audit_inquiry, pnl_body, true);
                        break;
                    case "BtnSalesOrderArchive":
                        frm_sales_order_archive so_archive = new frm_sales_order_archive(DashboardCall);
                        CallSubForm(so_archive, pnl_body, true);
                        break;
                }
            }
        }

        private void Reports_SubMenu_Click(object sender, EventArgs e)
        {
            if (Helper.MenuConfirmator(pnl_body.Controls.Count, pnl_body.Controls.Find("PictureDashboard", true).Count(), "Are you sure you want to switch modules?\nAll unsaved changes will be discarded.", "System Confirmation"))
            {
                pnl_body.Controls.Clear();
                var button = sender as ToolStripMenuItem;
                switch (button.Name)
                {
                    case "BtnSalesReports":
                        frm_sales_report sales_report = new frm_sales_report(DashboardCall);
                        CallSubForm(sales_report, pnl_body, true);
                        break;
                    case "BtnSalesReturnReports":
                        frm_sales_return_report sales_return_report = new frm_sales_return_report(DashboardCall);
                        CallSubForm(sales_return_report, pnl_body, true);
                        break;
                    case "BtnInventoryReports":
                        frm_inventory_report inventory_report = new frm_inventory_report();
                        CallSubForm(inventory_report, pnl_body, true);
                        break;
                    case "BtnReceivingReports":
                        frm_receiving_report receiving_report = new frm_receiving_report();
                        CallSubForm(receiving_report, pnl_body, true);
                        break;
                    case "BtnMonthlyInventoryReports":
                        frm_monthly_inventory_report inventory_monthly_report = new frm_monthly_inventory_report();
                        CallSubForm(inventory_monthly_report, pnl_body, true);
                        break;
                    case "BtnTransmittalReports":
                        frm_transmittal transmittal = new frm_transmittal();
                        CallSubForm(transmittal, pnl_body, true);
                        break;
                    case "BtnDailyTransactionReports":
                        frm_daily_transaction_report daily_transaction_report = new frm_daily_transaction_report(DashboardCall);
                        CallSubForm(daily_transaction_report, pnl_body, true);
                        break;
                        //case "btn_cf_sales_reports":
                        //    frm_cf_sales_report cf_report = new frm_cf_sales_report();
                        //    CallSubForm(cf_report, pnl_body);
                        //    break;
                }
            }
        }

        private void Utilities_SubMenu_Click(object sender, EventArgs e)
        {
            if (Helper.MenuConfirmator(pnl_body.Controls.Count, pnl_body.Controls.Find("PictureDashboard", true).Count(), "Are you sure you want to switch modules?\nAll unsaved changes will be discarded.", "System Confirmation"))
            {
                pnl_body.Controls.Clear();
                var button = sender as ToolStripMenuItem;
                switch (button.Name)
                {
                    case "BtnCompanyProfile":
                        frm_company company_profile = new frm_company(DashboardCall);
                        CallSubForm(company_profile, pnl_body, true);
                        break;
                    case "BtnUserProfile":
                        frm_user_profile user_profile = new frm_user_profile(DashboardCall);
                        CallSubForm(user_profile, pnl_body, true);
                        break;
                    case "BtnCountSheet":
                        frm_count_sheet count_sheet = new frm_count_sheet();
                        CallSubForm(count_sheet, pnl_body, true);
                        break;
                }
            }
        }

        private void CallSubForm(Form frm, Panel pnl, bool hideBrands)
        {
            if (hideBrands && PictureBrand1.Visible)
            {
                PictureBrand1.Visible = PictureBrand2.Visible = PictureBrand3.Visible = PictureBrand4.Visible = PictureBrand5.Visible = false;
            }
            else if (!hideBrands && !PictureBrand1.Visible)
            {
                PictureBrand1.Visible = PictureBrand2.Visible = PictureBrand3.Visible = PictureBrand4.Visible = PictureBrand5.Visible = true;
            }

            while (pnl.Controls.Count > 0)
            {
                pnl.Controls[0].Dispose();
            }

            frm.TopLevel = false;
            pnl.Controls.Add(frm);
            frm.Show();

            this.Close();
        }

        private void DashboardCall()
        {
            FrmDashBoard dash = new FrmDashBoard();
            CallSubForm(dash, pnl_body, false);
        }

        //private void ClearSubForm(Form frm, Panel pnl)
        //{


        //    Panel p = this.Parent as Panel;
        //    if (p != null)
        //    {


        //        // Create and show form3
        //        frm myForm = new frm();
        //        myForm.FormBorderStyle = FormBorderStyle.None;
        //        myForm.TopLevel = false;
        //        myForm.AutoScroll = true;
        //        panel.Controls.Add(myForm);
        //        myForm.Show();

        //        // Close the current form (form2)
        //        this.Close();
        //    }
        //}

        private void FrmCarsBeta_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
