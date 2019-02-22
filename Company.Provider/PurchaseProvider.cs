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
    public class PurchaseProvider
    {
        public List<Purchase> GetPurchases()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<Purchase> purchaselist = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_GetPurchases", con)
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
                    purchaselist = new List<Purchase>();
                    DataTable users = ds.Tables[0];
                    if (users.Rows.Count > 0)
                    {
                        for (int i = 0; i < users.Rows.Count; i++)
                        {
                            Purchase purchase = new Purchase
                            {
                                Id = Convert.ToInt32(users.Rows[i]["Id"].ToString()),
                                UserId = Convert.ToInt32(users.Rows[i]["UserId"].ToString()),
                                Date = Convert.ToDateTime(users.Rows[i]["PurchaseDate"].ToString()),
                                Description = users.Rows[i]["Description"].ToString().Trim(),
                                NetAmount = Convert.ToDecimal(users.Rows[i]["NetAmount"].ToString().Trim()),
                                VatApplied = Convert.ToBoolean(users.Rows[i]["VatApplied"].ToString().Trim()),
                                Active = Convert.ToBoolean(users.Rows[i]["Active"].ToString().Trim())
                            };

                            purchaselist.Add(purchase);
                        }
                    }
                }

                return purchaselist;
            }
            catch
            {
                return purchaselist;
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

        public string InsertPurchase(Purchase purchase)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_InsertPurchase", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@userId", purchase.UserId);
                cmd.Parameters.AddWithValue("@purchaseDate", purchase.Date);
                cmd.Parameters.AddWithValue("@description", purchase.Description);
                cmd.Parameters.AddWithValue("@netAmount", purchase.NetAmount);
                cmd.Parameters.AddWithValue("@vatApplied", purchase.VatApplied ? 1 : 0);
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
