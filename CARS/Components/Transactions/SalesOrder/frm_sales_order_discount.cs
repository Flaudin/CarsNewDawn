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
    public partial class frm_sales_order_discount : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesOrderController _SalesOrderController = new SalesOrderController();
        private decimal ListPrice;
        private string PartNo;
        private bool BelowCost;
        public event Action<decimal, bool> DiscountVal;

        public frm_sales_order_discount(decimal currentSRP, string partno)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            ListPrice = currentSRP;
            PartNo = partno;
            NumericDiscount.Maximum = currentSRP - 1;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close discount encoding?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (maskScheme.Textt != "00000000")
            {
                decimal price = ListPrice;
                price = price * ((100 - Convert.ToDecimal(maskScheme.Textt.Substring(0, 2)))/100);
                price = price * ((100 - Convert.ToDecimal(maskScheme.Textt.Substring(2, 2)))/100);
                price = price * ((100 - Convert.ToDecimal(maskScheme.Textt.Substring(4, 2)))/100);
                price = price * ((100 - Convert.ToDecimal(maskScheme.Textt.Substring(6, 2)))/100);
                IsBelowCost(ListPrice - price);
            }
            else if (NumericDiscount.Value != 0)
            {
                IsBelowCost(NumericDiscount.Value);
            }
            else if (NumericDiscountPercent.Value != 0)
            {
                IsBelowCost((NumericDiscountPercent.Value / 100) * ListPrice);
            }
            else if (NumericNetPrice.Value != 0)
            {
                IsBelowCost(ListPrice - NumericNetPrice.Value);
            }
            else
            {
                MessageBox.Show("Please provide a value in one discount type.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Discount_Enter(object sender, EventArgs e)
        {
            Control active = this.ActiveControl;
            switch (active.Name)
            {
                case "NumericDiscount":
                    NumericDiscountPercent.Value = NumericNetPrice.Value = 0;
                    maskScheme.Textt = "";
                    break;

                case "NumericDiscountPercent":
                    NumericDiscount.Value = NumericNetPrice.Value = 0;
                    maskScheme.Textt = "";
                    break;

                case "maskScheme":
                    NumericDiscount.Value = NumericDiscountPercent.Value = NumericNetPrice.Value = 0;
                    break;

                case "NumericNetPrice":
                    NumericDiscount.Value = NumericDiscountPercent.Value = 0;
                    maskScheme.Textt = "";
                    break;
            }
        }

        private void IsBelowCost(decimal discount)
        {
            BelowCost = _SalesOrderController.AllowBelowCost(ListPrice - discount, PartNo);
            if (BelowCost)
            {
                frm_sales_order_below_cost belowCost = new frm_sales_order_below_cost(discount);
                belowCost.Validation += ValidationPass;
                belowCost.ShowDialog(this);
            }
            else
            {
                DiscountVal?.Invoke(discount, false);
                this.Close();
            }
        }

        private void ValidationPass (decimal discount)
        {
            if (discount != 0)
            {
                DiscountVal?.Invoke(discount, true);
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        //private bool IsBelowCost(decimal discount)
        //{
        //    BelowCost = _SalesOrderController.AllowBelowCost(ListPrice - discount, PartNo);
        //    if (BelowCost)
        //    {
        //        if (Helper.Confirmator("This current quantity will yield below cost. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        private void BtnClearDiscount_Click(object sender, EventArgs e)
        {
            DiscountVal?.Invoke(0, false);
            this.Close();
        }
    }
}
