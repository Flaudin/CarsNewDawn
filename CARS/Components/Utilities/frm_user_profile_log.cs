using CARS.Controller.Transactions;
using CARS.Controller.Utilities;
using CARS.Functions;
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

namespace CARS.Components.Utilities
{
    public partial class frm_user_profile_log : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private UserProfileController _UserProfileController = new UserProfileController();
        private DataTable LogTable = new DataTable();

        public frm_user_profile_log()
        {
            InitializeComponent();
            LogTable = _UserProfileController.UserLog("Justin");
            DataGridLog.DataSource = LogTable;
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close User Log?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }
    }
}
