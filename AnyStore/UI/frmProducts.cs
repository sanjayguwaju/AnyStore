using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;
using AnyStore.UI;

namespace AnyStore.DAL
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //We need to Add code to hide this Product form when we click on Button
            this.Close();
        }
        categoriesDAL cdal = new categoriesDAL(); // This a Categroy Object ot get all the Data  from category 

        productsBLL p = new productsBLL(); //This is a product Bll to get all the data like which we canpass in data base like string in name and int in age like that 

        productsDAL pdal = new productsDAL(); // This is product object to fetch methods like Insert Delete Update and Search

        userDAL udal = new userDAL(); // This is for the added by function we created an object ot get user details

        private void frmProducts_Load(object sender, EventArgs e)
        {
            //Creating DataTable to  hold the categories from Database
            DataTable categoriesDT = cdal.Select();

            //Specify DataSource fro Category Combo Box
            cmbCategory.DataSource = categoriesDT;

            //Specify Display Member and Value Member for Combo box
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            //Load all the Products in the Data Grid View
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        } // This is Product form load event to perform different activity like displaying etc

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get all the values from product form
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;

            //Getting the username of the logged in user
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            //Create boolean variable to check if the product is added successfully or not
            bool success = pdal.Insert(p);

            //If the product is added sucessfully then the value of the succes will be else it will be false
            if (success == true)
            {
                //Product Inserted Sucesfully
                MessageBox.Show("Product Added Sucessfully");

                //Calling the Clear MEthod
                Clear();

                //Refreshing Data Grid View
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Add New Product
                MessageBox.Show("Failed to Add New Product");
            }
        } // This Method is for Adding the data in the table
         
        public void Clear() // This method is to Clear the Text Fields
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        } // This is used to clear the text fields

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Create Integer Variable to know which product was clicked
            int rowIndex = e.RowIndex;

            //Display the Value on Respective TextBoxes
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();

        } // This is for when we click on Row header then data will show in textfields

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from the Product Form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);

            //Getting Username of logged in user for added by
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            //Passing the id of the logged in User in added by field
            p.added_by = usr.id;

            //Creating Boolean variable to update Product and check
            bool success = pdal.Update(p);

            //If the products is updated successfully then the value of success will be true else it is false
            if (success == true)
            {
                //Products Updated Successfully
                MessageBox.Show("Product Updated Successfully");

                Clear(); //Clear the Text Box Fields after deletion is completed

                //Refresh Data Grid View
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Upadate Category
                MessageBox.Show("Failed to Upddate Products");
            }
        } // This is for Updating the Data in the Table

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get Id of the Product Which we want to delete
            p.id = int.Parse(txtID.Text);

            //Creating Boolean Variable to Delete the Product
            bool success = pdal.Delete(p);

            //If the Product id deleted Sucessfully then the value of the success is true else value of the success is false
            if (success == true)
            {
                MessageBox.Show("Product Deleted Sucessfully");

                // To Clear the Text Fields after deleted
                Clear();

                //Refresh Data Grid View
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;

            }
            else
            {
                MessageBox.Show("Failed to Delete Product");
            }
        } // This event is for delete button to delete the data form the data grid view

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the Keywords 
            string keywords = txtSearch.Text;

            //Filter the products based on keywords
            if (keywords != null)
            {
                //Use Search to Display products
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Use Select Method to Display all the products
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        } // This event is for searching the Data
    }
}
