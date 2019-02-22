using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Shop
    {
        public List<IBuyableItem> AllBuyableItems { get; set; }

        public Shop()
        {
            AllBuyableItems = new List<IBuyableItem>()
            {
                new Weapon("Sword", 3, 10),
                new Weapon("Axe", 4, 12),
                new Weapon("Longsword", 7, 15),

                new Armor("Wooden Armor", 10, 8),
                new Armor("Metal Armor", 12, 14),
                new Armor("Golden Armor", 15, 18),
            };
        }

        public List<Weapon> GetCurrentWeapons() => AllBuyableItems
                .Where(item => (item.ItemCategory == ItemCategoryEnum.Strength) && (!item.Sold))
                .ToList()
                .Cast<Weapon>()
                .ToList();

        public List<Armor> GetCurrentArmor() => AllBuyableItems
                .Where(item => (item.ItemCategory == ItemCategoryEnum.Defence) && (!item.Sold))
                .ToList()
                .Cast<Armor>()
                .ToList();


        public void ToggleSoldProperty(Weapon weapon) => weapon.Sold = weapon.Sold ? false : true;
        public void ToggleSoldProperty(Armor armor) => armor.Sold = armor.Sold ? false : true;


        public string DisplayWeapons(int startingIndex = 1)
        {
            List<Weapon> allWeapons = GetCurrentWeapons();

            string output = "";

            for (int i = startingIndex; i < allWeapons.Count + 1; i++)
            {
                output += $" {i}. ({(allWeapons[i - 1].ItemCategory == ItemCategoryEnum.Strength ? "Weapon" : "Armor")})\n";
                output += $"    - Name: {allWeapons[i - 1].Name}\n";
                output += $"    - Cost: {allWeapons[i - 1].Price}\n";
                output += $"    - {allWeapons[i - 1].ItemCategory}: {allWeapons[i - 1].ModifiesHeroStat}\n\n";
            }

            return output;
        }

        public string DisplayArmor(int startingIndex = 1)
        {
            List<Armor> allArmor = GetCurrentArmor();

            string output = "";

            for (int i = startingIndex; i < allArmor.Count + 1; i++)
            {
                output += $" {i}. ({(allArmor[i - 1].ItemCategory == ItemCategoryEnum.Strength ? "Weapon" : "Armor")})\n";
                output += $"    - Name: {allArmor[i - 1].Name}\n";
                output += $"    - Cost: {allArmor[i - 1].Price}\n";
                output += $"    - {allArmor[i - 1].ItemCategory}: {allArmor[i - 1].ModifiesHeroStat}\n\n";
            }

            return output;
        }

        public string DisplayAllItems()
        {
            List<IBuyableItem> allItems = AllBuyableItems.Where(item => !item.Sold).ToList();

            string output = "";

            for (int i = 1; i < allItems.Count + 1; i++)
            {
                output += $" {i}. ({(allItems[i - 1].ItemCategory == ItemCategoryEnum.Strength ? "Weapon" : "Armor")})\n";
                output += $"    - Name: {allItems[i - 1].Name}\n";
                output += $"    - Cost: {allItems[i - 1].Price}\n";
                output += $"    - {allItems[i - 1].ItemCategory}: {allItems[i - 1].ModifiesHeroStat}\n\n";
            }

            return output;
        }



    }
}
