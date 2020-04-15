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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }
        categoriesBLL c = new categoriesBLL(); // This is BLL Object to fetch the Values form BLL Class
        categoriesDAL dal = new categoriesDAL(); // This is DAL Object to fetch the value from DAL Class
        userDAL udal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        } // This Eevent is for My Picture Box

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the Values from Category Form
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            
            //Getting ID in Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            //PAssing the id of logged in User in added by field
            c.added_by = usr.id;

            //Creating Boolean Method to Insert data into database
            bool success = dal.Insert(c);

            //If the value is inserted sucessfully then the value of success will be true else value will be false
            if (success == true)
            {
                //New Categories Inserted Sucessfully
                MessageBox.Show("New Category Inserted Sucessfully");

                Clear(); // this clear() clears all thae data after it is inserted sucessfully

                //Refresh the Data Grid View after inserted sucessfully -------------Acually it is not refreshed but after inserting it agains shows data grid view from begin or loads data grid view again which makes us feel like it is refreshed---------
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Failed to Insert New Category
                MessageBox.Show("Failed to Insert to Add New Category");
            }
        } // This Event os for my Add Button

        public void Clear() //To clear all the fields
        {
            txtCategoryID.Text = "";
            txtDescription.Text = "";
            txtTitle.Text = "";
            txtSearch.Text= "";
        } // This method is for clearing the text box fields

        private void frmCategories_Load(object sender, EventArgs e)
        {
            //Here write the code to display all the categories in Data Grid View
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }  // This Event is to Load data in Categories Data Grid view

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) // This Event is to Select after clicking in row the txt of the data grid shows in Text box
        {
            //Finding the row index of the Row Clicked on Data Grid View
            int RowIndex = e.RowIndex;
            txtCategoryID.Text = Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from the Form
            c.id = int.Parse(txtCategoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            //Getting ID in Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            //Passing the id of the logged in User in added by field
            c.added_by = usr.id;

            //Creating Boolean variable to update categories and check
            bool success = dal.Update(c);
            
            //If the category is updated successfully then the value of success is greater than 0 else it is less than 0
            if (success == true)
            {
                //Category Updated Sucessfully
                MessageBox.Show("Category Updated Sucessfully");

                Clear(); //Clear the Text Box Fields after deletion is completed

                //Refresh Data Grid View
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Failed to Upadate Category
                MessageBox.Show("Failed to Upddate Category");
            }
        } // This is for Updating the Data in the Table
         
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get Id of the Category Which we want to delete
            c.id = int.Parse(txtCategoryID.Text);

            //Creating Boolean Variable to Delete the Category
            bool success = dal.Delete(c);

            //If the Category id deleted Sucessfully then the value of the success is true else value of the success is false
            if (success == true)
            {
                MessageBox.Show("Category Deleted Sucessfully");
                // To Clear the Text Fields after deleted
                Clear();

                //Refresh Data Grid View
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Delete Category");
            }
        } // This event is for delete button to delete the data form the data grid view

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the Keywords 
            string keywords = txtSearch.Text;

            //Filter the categories based on keywords
            if (keywords != null)
            {
                //Use Search to Display Categories
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Use Select Method to Display all the categoires
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        } // This event is for searching the Data
    }
}
