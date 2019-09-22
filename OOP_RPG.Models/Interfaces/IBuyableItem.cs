using OOP_RPG.Models.Enumerations;
using System;

namespace OOP_RPG.Models.Interfaces
{
    public interface IBuyableItem
    {
        ItemCategoryEnum ItemCategory { get; }
        Guid ItemId { get; }
        string Name { get; }
        int Price { get; }
        int SellingPrice { get; }
        bool Sold { get; set; }
        bool CanBeSoldMultipleTimes { get; set; }
        string ShowItemStats();
        string ShowItemStats(int itemIndex);
    }
}
