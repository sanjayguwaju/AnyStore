using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;

namespace AnyStore.DAL
{
    class transcationDAL
    {
        //Create a connection string variable
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method

        public bool Insert_Transction(transactionsBLL t, out int transcationID)
        {
            //Create a Boolean Value and set its default value to false
            bool isSuccess = false;

            //Set the Out TranscationID value to negative 1 i.e -1
            transcationID = -1;

            //Create a SQL Connection first
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL Query to Insert Transcations
                string sql = "INSERT INTO tbl_transactions(type,dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by) SELECT @@IDENTITY";

                //Sql Command to Pass the Value in Query
                SqlCommand cmd = new SqlCommand(sql,conn);

                //Passing the value to SQL Query using cmd
                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                //Open Database Connection
                conn.Open();

                //Execute the Query *************************************************************** <<<<<<<<<<<<<<<<<<<
                object o = cmd.ExecuteScalar();

                //If the query is executed sucessfully thene the value will not be null it will be null
                if (o!=null)
                {
                    //Query Executed Sucessfully
                    transcationID = int.Parse(o.ToString());
                    isSuccess = true;

                }
                else
                {
                    //Failed to execute Query
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

        #region Method to Display All the Transaction

        public DataTable DisplayAllTransactions()
        {
            //SQL Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a DataTable to Hold the Data From Database Temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write the SQL Query to Display all Transactions
                string sql = "SELECT * FROM tbl_transactions";

                //SqlCommand to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SqlDataAdapter to Hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open the Connection
                conn.Open();
                //Fill the DataGrid View
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

        #region Method to Display Transaction based on Transaction Type

        public DataTable DisplayTransactionByType(string type)
        {
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a DataTable
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Query
                string sql = "SELECT * FROM tbl_transactions WHERE type = '" + type + "'";

                //SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQL DataAdapter to hold the Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Fill the Data in the Table
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
