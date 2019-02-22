using System;

namespace OOP_RPG
{
    public class Armor : IBuyableItem
    {
        public string Name { get; }
        public int Defense { get; }
        public int Price { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public int ModifiesHeroStat { get; }
        public bool Sold { get; set; }

        public Armor(string name, int defense, int price)
        {
            Name = name;
            Defense = defense;
            Price = price;
            ItemCategory = ItemCategoryEnum.Defence;
            ItemId = Guid.NewGuid();
            ModifiesHeroStat = defense;
            Sold = false;
        }
    }
}