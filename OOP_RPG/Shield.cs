﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Shield : IBuyableItem
    {
        public string Name { get; }
        public int Defense { get; private set; } // Base Damage
        public int MaxDefense { get => (int)(Defense * 1.5); } // Maximum Damage
        public int MinDefense { get => (int)(Defense * 0.5); } // Minimum Damage
        public int Price { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public bool Sold { get; set; }
        public bool IsEquipped { get; set; }
        public bool CanBeSoldMultipleTimes { get; set; }
        public int SellingPrice { get; }

        public Shield(string name, int defense, int price)
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
            $"{itemIndex}. (Shield)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Defense: (+ {Defense})\n";

        public string ShowItemStats() =>
            $"(Shield)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Defense: (+ {Defense})\n";
    }
}