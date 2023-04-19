using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    class ItemType
    {
        int itemTypeID;
        string itemTypeName;

        public ItemType()
        {
        }

        public ItemType(int itemTypeID, string itemTypeName)
        {
            this.itemTypeID = itemTypeID;
            this.itemTypeName = itemTypeName;
        }

        public int ItemTypeID { get => itemTypeID; set => itemTypeID = value; }
        public string ItemTypeName { get => itemTypeName; set => itemTypeName = value; }
    }
}
