using Company.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Provider
{
    public class SaleProvider
    {
        public List<Sale> GetSales()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<Sale> salelist = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_GetSales", con)
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
                    salelist = new List<Sale>();
                    DataTable users = ds.Tables[0];
                    if (users.Rows.Count > 0)
                    {
                        for (int i = 0; i < users.Rows.Count; i++)
                        {
                            Sale sale = new Sale
                            {
                                Id = Convert.ToInt32(users.Rows[i]["Id"].ToString()),
                                UserId = Convert.ToInt32(users.Rows[i]["UserId"].ToString()),
                                Date = Convert.ToDateTime(users.Rows[i]["SaleDate"].ToString()),
                                Description = users.Rows[i]["Description"].ToString().Trim(),
                                NetAmount = Convert.ToDecimal(users.Rows[i]["NetAmount"].ToString().Trim()),
                                VatApplied = Convert.ToBoolean(users.Rows[i]["VatApplied"].ToString().Trim()),
                                Active = Convert.ToBoolean(users.Rows[i]["Active"].ToString().Trim())
                            };

                            salelist.Add(sale);
                        }
                    }
                }

                return salelist;
            }
            catch
            {
                return salelist;
            }
            finally
            {
                con.Close();
            }
        }

        //public int DeleteUser(int saleId)
        //{
        //    SqlConnection con = null;
        //    int result;
        //    try
        //    {
        //        con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
        //        SqlCommand cmd = new SqlCommand("sp_DeleteUser", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", userId);
        //        con.Open();
        //        result = cmd.ExecuteNonQuery();
        //        return result;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public string InsertSale(Sale sale)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_InsertSale", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", sale.UserId);
                cmd.Parameters.AddWithValue("@saleDate", sale.Date);
                cmd.Parameters.AddWithValue("@description", sale.Description);
                cmd.Parameters.AddWithValue("@netAmount", sale.NetAmount);
                cmd.Parameters.AddWithValue("@vatApplied", sale.VatApplied ? 1 : 0);
                cmd.Parameters.AddWithValue("@active", 1);
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

        //public string UpdateUser(User user)
        //{
        //    SqlConnection con = null;
        //    string result = "";
        //    try
        //    {
        //        con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
        //        SqlCommand cmd = new SqlCommand("sp_UpdateUser", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", user.Id);
        //        cmd.Parameters.AddWithValue("@password", user.Password);
        //        cmd.Parameters.AddWithValue("@email", user.Email);
        //        cmd.Parameters.AddWithValue("@admin", user.Admin ? 1 : 0);
        //        cmd.Parameters.AddWithValue("@active", user.Active ? 1 : 0);
        //        con.Open();
        //        result = cmd.ExecuteScalar().ToString();
        //        return result;
        //    }
        //    catch
        //    {
        //        return result = "";
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
    }
}
