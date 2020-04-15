using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;
using AnyStore.DAL;
using AnyStore.UI;

namespace AnyStore.Properties
{
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }


        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //To Close this form we need this close statement
            this.Hide();
        }

        DeaCustBLL dc = new DeaCustBLL(); // This Object is to connect this Form with Class BLL
        DeaCustDAL dcDal = new DeaCustDAL(); // This Database is to connect this Form with Class DAL

        userDAL uDal = new userDAL(); // This Object is Created to Fetch the User ID

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the Value from Form
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //Getting the ID to Logged of logged in user and passing its value in dealer
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            dc.added_by = usr.id;

            //Creating boolean variable to check whether the dealer or customer is added or not
            bool success = dcDal.Insert(dc);

            if (success == true)
            {
                //Dealer or Customer inserted successfully
                MessageBox.Show("Dealear or Customer is Added Sucessfully");
                Clear();

                //Refresh Data Grid View
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;

            }
            else
            {
                //Failed to Insert Dealer
                MessageBox.Show("Failed to Add Dealer or Customer");
            }
        } // this Method is to Make functionality of Add Button
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        } // This method is Create to clear the Text Fields

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            //Refresh Data Grid View
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        } // This Method is Created to Load different kind of load events

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int varibale to get the identity row clicked
            int rowIndex = e.RowIndex;

            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
        } // This Method is used to Create roe header active-------When you click oon row header then all the Data is shown in text fiedls

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from Form
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            //Getting the ID to Logged of logged in user and passing its value in dealer
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            dc.added_by = usr.id;

            //Create boolean variable to check whether the dealer or customer is updated or not
            bool success = dcDal.Update(dc);

            if (success == true)
            {
                //Dealer and Customer update Successfully
                MessageBox.Show("Dealer or Customer updated successfully");

                //To clear the Text Fields
                Clear();

                //Refresh the Data Grid View
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Dealer and Customer failed to Updated
                MessageBox.Show("Failed to Update Dealer or Customer");
            }
        } // This method is created to Update the Data in the DataGrid View or We can say Table

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //ID of the User to be Deleted
            dc.id = int.Parse(txtDeaCustID.Text);


            //Create boolean variable to check whether the dealer or customer is deleted ro not
            bool success = dcDal.Delete(dc);

            if (success == true)
            {
                //Dealer or Customer Deleted Successfully
                MessageBox.Show("Dealer or Customer Deleted Sucessfully");


                //To Clear the Text Fields
                Clear();

                //Refresh the Table
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to Delete Dealer or Customer
                MessageBox.Show("Failed to Delete Dealer or Customer");
            }
        } //This method is to Delete the Data in the Table

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the Keyword form the Textbox
            string keyword = txtSearch.Text;

            if (keyword != null)
            {
                //Search the Dealer or Customer
                DataTable dt = dcDal.Search(keyword);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Show all the Dealer or Customer
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
        } //tThis Method is created to Search the data in the Data Grid View
    } 
}
