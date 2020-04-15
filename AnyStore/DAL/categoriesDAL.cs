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
    class categoriesDAL
    {
        //Static String Method for Database Connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method

        public DataTable Select()
        {
            //Creating Database Connection 
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                //Writing SQL Query to get all the data from Database
                string sql = "SELECT * FROM tbl_categories";

                SqlCommand cmd = new SqlCommand(sql,conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Addding the value from adapter to Data Table form dt
                adapter.Fill(dt); 

                /* From------Creating Database Connection-----To---------Adding the value from adapter to the Data Table from dt
                 is Process of getting data from database to this app it will make connection to the database and gram the information from the tabel
                  this is really important step -----TO REMEMBER-------*/
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

        #region Insert New Category

        public bool Insert(categoriesBLL c)
        {
            // Creating a Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing Query to Add New Category
                string sql = "INSERT INTO tbl_categories (title,description,added_date,added_by) VALUES (@title,@description,@added_date,@added_by)";

                //Creating SQL Command to pass values in our Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Values through Parameters
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                //Open Database Connection
                conn.Open();

                //Creating the int variable to execute the Query
                int rows = cmd.ExecuteNonQuery();

                //If the query is executed successfully then its value will be greater than 0 then it's value will be true else it will be false and it's value will be less than 0
                if (rows>0)
                {
                   //Query Executed Sucessfully 
                   isSuccess = true;

                }
                else
                {
                    //Query Execution is failed
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

        #region Update Method

        public bool Update(categoriesBLL c)
        {
            //Creating Boolean variable and set its default value to false
            bool isSuccess = false;

            //Creating SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_categories SET title=@title, description=@description,added_date=@added_date,added_by=@added_by WHERE id=@id";
                
                
                //SQL Command to Pass the Value on Sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the Value using cmd
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open the Database Connection
                conn.Open();

                //Create Int Variable to execute Query
                int rows = cmd.ExecuteNonQuery();

                // if the query is sucessfully executed then the value will be greater than 0 else the value will be less than 0
                if (rows>0)
                {
                    //Query is Executed Sucessfull then 
                    isSuccess = true;
                }
                else
                {
                    //Query is Failed to Execute then 
                    isSuccess = false;
                }
            }
            catch (Exception ex) // Here is Exception = ex
            {
                MessageBox.Show(ex.Message);  // MessageBox.Show means Show MessageBox but which type of message so that bracket is Saying if there is any Exeception then Show that MEssage so there we have written (ex.Message)
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Delete Category Method

        public bool Delete(categoriesBLL c)  // In this line we are making connection wiht our BLL(Bussiness Layer Class) to compare that each has values like string is correct or not by using Bool or we can say true or false

        {
            //Create a Boolean Variaable and set its value to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL Query to Delete from Database
                string sql = "DELETE FROM tbl_categories WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql,conn);

                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open SqlConnection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the vQuery is Executed successfully Value of rows will be greater than 0 else it will be less that 0
                if (rows>0)
                {
                    //Query Execute is Success
                    isSuccess = true;
                }
                else
                {
                    //Query Execute is Failed
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

        #region Method for Search functionality

        public DataTable Search(string keywords)
        {
            //SQL Connection for Databse Connection
            SqlConnection conn = new SqlConnection(myconnstrng);


            //Creating Datatable to Hold the data from the Database Tempropral
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Search  Categories from Database
                String sql = "SELECT * FROM tbl_categories WHERE id LIKE '%" + keywords + "%'  OR  title LIKE '%"+keywords+ "%' OR description  LIKE '%" + keywords + "%'";

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
    }
}
