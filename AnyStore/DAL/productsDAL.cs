using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.UI;

namespace AnyStore.DAL
{
    class productsDAL
    {
        //Creating START String Method for DB Connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Product Module

        public DataTable Select()
        {
            //Create sql connection to connect Databases
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Datatable to Hold the Data from Database
            DataTable dt = new DataTable();

            try
            {
                //Writing the Query to Select all the products from database
                String sql = "SELECT *FROM tbl_products";

                //Creating SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creating SQL Data Adapter to hold the value form the databse temprorarly
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Addding the value from adapter to Data Table form dt
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion

        #region Method to Insert Product in database

        public bool Insert(productsBLL p)
        {
            //Creating Boolean Variable and Set its value to false
            bool isSuccess = false;

            //Sql connection for Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL Query to insert product into database
                String sql =
                    "INSERT INTO tbl_products(name,category,description,rate,qty,added_date,added_by) VALUES(@name,@category,@description,@rate,@qty,@added_date,@added_by)";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

                //Opening the Database Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed sucessfully then the value of rows will be else the value will be less than 0
                if (rows > 0)
                {
                    //Query Executed Sucessfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Method to Update Products in Database

        public bool Update(productsBLL p)
        {
            //Create a boolean variables and set its initial value to false
            bool isSuccess = false;

            //Create SQL Conection for Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL  query to Update in the Database
                String sql =
                    "Update tbl_products SET name=@name, category=@category, description=@description, rate=@rate,added_date=@added_date, added_by=@added_by WHERE id=@id";

                //Create SQL Command to Pass the value to Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the values using parameters and cmd
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open the Database Connection
                conn.Open();

                //Create Int Variable to check if the query is executed successfully or not
                int rows = cmd.ExecuteNonQuery();

                //If the query is executed then the values of the rows will be greater than 0 else less than 0
                if (rows > 0)
                {
                    //Query Executed Sucessfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Method to Delete Product from Database

        public bool Delete(productsBLL p)
        {
            //Create Boolean Variable and Set its default value to false
            bool isSuccess = false;

            //SQL Connection for Database connnection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write Query Product from Database
                String sql = "DELETE FROM tbl_products WHERE id=@id";

                //Sql command to Pass the Value
                SqlCommand cmd = new SqlCommand(sql, conn);

                //PAssing the values using cmd
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open Database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the Query is executed sucessfully then the values of the rows is greater than 0 else it is less than 
                if (rows > 0)
                {
                    //Query Executed Sucessfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Search Method for Product Module

        public DataTable Search(string keywords)
        {
            //SQL Connection for Databse Connection
            SqlConnection conn = new SqlConnection(myconnstrng);


            //Creating Datatable to Hold the data from the Database Tempropray
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Search  Products from Database
                String sql = "SELECT * FROM tbl_products WHERE id LIKE '%" + keywords + "%'  OR  name LIKE '%" +
                             keywords + "%' OR category  LIKE '%" + keywords + "%'";

                //Create SQL Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting Data From Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Fill the Data Adapter
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion

        #region Method to Search Product in Transaction Module

        public productsBLL GetProductsforTransaction(string keyword)
        {
            //Create an objeect of productsBLL and return it
            productsBLL p = new productsBLL();

            //SQL Connection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Data Table to Store data temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write the Query to get the details
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%" + keyword +
                             "%' OR name LIKE '%" + keyword + "%'";

                //Create Sql Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //Opem Database Connection
                conn.Open();

                //Pass the value from adapter to dt
                adapter.Fill(dt);

                //If we have an values on dt then set the values to productsBLL 
                if (dt.Rows.Count > 0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return p;
        }

        #endregion

        #region Method to Get Product ID Bassed on Product Name

        public productsBLL GetDeaProductIDFromName(string ProductName)
        {
            //First Create an Object of DeaCust BLL and Return it
            productsBLL p = new productsBLL();

            //Sql Connnection here
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Data Table to Hold the Data temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Get id Based on Name
                string sql = "SELECT id FROM tbl_products WHERE name ='" + ProductName + "'";

                //Create the SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                //Passing the value from Adpter to DataTable
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                //Show the Error When Message
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return p;
        }

        #endregion

        #region Method to Get Current Quantity from the Database on Product ID

        public decimal GetProductQty(int ProductID)
        {
            //SQL Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a Decimal Variable and set its default value to 0
            decimal qty = 0;

            //Crate DataTable to save the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Query to Get Quantity from Database
                string sql = "SELECT qty FROM tbl_products WHERE id = '" + ProductID + "'";

                //create a SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create a SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Pass the value from Data adapter to Data Table
                adapter.Fill(dt);

                //Lets check the data table has value or not
                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return qty;
        }

        #endregion

        #region Method to Update Quantity

        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            //Create a boolean Variable and Set its value ot false
            bool success = false;

            //SQL Connection to Connect Database 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write the SQL Query to Update Qty
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";

                //Create SQL Command to Pass the value into Quantity
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the Value through parameters
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //Open Database Connection
                conn.Open();

                //Create Int Variable and Check whether the query is Executed Successfully or Not
                int rows = cmd.ExecuteNonQuery();

                //Lets check if the query is executed successfully or not
                if (rows > 0)
                {
                    //Query Executed Successfully
                    success = true;
                }
                else
                {
                    //Failed to Execute the Query
                    success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }

        #endregion

        #region Method to Increase Product

        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
            //Create a Boolean Variable and Set its Value to False
            bool success = false;

            //Create SQL Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Get the Current Qty From Database on ID
                decimal currentQty = GetProductQty(ProductID);

                //Increase the Current Quantity by the Qty purchased from Dealer
                decimal NewQty = currentQty + IncreaseQty;

                //Update the Product Quantity Now
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }
        #endregion

        #region Method to Decrease  Product

        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            //Create Boolean Variable and SET its Value to false
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Get the Current Product Quantity
                decimal currentQty = GetProductQty(ProductID);

                //Decrease the Product Quantity based on product sales
                decimal NewQty = currentQty - Qty;

                //Update product in Database
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }
        #endregion

        #region Display Products Based on Categories

        public DataTable DisplayProductsByCategory(string category)
        {
            //SQL Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Display Product Based on Category
                string sql = "SELECT * FROM tbl_products WHERE category = '" + category + "'";

                SqlCommand cmd = new SqlCommand(sql,conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Fill the Data With Table
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion

    }
}