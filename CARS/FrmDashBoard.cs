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
    public partial class FrmDashBoard : Form
    {
        private ColorManager _ColorManager = new ColorManager();

        public FrmDashBoard()
        {
            InitializeComponent();
            PictureDashboard.BackColor = Color.FromArgb(_ColorManager.BackgroundRGB[0], _ColorManager.BackgroundRGB[1], _ColorManager.BackgroundRGB[2]); ;
        }
    }
}
