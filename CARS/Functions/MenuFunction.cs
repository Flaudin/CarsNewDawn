using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Functions
{
    internal class MenuFunction
    {
        public static ToolStripMenuItem[] PopulateMenuStrip(string[][] menu_items)
        {
            //strip_menu.Items.Clear();
            //string[][] menu_items =
            //{
            //    new string []{"P.O. Generation", "po_generation"}, new string []{ "P.O. Monitoring", "po_monitoring"}, new string []{ "P.O. Approval", "po_approval"}, 
            //    new string []{"Receiving", "receiving"}, new string []{"Purchase Returns", "purchase_returns"}, new string []{ "Price Management", "price_management"},
            //    new string []{"SO Entry", "so_entry"}, new string []{"ATR Approval", "atr_approval"}, new string []{ "Receipt Generation", "receipt_generation"},
            //    new string []{"Sales Return", "sales_return"}, new string []{"Beginning Balance", "beginning_balance"}, new string []{ "Stock Transfer", "stock_transfer"},
            //    new string []{"Stock Adjustment", "stock_adjustment"}, new string []{"Counter Receipt Generation", "counter_receipt"}, new string []{ "DR Verification", "dr_verification"},
            //    new string []{"Payment Entry", "payment_entry"}, new string []{"Customer Deposit Application", "customer_deposti"}, new string []{ "Billing Statement", "billing"}
            //};
            //strip_menu.Items.AddRange(MenuFunction.PopulateMenuStrip(menu_items));
            ToolStripMenuItem[] strip_items = new ToolStripMenuItem[menu_items.Length];

            for (int i = 0; i < menu_items.Length; i++)
            {
                strip_items[i] = new ToolStripMenuItem();
                strip_items[i].Text = menu_items[i][0].ToString();
                strip_items[i].Name = menu_items[i][1].ToString();
                strip_items[i].Click += new EventHandler(MenuItemClickHandler);
            }
            return strip_items;
        }

        private static void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            switch (clickedItem.Name.ToString())
            {
                case "po_generation":
                    MessageBox.Show("1");
                    break;
                case "po_monitoring":
                    MessageBox.Show("2");
                    break;
            }
        }
    }
}
