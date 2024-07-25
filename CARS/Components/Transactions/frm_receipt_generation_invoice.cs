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

namespace CARS.Components.Transactions
{
    public partial class frm_receipt_generation_invoice : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesOrderController _SalesOrderController = new SalesOrderController();
        private SortedDictionary<string, string> _CustomerDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _TermDictionary = new SortedDictionary<string, string>();
        private List<string> PartsList = new List<string>();
        private int SOPartCounter = 0;
        public event Action<List<dynamic[]>> StringArraySent;
        private DataTable PartTable  = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_receipt_generation_invoice()
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = LblFilter.ForeColor = BtnClose.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _CustomerDictionary = _TransactionController.GetDictionary("Customer");
            _TermDictionary = _TransactionController.GetDictionary("Term");
            ComboCustomer.DataSource = new BindingSource(_CustomerDictionary, null);
            ComboTerm.DataSource = new BindingSource(_TermDictionary, null);
            ComboCustomer.DisplayMember = ComboTerm.DisplayMember = "Key";
            ComboCustomer.ValueMember = ComboTerm.ValueMember = "Value";
        }

        private void frm_stock_transfer_parts_encode_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Check out?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void frm_stock_transfer_parts_encode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!TxtColumnSearch.Visible)
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

        private void ComboCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
    }
}
