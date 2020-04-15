using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using AnyStore.BLL;
using AnyStore.DAL;

namespace AnyStore.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //This will close the form
            this.Hide();
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------
         --------------------------------------------OBJECT ARE PRESENT HERE FOR DIFFERENT PURPOSES--------------------------------------------------------------------
         --------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        DeaCustDAL dcDAL = new DeaCustDAL();

        productsDAL pDAL = new productsDAL();

        userDAL uDAL = new userDAL();

        transcationDAL tDAL = new transcationDAL();

        transactionDetailDAL tdDAL = new transactionDetailDAL();

        DataTable transactionDT = new DataTable();

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------
         --------------------------------------------OBJECT ARE PRESENT HERE FOR DIFFERENT PURPOSES--------------------------------------------------------------------
         --------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        private void pnlDeaCust_Paint(object sender, PaintEventArgs e) //----------------Mistakely Clicked--------------------//
        {

        }

        private void pnlCalculation_Paint(object sender, PaintEventArgs e) //----------------Mistakely Clicked--------------------//
        {

        }

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            //Get the transactionType value from frmUserDAshboard
            string type = frmUserDashboard.transactionType;

            //Set the value on lblTop
            lblTop.Text = type;


            //Specify columns for our Transaction Data Table
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keywords from the text box
            string keyword = txtSearch.Text;

            if (keyword == "")
            {
                //Clear all the textboxes
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }

            //Write the code to get the details and set the value on the text boxes
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword); //********************<<<<<<<<<<<<<<<<<

            //Now Transfer or Set the value from DeCustBLL to textboxes
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            //Get the Keywords from product search textbox
            string keyword = txtSearchProduct.Text;

            //Check if we have value to txtSearchProduct or not
            if (keyword == "")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }

            //Search the product and display on respective textboxes
            productsBLL p = pDAL.GetProductsforTransaction(keyword);

            //Set the values on the textboxes
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get product Nam,Rate and Qty customer wants to buy
            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal Qty = decimal.Parse(txtQty.Text);

            decimal Total = Rate * Qty; // Total = Rate * Qty <<<<<<<<< We are calculating the Total Value of the Calculation

            //Display the Total in the textbox
            //Get the subtotal value from textbox
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total;

            //Check whether the product is selected or not
            if (productName == "")
            {
                //Display Error Message
                MessageBox.Show("Select the product first then Try Again!!!");
            }
            else
            {
                //Add product to the Data Grid View
                transactionDT.Rows.Add(productName, Rate, Qty, Total);


                //Show in Data Grid View
                dgvAddedProoducts.DataSource = transactionDT;

                //Display the subtotal in the Textbox
                txtSubTotal.Text = subTotal.ToString();

                //Clear the Textboxes
                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtQty.Text = "";


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Get the Values or details from the PurchaseForm first
            transactionsBLL transaction = new transactionsBLL();

            transaction.type = lblTop.Text;

            //Get the ID of Dealer or Customer Here
            //Lets get name of the dealer or customer first
            string deaCustName = txtName.Text;

            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName); //***********************

            transaction.dea_cust_id = dc.id; //***********************************
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //Get the Username of the Logged in User
            string username = frmLogin.loggedIn;
            userBLL u = uDAL.GetIDFromUsername(username);

            transaction.added_by = u.id;
            transaction.transactionDetails = transactionDT;

            //Lets Create a Boolean Variable and Set it's value to False
            bool success = false;

            //Actual code to Insert Transaction and Transaction Details
            using (TransactionScope scope = new TransactionScope())
            {
                int tranasctionID = -1;

                //Create a boolean Value and Insert Transaction
                bool w = tDAL.Insert_Transction(transaction, out tranasctionID);

                //use for  loop to Insert Transaction Details
                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    //Get all the details of the product
                    transcationDetailsBLL transcationDetails = new transcationDetailsBLL();

                    //Get the product Name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString(); // Here was Errror before<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    productsBLL p = pDAL.GetDeaProductIDFromName(ProductName);
                    transcationDetails.product_id = p.id;
                    transcationDetails.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transcationDetails.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transcationDetails.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()),2);
                    transcationDetails.dea_cust_id = dc.id;
                    transcationDetails.added_date = DateTime.Now;
                    transcationDetails.added_by = u.id;

                    /*------------------------------------------------------------------------------------------------------------------
                     ------------ PRODUCT INCREASE OR DECREASE OR UPDATE ---------------------------------------------------------------
                     -------------------------------------------------------------------------------------------------------------------*/


                    //Here Increase or Decrease Product Quantity based on Purchase or Sales
                    string transactionType = lblTop.Text;

                    //Lets check whether we are on Purchase or Sales
                    bool x = false;
                    if (transactionType=="Purchase")
                    {
                        //Increase the product 
                        x = pDAL.IncreaseProduct(transcationDetails.product_id, transcationDetails.qty);
                    }
                    else if (transactionType=="Sales")
                    {
                        //Decrease the Product Quantity
                        x = pDAL.DecreaseProduct(transcationDetails.product_id, transcationDetails.qty);
                    }

                    /*---------------------------------------------------------------------------------------------------------------------------
                     ------------------------PRODUCT INCREASE OR DECREASE OR UPDATE--------------------------------------------------------------
                     ----------------------------------------------------------------------------------------------------------------------------*/

                    //Insert Transaction Details inside the database
                    bool y = tdDAL.InsertTransactionDetail(transcationDetails);
                    success = w && x && y;
                }
                
                if (success == true)
                {
                    //Transaction Complete
                    scope.Complete();
                    MessageBox.Show("Transaction Completed Sucessfully");

                    //Clear Data Grid View and all of the Textboxes
                    dgvAddedProoducts.DataSource = null;
                    dgvAddedProoducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProduct.Text = "";
                    txtProductName.Text = "";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    txtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";

                }
                else
                {
                    //Transaction Failed
                    MessageBox.Show("Transaction Failed");
                }
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //Get the value for discount textbox
            string value = txtDiscount.Text;

            if (value == "")
            {
                //Display Error Message
                MessageBox.Show("Please and Discount First");
            }
            else
            {
                //Get the discount in decimal value
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);

                //Calculate the grandtotal based on discount 
                decimal grandTotal = (( 100 - discount) / 100) * subTotal;

                //Display the GrandTotal in TextBox
                txtGrandTotal.Text = grandTotal.ToString();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            //Check if the grandTotal has value or not if it has not value then calculate the discount first
            string check = txtGrandTotal.Text;
            if (check == "")
            {
                //Display the error message to calculate discount
                MessageBox.Show("Calculate the discount and set the Grand Total First");
            }
            else
            {
                //Calculate VAT
                //Getting the VAT percent first
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVAT = ((100 + vat) / 100) * previousGT;

                //Displaying new grandTotal with VAT
                txtGrandTotal.Text = grandTotalWithVAT.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            //Get the paid Amount and Grand Total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount = paidAmount - grandTotal;

            //Display the return amount as well
            txtReturnAmount.Text = returnAmount.ToString();
        }
    } // This class has all the codes for Purchase and Sales
}