using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.Properties;
using AnyStore.UI;

namespace AnyStore
{
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
            InitializeComponent();
        }
        //Set a public static method to specify wheteher the form is purchase or sales
        public static string transactionType;

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn; 
        }

        private void deealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Set Value on transactionType static method
            transactionType = "Purchase";


            //This will show purchase form when we click on Userdashboard's Purchase
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();

           

        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set Value on transactionType Static Method
            transactionType = "Sales";

            //This will show purchase form when we click on Userdashboard's sales
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();

           
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }
    }
}
