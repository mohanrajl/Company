using Company.Web.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Company.Web
{
    public class UserProvider
    {
        public List<User> GetUsers()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<User> userlist = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("sp_GetUsers", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter
                {
                    SelectCommand = cmd
                };

                ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    userlist = new List<User>();
                    DataTable users = ds.Tables[0];
                    if (users.Rows.Count > 0)
                    {
                        for (int i = 0; i < users.Rows.Count; i++)
                        {
                            User cobj = new User
                            {
                                Id = Convert.ToInt32(users.Rows[i]["UserID"].ToString()),
                                Name = users.Rows[i]["Name"].ToString(),
                                Password = users.Rows[i]["Password"].ToString(),
                                Active = (bool) users.Rows[i]["Active"]
                            };

                            userlist.Add(cobj);
                        }
                    }
                }

                return userlist;
            }
            catch
            {
                return userlist;
            }
            finally
            {
                con.Close();
            }
        }

        public int DeleteUser(int userId)
        {
            SqlConnection con = null;
            int result;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("sp_DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

        public string InsertUser(User user)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("sp_InsertUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string UpdateUser(User user)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}