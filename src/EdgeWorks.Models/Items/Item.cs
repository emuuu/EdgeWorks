using System;
using System.Collections.Generic;
using System.Text;

namespace EdgeWorks.Models.Items
{
    public class Item
    {
        public Links _Links { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public Quality Quality { get; set; }
        public int Level { get; set; }
        public int Required_Level { get; set; }
        public Media Media { get; set; }
        public ItemClass Item_Class { get; set; }
        public ItemSubclass Item_Subclass { get; set; }
        public InventoryType Inventory_Type { get; set; }
        public int Purchase_Price { get; set; }
        public int Sell_Price { get; set; }
        public int Max_Count { get; set; }
        public bool is_Equippable { get; set; }
        public bool is_Stackable { get; set; }
        public string Description { get; set; }
    }
}
