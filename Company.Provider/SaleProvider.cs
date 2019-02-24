using Company.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Company.Provider
{
    public class SaleProvider
    {
        public List<Sale> GetSales(int userId)
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
                    salelist = new List<Sale>();
                    DataTable sales = ds.Tables[0];
                    if (sales.Rows.Count > 0)
                    {
                        for (int i = 0; i < sales.Rows.Count; i++)
                        {
                            Sale sale = new Sale
                            {
                                Id = Convert.ToInt32(sales.Rows[i]["Id"].ToString()),
                                UserId = Convert.ToInt32(sales.Rows[i]["UserId"].ToString()),
                                Date = Convert.ToDateTime(sales.Rows[i]["SaleDate"].ToString()),
                                Description = sales.Rows[i]["Description"].ToString().Trim(),
                                NetAmount = Convert.ToDecimal(sales.Rows[i]["NetAmount"].ToString().Trim()),
                                VatApplied = Convert.ToBoolean(sales.Rows[i]["VatApplied"].ToString().Trim()),
                                Active = Convert.ToBoolean(sales.Rows[i]["Active"].ToString().Trim())
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

        public string CreateSale(Sale sale)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyDbConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_InsertSale", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", sale.UserId);
                cmd.Parameters.AddWithValue("@saleDate", sale.Date.ToString("dd/MM/yyyy"));
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
    }
}
