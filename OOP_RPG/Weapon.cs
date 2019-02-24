using System;
using System.Linq;

namespace OOP_RPG
{
    public class Weapon : IBuyableItem
    {
        public string Name { get; }
        public int Strength { get; } // Base Damage
        public int MaxDamage { get => Strength * 2; } // Maximum Damage
        public int MinDamage { get => Strength / 2; } // Minimum Damage
        public int Price { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public int ModifiesHeroStat { get; }
        public bool Sold { get; set; }
        public bool IsEquipped { get; set; }

        public Weapon(string name, int strength, int price)
        {
            Name = name;
            Strength = strength;
            Price = price;
            ItemCategory = ItemCategoryEnum.Strength;
            ItemId = Guid.NewGuid();
            ModifiesHeroStat = strength;
            Sold = false;
            IsEquipped = false;
        }
    }
}