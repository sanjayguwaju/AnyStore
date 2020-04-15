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

namespace AnyStore.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        
        userBLL u = new userBLL();   // These are both classes we have created in Folders
        userDAL dal = new userDAL(); // These are both classes we have created in Folders


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Mistakly clicked.
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Mistakely Clicked
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Getting Username of the logged in user

            string loggedUser = frmLogin.loggedIn;
            // Getting Data From UI
            // We will use BLL Object here
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now; // This DateTime.Now will show the current date when user adds data

            userBLL usr = dal.GetIDFromUsername((loggedUser));

            u.added_by = usr.id; // currently we don't have user id so we wil put it later ***** We have fixed it and we have user id now

            //Inserting Data into Database
            bool sucess = dal.Insert(u);
            // If the data is sucessfully inserted then the value will be true else it will be false
            if (sucess == true)
            {
                //Data Sucessfully Inserted
                MessageBox.Show("User Sucessfully Added");
                clear();
            }
            else
            {
                // Failed to insert data
                MessageBox.Show("Failed to Add New User");
            }
            //Refreshing Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            //Refreshing Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void clear()
        {
            // This is for clearing the Fields after adding user details
            txtUserID.Text = "";
            txtFirstName.Text= "";
            txtLastName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";
            txtEmail.Text = " ";
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get index of the particular Row index
            int rowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from User UI
            u.id = Convert.ToInt32(txtUserID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now; // This DateTime.Now will show the current date when user adds data
            u.added_by = 1; // currently we don't have user id so we wil put it later
            
            //Updating Data into Database
            bool success = dal.Update(u);
            //if data is updated sucessfully then the value of success will be true else it will be false
            if (success == true)
            {
                //Data Updated Sucessfully
                MessageBox.Show("User Sucessfully Updated");
            }
            else
            {
                // failed to update user
                MessageBox.Show("Failed to Update User");
            }
            //Refreshing Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Getting User ID from DataTable Form
            u.id = Convert.ToInt32(txtUserID.Text);

            bool success = dal.Delete((u));
            //if data is deleted then the value of success will be true else it is false
            if (success == true)
            {
                // User Deleted Sucessfullly
                MessageBox.Show(" User Deleted Successfully ");
                clear();
            }
            else
            {
                //
                MessageBox.Show("Delete User Unsucessful");
            }
            //Refreshing DataGridView
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get Keyword form Text box
            string keywords = txtSearch.Text;

            //Check if the keywords has value or not
            if (keywords != null)
            {
                //Show user based on keywords
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
        }

        private void frmUsers_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide(); // This hides background dialouge box
        }
    }
}
