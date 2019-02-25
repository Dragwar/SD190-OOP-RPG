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

                new HealthPotion("Health Potion", 7, 5),
                new HealthPotion("Strong Health Potion", 11, 10),
                new HealthPotion("Great Health Potion", 16, 15),
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
            }
            else
            {
                return true;
            }
        }

        private void SellItem(IBuyableItem item)
        {
            Hero.RemoveGoldCoins(item.Price);
            ToggleSoldProperty(item);

            if (item.ItemCategory == ItemCategoryEnum.Strength)
            {
                Hero.WeaponsBag.Add((Weapon)item);
            }
            else if (item.ItemCategory == ItemCategoryEnum.Defence)
            {
                Hero.ArmorBag.Add((Armor)item);
            }
            else
            {
                Hero.HealthPotionBag.Add((HealthPotion)item);
            }
        }


        public List<Weapon> GetCurrentWeapons() => AllBuyableItems
                .Where(item => (item.ItemCategory == ItemCategoryEnum.Strength) && (!item.Sold || item.CanBeSoldMultipleTimes))
                .Cast<Weapon>()
                .ToList();

        public List<Armor> GetCurrentArmor() => AllBuyableItems
                .Where(item => (item.ItemCategory == ItemCategoryEnum.Defence) && (!item.Sold || item.CanBeSoldMultipleTimes))
                .Cast<Armor>()
                .ToList();


        private void ToggleSoldProperty(IBuyableItem item) => item.Sold = item.Sold ? false : true;


        private void DisplayAllItems()
        {
            List<IBuyableItem> allItems = AllBuyableItems.Where(item => (!item.Sold || item.CanBeSoldMultipleTimes)).ToList();

            for (int i = 1; i < allItems.Count + 1; i++)
            {
                if (allItems[i - 1].ItemCategory == ItemCategoryEnum.Strength)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(allItems[i - 1].ShowItemStats(i));
                }
                else if (allItems[i - 1].ItemCategory == ItemCategoryEnum.Defence)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(allItems[i - 1].ShowItemStats(i));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(allItems[i - 1].ShowItemStats(i));
                }
                Console.ResetColor();
            }
        }


        private void BuyItem()
        {
            Console.Clear();

            List<IBuyableItem> buyableItems = AllBuyableItems.Where(item => (!item.Sold || item.CanBeSoldMultipleTimes)).ToList();

            if (buyableItems.Any())
            {
                Console.WriteLine("================[All Items]================");
                DisplayAllItems();

                Console.WriteLine("Tip: input item number to buy it");
                bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int inputtedIndex);


                IBuyableItem selectedItem = isNumber && inputtedIndex > 0 && inputtedIndex <= buyableItems.Count ? buyableItems[inputtedIndex - 1] : null;


                if (isNumber && selectedItem != null && selectedItem.Price <= Hero.GoldCoins)
                {
                    SellItem(selectedItem);

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
                Console.WriteLine("Sorry The Shop Is Out Of Stock . . .");
                Console.ResetColor();
            }
        }

        public void OpenShopAndTakeUserOrder()
        {
            string userInput = "";

            while (userInput != "2")
            {
                Console.Title = $"Spend Your Gold Coins | Current Gold Coins: {Hero.GoldCoins}";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***** Shop ******\n");
                Console.ResetColor();

                Console.WriteLine("1. Buy Any Type Of Item");
                Console.WriteLine("2. Exit");

                userInput = Console.ReadLine().Trim();


                if (userInput == "1")
                {
                    BuyItem();
                }
            } // End of While Loop
        }
    }
}
