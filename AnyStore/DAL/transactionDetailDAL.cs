﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;

namespace AnyStore.DAL
{
    class transactionDetailDAL
    {
        //Create a connection string variable
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Method for Transaction Detail

        public bool InsertTransactionDetail(transcationDetailsBLL td)
        {
            //Create a boolean value and set its default value to false
            bool isSuccess = false;

            //Create a database connection here
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Sql Query to Insert Transaction Details
                string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";
                
                //Passing the value to the SQL Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the Values using cmd
                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("total", td.total);
                cmd.Parameters.AddWithValue("dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

                //Open Sql Database Connnection 
                conn.Open();

                //declare the int variable and execute the Query
                int rows = cmd.ExecuteNonQuery();

                if (rows>0)
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
                //Close Databse Connection
                conn.Close();
            }

            return isSuccess;
        }

        #endregion
    }
}
