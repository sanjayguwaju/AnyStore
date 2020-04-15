using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;
using AnyStore.DAL;

namespace AnyStore.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //Creating method for LoginBLL and loginDAL------- Otherwise classes wil not be connected
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            //Checking the login credentials
            bool sucess = dal.loginCheck(l);
            if (sucess == true)
            {
                //Login Sucessful
                MessageBox.Show("Login Successful");
                loggedIn = l.username;

                //Need to open Respective Forms and on User Type
                switch (l.user_type)
                {
                    case "Admin":
                    {
                            //Display Admin Dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                    }
                        break;
                    case "User":
                    {
                            //Display User Dashboard
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                    }
                        break;
                    default:
                    {
                            //Display an error message
                            MessageBox.Show("Invalid User Type");
                    }
                        break;
                }
            }
            else
            {
                //Login Failed
                MessageBox.Show(("Login Failed Please Try Again !!!"));
            }

        }
    }
}
