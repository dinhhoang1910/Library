using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    class item
    {
        int itemID, itemTypeID;
        string itemName, itemDes;
        int sellerID;
        double minBidIncre;
        DateTime endDate;
        double currentPrice;

        public item()
        {
        }

        public item(int itemID, int itemTypeID, string itemName, string itemDes, int sellerID, double minBidIncre, DateTime endDate, double currentPrice)
        {
            this.itemID = itemID;
            this.itemTypeID = itemTypeID;
            this.itemName = itemName;
            this.itemDes = itemDes;
            this.sellerID = sellerID;
            this.minBidIncre = minBidIncre;
            this.endDate = endDate;
            this.currentPrice = currentPrice;
        }

        public int ItemID { get => itemID; set => itemID = value; }
        public int ItemTypeID { get => itemTypeID; set => itemTypeID = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string ItemDes { get => itemDes; set => itemDes = value; }
        public int SellerID { get => sellerID; set => sellerID = value; }
        public double MinBidIncre { get => minBidIncre; set => minBidIncre = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public double CurrentPrice { get => currentPrice; set => currentPrice = value; }
        public string ItemDescription { get; internal set; }
        public double Increment { get; internal set; }
        public DateTime CurrentDate { get; internal set; }
    }
}
