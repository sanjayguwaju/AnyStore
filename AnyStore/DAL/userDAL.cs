using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;

namespace AnyStore.DAL
{
    class userDAL
    {
        /* DAL = DATA Access Layer
         */
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database

        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_users";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
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

        #region Insert Data in Database

        public bool Insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql =
                    "INSERT INTO tbl_users(first_name,last_name,email,username,password,contact,address,gender,user_type,added_date,added_by) VALUES (@first_name,@last_name,@email,@username,@password,@contact,@address,@gender,@user_type,@added_date,@added_by)";
                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@first_name", u.first_name);
                        cmd.Parameters.AddWithValue("@last_name", u.last_name);
                        cmd.Parameters.AddWithValue("@email", u.email);
                        cmd.Parameters.AddWithValue("@username", u.username);
                        cmd.Parameters.AddWithValue("@password", u.password);
                        cmd.Parameters.AddWithValue("@contact", u.contact);
                        cmd.Parameters.AddWithValue("@address", u.address);
                        cmd.Parameters.AddWithValue("@gender", u.gender);
                        cmd.Parameters.AddWithValue("@user_type", u.user_type);
                        cmd.Parameters.AddWithValue("@added_date", u.added_date);
                        cmd.Parameters.AddWithValue("@added_by", u.added_by);

                        conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the query is executed Sucessfully then the values to the rows will be greater tha 0 else it will be less than 0
                if (rows > 0)
                {
                    //Query Sucessfull
                    isSuccess = true;

                }
                else
                {
                    //Query Failed
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

        #region Update Data in Database

        public bool Update(userBLL u)
        {
            bool isSucess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql =
                    "UPDATE tbl_users SET first_name = @first_name,last_name = @last_name,email = @email,username = @username,password = @password,contact = @contact,address =@address,gender= @gender,user_type=@user_type,added_date=@added_date,added_by=@added_by WHERE id =@id";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@first_name", u.first_name);
                        cmd.Parameters.AddWithValue("@last_name", u.last_name);
                        cmd.Parameters.AddWithValue("@email", u.email);
                        cmd.Parameters.AddWithValue("@username", u.username);
                        cmd.Parameters.AddWithValue("@password", u.password);
                        cmd.Parameters.AddWithValue("@contact", u.contact);
                        cmd.Parameters.AddWithValue("@address", u.address);
                        cmd.Parameters.AddWithValue("@gender", u.gender);
                        cmd.Parameters.AddWithValue("@user_type", u.user_type);
                        cmd.Parameters.AddWithValue("@added_date", u.added_date);
                        cmd.Parameters.AddWithValue("@added_by", u.added_by);
                        cmd.Parameters.AddWithValue("@id", u.id);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            // Query Sucessful
                            isSucess = true;
                        }
                        else
                        {
                            //Query failed
                            isSucess = false;
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

            return isSucess;

        }
        #endregion

        #region Delete Data from Database

        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //Query Sucessfull
                    isSuccess = true;
                }
                else
                {
                    //Query Failed
                    isSuccess = false;
                }

            }
            catch(Exception ex)
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

        #region Search User on Database usingKeywords

        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keywords+ "%' OR first_name LIKE '%" + keywords + "%' OR username LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
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

        #region Getting Data from Username

        public userBLL GetIDFromUsername(string username)
        {
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROm tbl_users Where username='" + username + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return u;
        }
        #endregion
    }
}
