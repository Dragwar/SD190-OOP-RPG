using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Interfaces;
using System;

namespace OOP_RPG.ConsoleGame
{
    public class Weapon : IBuyableItem
    {
        public string Name { get; }
        public int Strength { get; } // Base Damage
        public int MaxDamage { get => (int)(Strength * 1.5); } // Maximum Damage
        public int MinDamage { get => (int)(Strength * 0.5); } // Minimum Damage
        public int Price { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public bool Sold { get; set; }
        public bool IsEquipped { get; set; }
        public bool CanBeSoldMultipleTimes { get; set; }
        public int SellingPrice { get; }

        public Weapon(string name, int strength, int price)
        {
            Name = name;
            Strength = strength;
            Price = price;
            SellingPrice = price / 2;
            ItemCategory = ItemCategoryEnum.Strength;
            ItemId = Guid.NewGuid();
            Sold = false;
            CanBeSoldMultipleTimes = false;
            IsEquipped = false;
        }

        public string ShowItemStats(int itemIndex) =>
            $"{itemIndex}. (Weapon)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Strength: (+ {Strength})\n";

        public string ShowItemStats() =>
            $"(Weapon)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Strength: (+ {Strength})\n";
    }
}