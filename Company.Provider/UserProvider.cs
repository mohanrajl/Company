using Company.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Company.Provider
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
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
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
                            User user = new User
                            {
                                Id = Convert.ToInt32(users.Rows[i]["Id"].ToString()),
                                Name = users.Rows[i]["Name"].ToString().Trim(),
                                Password = users.Rows[i]["Password"].ToString().Trim(),
                                Email = users.Rows[i]["Email"] != null ? users.Rows[i]["Email"].ToString().Trim() : string.Empty,
                                Admin = (bool)users.Rows[i]["Admin"],
                                Active = (bool) users.Rows[i]["Active"]
                            };

                            userlist.Add(user);
                        }
                    }
                }

                return userlist;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);                
            }
            finally
            {
                con.Close();
            }
        }

        public string CreateUser(User user)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_InsertUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@admin", user.Admin ? 1 : 0);
                cmd.Parameters.AddWithValue("@active", user.Active ? 1 : 0);
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
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_UpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@admin", user.Admin ? 1 : 0);
                cmd.Parameters.AddWithValue("@active", user.Active ? 1 : 0);
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
