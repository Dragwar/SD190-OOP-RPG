using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class HealthPotion : IBuyableItem
    {
        public string Name { get; }
        public int HealAmount { get; }
        public int Price { get; }
        public int SellingPrice { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public bool Sold { get; set; }
        public bool IsEquipped { get; set; }
        public bool CanBeSoldMultipleTimes { get; set; }

        public HealthPotion(string name, int healAmount, int price)
        {
            Name = name;
            HealAmount = healAmount;
            Price = price;
            SellingPrice = price / 2;
            ItemCategory = ItemCategoryEnum.CurrentHP;
            ItemId = Guid.NewGuid();
            Sold = false;
            CanBeSoldMultipleTimes = true;
            IsEquipped = false;
        }

        public string ShowItemStats(int itemIndex) =>
            $"{itemIndex}. (Healing Item)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Heal Amount: (+ {HealAmount} cHP)\n";

        public string ShowItemStats() =>
            $"(Healing Item)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price} Gold {(Price > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {SellingPrice} Gold {(SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Heal Amount: (+ {HealAmount} cHP)\n";
    }
}
