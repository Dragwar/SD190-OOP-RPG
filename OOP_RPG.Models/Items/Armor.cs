using OOP_RPG.Models.Interfaces;

namespace OOP_RPG.Models.Items
{
    public class Armor : IArmor
    {
        public string Name { get; }
        public ItemStat Defense { get; }
        public ItemPrice Price { get; }
        public bool IsEquipped { get; set; }

        public Armor(string name, int defense, int price)
        {
            Name = name;
            Defense = new ItemStat(baseValue: defense);
            Price = new ItemPrice(buyingPrice: price);
        }

        public string ItemStatsAsString(int itemIndex) =>
            $"{itemIndex}. (Armor)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price.BuyingPrice} Gold {(Price.BuyingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {Price.SellingPrice} Gold {(Price.SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Defense: (+ {Defense.BaseValue})\n";

        public string ItemStatsAsString() =>
            $"(Armor)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price.BuyingPrice} Gold {(Price.BuyingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {Price.SellingPrice} Gold {(Price.SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Defense: (+ {Defense.BaseValue})\n";
    }
}