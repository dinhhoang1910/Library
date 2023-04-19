using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Lab4
{
    class itemDAO
    {
        public static void Insert(item i)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Items ( ItemTypeID , ItemName , ItemDescription , SellerID , MinimumBidIncrement , EndDateTime , CurrentPrice )\n"
                                            + "VALUES(@ItemTypeID , @ItemName , @ItemDescription , @SellerID , @MinimumBidIncrement , @EndDateTime , @CurrentPrice)");
            cmd.Parameters.AddWithValue("@ItemTypeID", i.ItemTypeID);
            cmd.Parameters.AddWithValue("@ItemName", i.ItemName);
            cmd.Parameters.AddWithValue("@ItemDescription", i.ItemDes);
            cmd.Parameters.AddWithValue("@SellerID", i.SellerID);
            cmd.Parameters.AddWithValue("@MinimumBidIncrement", i.MinBidIncre);
            cmd.Parameters.AddWithValue("@EndDateTime", i.EndDate);
            cmd.Parameters.AddWithValue("@CurrentPrice", i.CurrentPrice);

            DAO.UpdateTable(cmd);
        }
    }
}
