using OOP_RPG.Models.Interfaces;

namespace OOP_RPG.Models.Items
{
    public class Weapon : IWeapon
    {
        public string Name { get; }
        public ItemStat Strength { get; }
        public ItemPrice Price { get; }
        public bool IsEquipped { get; set; }

        public Weapon(string name, int strength, int price)
        {
            Name = name;
            Strength = new ItemStat(baseValue: strength);
            Price = new ItemPrice(buyingPrice: price);
        }

        public string ItemStatsAsString(int itemIndex) =>
            $"{itemIndex}. (Weapon)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price.BuyingPrice} Gold {(Price.BuyingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {Price.SellingPrice} Gold {(Price.SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Strength: (+ {Strength.BaseValue})\n";

        public string ItemStatsAsString() =>
            $"(Weapon)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price.BuyingPrice} Gold {(Price.BuyingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {Price.SellingPrice} Gold {(Price.SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Strength: (+ {Strength.BaseValue})\n";
    }
}