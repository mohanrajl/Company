using Company.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Company.Provider
{
    public class PurchaseProvider
    {
        public List<Purchase> GetPurchases(int userId)
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

                cmd.Parameters.AddWithValue("@userId", userId);
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
                    DataTable purchases = ds.Tables[0];
                    if (purchases.Rows.Count > 0)
                    {
                        for (int i = 0; i < purchases.Rows.Count; i++)
                        {
                            Purchase purchase = new Purchase
                            {
                                Id = Convert.ToInt32(purchases.Rows[i]["Id"].ToString()),
                                UserId = Convert.ToInt32(purchases.Rows[i]["UserId"].ToString()),
                                Date = Convert.ToDateTime(purchases.Rows[i]["PurchaseDate"].ToString()),
                                Description = purchases.Rows[i]["Description"].ToString().Trim(),
                                NetAmount = Convert.ToDecimal(purchases.Rows[i]["NetAmount"].ToString().Trim()),
                                VatApplied = Convert.ToBoolean(purchases.Rows[i]["VatApplied"].ToString().Trim()),
                                Active = Convert.ToBoolean(purchases.Rows[i]["Active"].ToString().Trim())
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

        public string CreatePurchase(Purchase purchase)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_InsertPurchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", purchase.UserId);
                cmd.Parameters.AddWithValue("@purchaseDate", purchase.Date.ToString("dd/MM/yyyy"));
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
    }
}
