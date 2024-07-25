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
    public partial class frm_sales_order_free_check : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesOrderController _SalesOrderController = new SalesOrderController();
        private SortedDictionary<string, string> _ReasonDictionary = new SortedDictionary<string, string>();
        public event Action<string> StringReason;

        public frm_sales_order_free_check()
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _ReasonDictionary = _TransactionController.GetDictionary("Reason");
            ComboReason.DataSource = new BindingSource(_ReasonDictionary, null);
            ComboReason.DisplayMember = "Key";
            ComboReason.ValueMember = "Value";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Parts selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtPassword.Textt.Length != 0 && ComboReason.SelectedIndex != 0)
            {
                StringReason?.Invoke(ComboReason.SelectedValue.ToString());
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
