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
    class loginDAL
    {
        //Static String to Connect Database
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l) //----Here login BLL is connnected through word l----//
        {
            // Create a boolean variable and set its value to false and return to it
            bool isSuccess = false;
            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                //SQL Query to check login
                string sql =
                    "SELECT * FROM tbl_users WHERE username=@username AND password = @password AND user_type = @user_type";
                //Creating SQL command to Pass the value
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                //Checking the rows in Datatable
                if (dt.Rows.Count > 0)
                {
                    //Login Successful
                    isSuccess = true;
                }
                else
                {
                    //Login Failed
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
    }
}
