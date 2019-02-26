using System;

namespace OOP_RPG
{
    public class Armor : IBuyableItem
    {
        public string Name { get; }
        public int Defense { get; } // Base Defense
        public int MaxDefense { get => (int)(Defense * 1.5); } // Maximum Defense
        public int MinDefense { get => (int)(Defense * 0.5); } // Minimum Defense
        public int Price { get; }
        public int SellingPrice { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public bool Sold { get; set; }
        public bool IsEquipped { get; set; }
        public bool CanBeSoldMultipleTimes { get; set; }

        public Armor(string name, int defense, int price)
        {
            Name = name;
            Defense = defense;
            Price = price;
            SellingPrice = price / 2;
            ItemCategory = ItemCategoryEnum.Defence;
            ItemId = Guid.NewGuid();
            Sold = false;
            CanBeSoldMultipleTimes = false;
            IsEquipped = false;
        }

        public string ShowItemStats(int itemIndex) =>
            $"{itemIndex}. (Armor)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Defense: (+ {Defense})\n";

        public string ShowItemStats() =>
            $"(Armor)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n"+
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Defense: (+ {Defense})\n";
    }
}