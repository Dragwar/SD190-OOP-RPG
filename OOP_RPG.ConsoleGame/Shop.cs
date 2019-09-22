using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.ConsoleGame
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

                new Shield("Wooden Shield", 3, 10),
                new Shield("Battle Shield", 4, 12),
                new Shield("Dragon Shield", 7, 15),

                new HealthPotion("Health Potion", 7, 5),
                new HealthPotion("Strong Health Potion", 11, 10),
                new HealthPotion("Great Health Potion", 16, 15),
            };
        }



        /*
        ======================================================================================== 
        SellItem ---> Allows transfers the sold item to the hero
        ======================================================================================== 
        */
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
                if (item is Armor)
                {
                    Hero.ArmorBag.Add((Armor)item);
                }
                else if (item is Shield)
                {
                    Hero.ShieldBag.Add((Shield)item);
                }
            }
            else
            {
                Hero.HealthPotionBag.Add((HealthPotion)item);
            }
        }


        public List<Weapon> GetCurrentWeapons() => AllBuyableItems
                .Where(item => item.ItemCategory == ItemCategoryEnum.Strength && item is Weapon && (!item.Sold || item.CanBeSoldMultipleTimes))
                .Cast<Weapon>()
                .ToList();

        public List<Armor> GetCurrentArmor() => AllBuyableItems
                .Where(item => item.ItemCategory == ItemCategoryEnum.Defence && item is Armor && (!item.Sold || item.CanBeSoldMultipleTimes))
                .Cast<Armor>()
                .ToList();

        public List<Shield> GetCurrentShields() => AllBuyableItems
                .Where(item => item.ItemCategory == ItemCategoryEnum.Defence && item is Shield && (!item.Sold || item.CanBeSoldMultipleTimes))
                .Cast<Shield>()
                .ToList();


        private void ToggleSoldProperty(IBuyableItem item) => item.Sold = item.Sold ? false : true;



        /*
        ======================================================================================== 
        DisplayAllItems ---> Prints all shop items and color codes items by their category 
        ======================================================================================== 
        */
        private void DisplayAllItems()
        {
            var allItems = AllBuyableItems.Where(item => !item.Sold || item.CanBeSoldMultipleTimes).ToList();

            for (var i = 1; i < allItems.Count + 1; i++)
            {
                if (allItems[i - 1].ItemCategory == ItemCategoryEnum.Strength)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(allItems[i - 1].ShowItemStats(i));
                }
                else if (allItems[i - 1].ItemCategory == ItemCategoryEnum.Defence)
                {
                    Console.ForegroundColor = allItems[i - 1] is Armor ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
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



        /*
        ======================================================================================== 
        BuyShopItem ---> Allows hero to buy the shops items
        ======================================================================================== 
        */
        private void SellShopItem()
        {
            Console.Clear();

            var buyableItems = AllBuyableItems.Where(item => !item.Sold || item.CanBeSoldMultipleTimes).ToList();

            if (buyableItems.Any())
            {
                Console.WriteLine("================[All Items]================");
                DisplayAllItems();

                Console.WriteLine("Tip: input item number to buy it");
                var isNumber = int.TryParse(Console.ReadLine().Trim(), out var inputtedIndex);


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



        /*
        ======================================================================================== 
        OpenShopAndTakeUserOrder ---> When hero steps into shop a list of options appear
        ======================================================================================== 
        */
        public void OpenShopAndTakeUserOrder()
        {
            var userInput = "";

            while (userInput != "3")
            {
                Console.Title = $"Spend Your Gold Coins | Current Gold Coins: {Hero.GoldCoins}";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***** Shop ******\n");
                Console.ResetColor();

                Console.WriteLine("1. Buy An Item");
                Console.WriteLine("2. Sell An Item");
                Console.WriteLine("3. Exit");

                userInput = Console.ReadLine().Trim();


                if (userInput == "1")
                {
                    SellShopItem();
                }
                else if (userInput == "2")
                {
                    BuyHeroItem();
                }
            } // End of While Loop
        }



        /*
        ======================================================================================== 
        BuyHeroItem ---> Allows hero to sell his/her items to the shop
        ======================================================================================== 
        */
        private void BuyHeroItem()
        {
            Console.Clear();

            var heroItems = Hero.GetMasterInventoryList().ToList();

            if (heroItems.Any())
            {
                for (var i = 0; i < heroItems.Count; i++)
                {
                    if (heroItems[i].ItemCategory == ItemCategoryEnum.Strength)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        if (heroItems[i] is Weapon weapon)
                        {
                            if (weapon.IsEquipped)
                            {
                                Console.WriteLine($"{i + 1}. Sell your {weapon.Name} for {weapon.SellingPrice} Gold Coins (Currently Equipped)");
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. Sell your {weapon.Name} for {weapon.SellingPrice} Gold Coins");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. Sell your {heroItems[i].Name} for {heroItems[i].SellingPrice} Gold Coins");
                        }
                    }
                    else if (heroItems[i].ItemCategory == ItemCategoryEnum.Defence)
                    {
                        Console.ForegroundColor = heroItems[i] is Armor ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
                        if (heroItems[i] is Armor armor)
                        {
                            if (armor.IsEquipped)
                            {
                                Console.WriteLine($"{i + 1}. Sell your {armor.Name} for {armor.SellingPrice} Gold Coins (Currently Equipped)");
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. Sell your {armor.Name} for {armor.SellingPrice} Gold Coins");
                            }
                        }
                        else if (heroItems[i] is Shield shield)
                        {
                            if (shield.IsEquipped)
                            {
                                Console.WriteLine($"{i + 1}. Sell your {shield.Name} for {shield.SellingPrice} Gold Coins (Currently Equipped)");
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. Sell your {shield.Name} for {shield.SellingPrice} Gold Coins");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. Sell your {heroItems[i].Name} for {heroItems[i].SellingPrice} Gold Coins");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{i + 1}. Sell your {heroItems[i].Name} for {heroItems[i].SellingPrice} Gold Coins");
                    }
                    Console.ResetColor();
                }

                Console.WriteLine("\nPlease input a item number to sell it\n");

                var isNumber = int.TryParse(Console.ReadLine().Trim(), out var userIndex);

                userIndex--;

                var invalidIndex = userIndex > heroItems.Count || userIndex < 0 ? true : false;

                if (isNumber && !invalidIndex)
                {
                    Guid foundId = heroItems[userIndex].ItemId;
                    IBuyableItem deleteThisItem = Hero.ArmorBag.Where(item => item.ItemId == foundId).FirstOrDefault();

                    if (deleteThisItem == null)
                    {
                        deleteThisItem = Hero.ShieldBag.Where(item => item.ItemId == foundId).FirstOrDefault();
                    }
                    if (deleteThisItem == null)
                    {
                        deleteThisItem = Hero.WeaponsBag.Where(item => item.ItemId == foundId).FirstOrDefault();
                    }
                    if (deleteThisItem == null)
                    {
                        deleteThisItem = Hero.HealthPotionBag.Where(item => item.ItemId == foundId).FirstOrDefault();
                    }
                    if (deleteThisItem == null)
                    {
                        throw new Exception("No Item was found to sell");
                    }

                    Hero.UnEquipItem(deleteThisItem);
                    Hero.AddGoldCoins(deleteThisItem.SellingPrice);
                    if (deleteThisItem.ItemCategory == ItemCategoryEnum.Strength)
                    {
                        Hero.WeaponsBag.Remove((Weapon)deleteThisItem);
                    }
                    else if (deleteThisItem.ItemCategory == ItemCategoryEnum.Defence)
                    {
                        if (deleteThisItem is Armor)
                        {
                            Hero.ArmorBag.Remove((Armor)deleteThisItem);
                        }
                        else if (deleteThisItem is Shield)
                        {
                            Hero.ShieldBag.Remove((Shield)deleteThisItem);
                        }
                    }
                    else
                    {
                        Hero.HealthPotionBag.Remove((HealthPotion)deleteThisItem);
                    }

                    // Make sold item available in the shop again
                    IBuyableItem shopItem = AllBuyableItems.Where(item => item.ItemId == foundId).FirstOrDefault();
                    shopItem.Sold = false;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"You just sold your {deleteThisItem.Name} for {deleteThisItem.SellingPrice} Gold Coins");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Did not sell an item because of one of the following statements:");
                    Console.WriteLine(" - didn't input a number");
                    Console.WriteLine(" - inputted number wasn't within a valid range");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have No Items To Sell");
                Console.ResetColor();
            }
        }
    }
}
