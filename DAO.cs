using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Lab4
{
    class DAO
    {
        static string strConnection;

        public static DataTable GetDataTable(string sql)
        {
            try
            {
                strConnection = ConfigurationManager.ConnectionStrings["AuctionConnectionString"]
                    .ConnectionString;
                SqlConnection conn = new SqlConnection(strConnection);

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static DataTable GetDataTable(SqlCommand cmd)
        {
            try
            {
                strConnection = ConfigurationManager.ConnectionStrings["AuctionConnectionString"]
                    .ConnectionString;
                SqlConnection conn = new SqlConnection(strConnection);

                cmd.Connection = conn;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void UpdateTable(SqlCommand cmd)
        {
            try
            {
                strConnection = ConfigurationManager.ConnectionStrings["AuctionConnectionString"]
                    .ConnectionString;
                SqlConnection conn = new SqlConnection(strConnection);

                cmd.Connection = conn;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
