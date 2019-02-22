using System;
using System.Linq;

namespace OOP_RPG
{
    public class Weapon : IBuyableItem
    {
        public string Name { get; }
        public int Strength { get; }
        public int Price { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public int ModifiesHeroStat { get; }
        public bool Sold { get; set; }

        public Weapon(string name, int strength, int price)
        {
            Name = name;
            Strength = strength;
            Price = price;
            ItemCategory = ItemCategoryEnum.Strength;
            ItemId = Guid.NewGuid();
            ModifiesHeroStat = strength;
            Sold = false;
        }
    }
}