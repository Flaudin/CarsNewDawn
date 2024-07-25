using CARS.Controller.Login;
using CARS.Functions;
using CARS.Model.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Login
{
    public partial class Login : Form
    {
        private LoginModel loginModel;
        private LoginController loginController;
        public Login()
        {
            InitializeComponent();
            loginController = new LoginController();
            loginModel = new LoginModel();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtPass.Textt != "" && txtUser.Textt == "")
            {
                Helper.Confirmator("Please enter a username", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if (txtPass.Textt == "")
            {
                Helper.Confirmator("Please enter a password", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(txtUser.Textt != "" && txtPass.Textt != "")
            {
                loginModel = loginController.Login(txtUser.Textt.TrimEnd());
                if(loginModel.UserID != null)
                {
                    string decryptedPass = Helper.DecryptPasswordDesktopAppVersion(loginModel.UserPassword);
                    if(txtUser.Textt == loginModel.UserID)
                    {
                        if(decryptedPass == txtPass.Textt)
                        {
                            //Helper.Confirmator("Login Successfully", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FrmCarsBeta frm = new FrmCarsBeta();
                            frm.FormClosed += Frm_FormClosed;
                            this.Hide();
                            frm.Show(this);
                        }
                        else
                        {
                            Helper.Confirmator("Password is not match. Please try again", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                    else
                    {
                        Helper.Confirmator("UserID is not match. Please try again", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            else
            {
                MessageBox.Show("UserID is not match.Please try again", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
