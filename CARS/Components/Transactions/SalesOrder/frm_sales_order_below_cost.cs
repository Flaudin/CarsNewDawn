using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions.SalesOrder
{
    public partial class frm_sales_order_below_cost : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesOrderController _SalesOrderController = new SalesOrderController();
        private decimal discount;
        public event Action<decimal> Validation;

        public frm_sales_order_below_cost(decimal Discount)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            discount = Discount;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("The discount will be discarded. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Validation?.Invoke(0);
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtPassword.Textt.Length != 0)
            {
                Validation?.Invoke(discount);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please provide a password of another employee and a valid reason before proceeding", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void frm_stock_transfer_parts_encode_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    BtnClose.PerformClick();
                    break;

                case Keys.Enter:
                    BtnSave.PerformClick();
                    break;
            }
        }
    }
}
