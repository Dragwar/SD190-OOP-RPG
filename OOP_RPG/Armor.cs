using System;

namespace OOP_RPG
{
    public class Armor : IBuyableItem
    {
        public string Name { get; }
        public int Defense { get; } // Base Defense
        public int MaxDefense { get => (int)(Defense * 1.5); } // Maximum Defense
        public int MinDefense { get => Defense / 2; } // Minimum Defense
        public int Price { get; }
        public ItemCategoryEnum ItemCategory { get; }
        public Guid ItemId { get; private set; }
        public int ModifiesHeroStat { get; }
        public bool Sold { get; set; }
        public bool IsEquipped { get; set; }

        public Armor(string name, int defense, int price)
        {
            Name = name;
            Defense = defense;
            Price = price;
            ItemCategory = ItemCategoryEnum.Defence;
            ItemId = Guid.NewGuid();
            ModifiesHeroStat = defense;
            Sold = false;
            IsEquipped = false;
        }
    }
}