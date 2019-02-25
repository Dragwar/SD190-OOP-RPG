using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Shop
    {
        public List<IBuyableItem> AllBuyableItems { get; }
        private Hero Hero { get; }

        public Shop(Hero hero)
        {
            Hero = hero;
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

        public bool CheckIfThereIsAnyStock()
        {
            if (AllBuyableItems.All(item => item.Sold))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry The Entire Shop Is Out Of Stock. . .");
                Console.ResetColor();
                return false;
            } else
            {
                return true;
            }
        }

        private void SellItem(Weapon weapon)
        {
            Hero.RemoveGoldCoins(weapon.Price);
            ToggleSoldProperty(weapon);
            Hero.WeaponsBag.Add(weapon);
        }

        private void SellItem(Armor armor)
        {
            Hero.RemoveGoldCoins(armor.Price);
            ToggleSoldProperty(armor);
            Hero.ArmorsBag.Add(armor);
        }

        public List<Weapon> GetCurrentWeapons() => AllBuyableItems
                .Where(item => (item.ItemCategory == ItemCategoryEnum.Strength) && (!item.Sold))
                .Cast<Weapon>()
                .ToList();

        public List<Armor> GetCurrentArmor() => AllBuyableItems
                .Where(item => (item.ItemCategory == ItemCategoryEnum.Defence) && (!item.Sold))
                .Cast<Armor>()
                .ToList();


        private void ToggleSoldProperty(Weapon weapon) => weapon.Sold = weapon.Sold ? false : true;
        private void ToggleSoldProperty(Armor armor) => armor.Sold = armor.Sold ? false : true;


        private string DisplayWeapons(int startingIndex = 1)
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

        private string DisplayArmor(int startingIndex = 1)
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

        private string DisplayAllItems()
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

        private void BuyWeapon()
        {
            Console.Clear();

            List<Weapon> shopWeapons = GetCurrentWeapons();

            if (shopWeapons.Any())
            {
                Console.WriteLine("================[Strength Items]================");
                Console.WriteLine(DisplayWeapons());

                Console.WriteLine("Tip: input item number to buy it");
                bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int inputtedIndex);


                Weapon selectedWeapon = isNumber && inputtedIndex > 0 && inputtedIndex <= shopWeapons.Count ? shopWeapons[inputtedIndex - 1] : null;


                if (isNumber && selectedWeapon != null && selectedWeapon.Price <= Hero.GoldCoins)
                {
                    SellItem(selectedWeapon);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"You just bought a new {selectedWeapon.Name}\n\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nothing Was Bought . . .");
                    Console.WriteLine("Because one of the following reasons:");
                    Console.WriteLine("- item's price was greater than your current Gold Coins");
                    Console.WriteLine("- input wasn't an item number\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry The Shop Is Out Of Stock. . .");
                Console.ResetColor();
            }
        }

        private void BuyArmor()
        {
            Console.Clear();

            List<Armor> shopArmor = GetCurrentArmor();

            if (shopArmor.Any())
            {
                Console.WriteLine("================[Defense Items]================");
                Console.WriteLine(DisplayArmor());

                Console.WriteLine("Tip: input item number to buy it");
                bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int inputtedIndex);


                Armor selectedArmor = isNumber && inputtedIndex > 0 && inputtedIndex <= shopArmor.Count ? shopArmor[inputtedIndex - 1] : null;


                if (isNumber && selectedArmor != null && selectedArmor.Price <= Hero.GoldCoins)
                {
                    SellItem(selectedArmor);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"You just bought a new {selectedArmor.Name}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nothing Was Bought . . .");
                    Console.WriteLine("Because one of the following reasons:");
                    Console.WriteLine("- item's price was greater than your current Gold Coins");
                    Console.WriteLine("- input wasn't an item number\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry The Shop Is Out Of Stock. . .");
                Console.ResetColor();
            }
        }

        private void BuyAnyItem()
        {
            Console.Clear();

            List<IBuyableItem> buyableItems = AllBuyableItems.Where(item => !item.Sold).ToList();

            if (buyableItems.Any())
            {
                Console.WriteLine("================[All Items]================");
                Console.WriteLine(DisplayAllItems());

                Console.WriteLine("Tip: input item number to buy it");
                bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int inputtedIndex);


                IBuyableItem selectedItem = isNumber && inputtedIndex > 0 && inputtedIndex <= buyableItems.Count ? buyableItems[inputtedIndex - 1] : null;


                if (isNumber && selectedItem != null && selectedItem.Price <= Hero.GoldCoins)
                {
                    if (selectedItem.ItemCategory == ItemCategoryEnum.Strength)
                    {
                        SellItem((Weapon)selectedItem);
                    }
                    else
                    {
                        SellItem((Armor)selectedItem);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"You just bought a new {selectedItem.Name}\n\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nothing Was Bought . . .");
                    Console.WriteLine("Because one of the following reasons:");
                    Console.WriteLine("- item's price was greater than your current Gold Coins");
                    Console.WriteLine("- input wasn't an item number\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry The Shop Is Out Of Stock. . .");
                Console.ResetColor();
            }
        }

        public void OpenShopAndTakeUserOrder()
        {
            string userInput = "";

            while (userInput != "4")
            {
                Console.Title = $"Spend Your Gold Coins | Current Gold Coins: {Hero.GoldCoins}";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***** Shop ******\n");
                Console.ResetColor();

                Console.WriteLine("1. Buy Weapons");
                Console.WriteLine("2. Buy Armor");
                Console.WriteLine("3. Buy Any Type Of Item");
                Console.WriteLine("4. Exit");

                userInput = Console.ReadLine().Trim();


                if (userInput == "1")
                {
                    BuyWeapon();
                }
                else if (userInput == "2")
                {
                    BuyArmor();
                }
                else if (userInput == "3")
                {
                    BuyAnyItem();
                }
            } // End of While Loop
        }
    }
}
