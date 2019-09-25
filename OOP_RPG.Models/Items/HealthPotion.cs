using OOP_RPG.Models.Interfaces;

namespace OOP_RPG.Models.Items
{
    public class HealthPotion : IHealthPotion
    {
        public string Name { get; }
        public ItemStat HealAmount { get; }
        public ItemPrice Price { get; }
        public bool IsEquipped { get; set; }

        public HealthPotion(string name, int healAmount, int price)
        {
            Name = name;
            HealAmount = new ItemStat(baseValue: healAmount);
            Price = new ItemPrice(buyingPrice: price);
        }

        public string ItemStatsAsString(int itemIndex) =>
            $"{itemIndex}. (Healing Item)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price.BuyingPrice} Gold {(Price.BuyingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {Price.SellingPrice} Gold {(Price.SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Heal Amount: (+ {HealAmount.BaseValue} cHP)\n";

        public string ItemStatsAsString() =>
            $"(Healing Item)\n" +
            $"   - Name: {Name}\n" +
            $"   - Cost: {Price.BuyingPrice} Gold {(Price.BuyingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - SellingPrice: {Price.SellingPrice} Gold {(Price.SellingPrice > 1 ? $"Coins" : $"Coin")}\n" +
            $"   - Heal Amount: (+ {HealAmount.BaseValue} cHP)\n";

        public override string ToString() =>
            $"============({Name})============\n" +
            $"Worth: {Price.SellingPrice} Gold Coins\n" +
            $"HealAmount: (+ {HealAmount.BaseValue})";
    }
}
