using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.UI;

namespace AnyStore.DAL
{
    class DeaCustDAL
    {
        //Static String Method for Database Connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method for Dealer and Customer

        public DataTable Select()
        {
            //SQL Connection for the Database Connection
            SqlConnection conn  = new SqlConnection(myconnstrng);

            //DataTable to hold the value form database and return it
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Query to Select all the Data from Database
                string sql = "SELECT * FROM tbl_dea_cust";

                //Creating SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creating SQL Data Adapter to Store Date from Database Temporarily
                SqlDataAdapter adapter  = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Passing the value from Data Adapter to Data Table
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

        #region Insert Method to Add Details of Dealer or Customer

        public bool Insert(DeaCustBLL dc)
        {
            //Creating SQL Connection First 
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create and Boolean Value and Set its default value to False
            bool isSuccess = false;

            try
            {
                //Write SQL Query to Insert Details of Dealer or Customer
                string sql =
                    "INSERT INTO tbl_dea_cust(type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                //SQL Command to Pass the Values to Query and Execute
                SqlCommand cmd = new SqlCommand(sql,conn);

                //Passing the values using Parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                //Open Database Connection
                conn.Open();

                //Int variable ot check whether the query is executed successfully or not
                int rows = cmd.ExecuteNonQuery();

                //IF the query is executed sucessfully then the value of rows will be greater than 0 else value will be less than 0
                if (rows>0)
                {
                    //Query Executed Sucessfully
                    isSuccess = true;
                }
                else
                {
                    //Query Execution is Failed
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

        #region Update Method for Dealer and Customer Module

        public bool Update(DeaCustBLL dc)
        {
            //SQL Connection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create Boolean variables and set its default value to false
            bool isSuccess = false;

            try
            {
                //SQL Query to Update data in database
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";
                //Create SQL Command to Pass the Value in SQL
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the values through parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open the Database Connection
                conn.Open();

                //Int variable to check if the query is executed succesfuly or not
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //Query is Executed Sucessfully
                    isSuccess = true;
                }
                else
                {
                    //Query Execution Failed
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

        #region Delete Method for Dealer and Customer Module

        public bool Delete(DeaCustBLL dc)
        {
            //SQL Connection for the Databse connection
            SqlConnection conn  = new SqlConnection(myconnstrng);

            //Create a Boolean variable and set its default value to false
            bool isSuccess = false;

            try
            {
                //SQL Query to Delete Data form Database
                string sql = "DELETE FROM tbl_dea_cust WHERE id =@id";

                //SQL Command to pass the value
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the Value
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open Database Connection
                conn.Open();
                //Integer variable to check the Query is executed sucesfully or Not
                int rows = cmd.ExecuteNonQuery();

                //Checking the Query is sucessfull ot not
                if (rows>0)
                {
                    //Query Execution is Sucessfull
                    isSuccess = true;
                }
                else
                {
                    //Query Execution is Failed
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

        #region Search Method for Dealer and Customer Module

        public DataTable Search(string keyword)
        {
            //Create a SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Creating a DataTable and Returning it's Value
            DataTable dt = new DataTable();

            try
            {
                //Write the Query to Search Dealer or Customer Based in ID ,Type and Name
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%" + keyword + "%' OR type LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

                //SqlCommand to Execute the Query
                SqlCommand cmd = new SqlCommand(sql,conn);

                //Sql Data Adapter to hold the Data Temperarily form  Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Pass the Value from adapter to the data table
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;

        }

        #endregion

        #region Method to Search Dealer or Customer for Transcation Module

        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            //Create an object fro DeaCustBLL class
            DeaCustBLL dc = new DeaCustBLL();

            //Create a Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a DataTable to hold the value temprorily
            DataTable dt = new DataTable();

            try
            {
                //Write a SQL Query to Search Dealer or Customer Based on Keywords
                string sql = "SELECT name, email, contact, address from tbl_dea_cust WHERE id LIKE '%"+keyword+ "%' OR name LIKE '%" + keyword + "%'";

                //Create SQl Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //Open the Database Connection
                conn.Open();

                //Transfer the data from sqlData Adapter to Data Table
                adapter.Fill(dt);

                //If we have values on dt we need to save it in dealerCustomer BLL
                if (dt.Rows.Count>0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
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
            return dc;
        }
        #endregion

        #region Method to Get ID of the Dealer or Customer Based on Name

        public DeaCustBLL GetDeaCustIDFromName(string Name)
        {
            //First Create an Object of DeaCust BLL and Return it
            DeaCustBLL dc = new DeaCustBLL();

            //Sql Connnection here
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Data Table to Hold the Data temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Get id Based on Name
                string sql = "SELECT id FROM tbl_dea_cust WHERE name ='" + Name + "'";

                //Create the SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);

                conn.Open();

                //Passing the value from Adpter to DataTable
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return dc;
        }
        #endregion
    }
}