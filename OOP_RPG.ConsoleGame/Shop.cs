using OOP_RPG.Models.Interfaces;
using OOP_RPG.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.ConsoleGame
{
    public class Shop
    {
        private IReadOnlyList<IBuyableItem> ALLSHOPITEMS => new List<IBuyableItem>()
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

        public List<IBuyableItem> AllBuyableItems { get; }
        private Hero Hero { get; }

        public Shop(Hero hero)
        {
            Hero = hero;
            AllBuyableItems = ALLSHOPITEMS.ToList();
        }



        /*
        ======================================================================================== 
        SellItem ---> Allows transfers the sold item to the hero
        ======================================================================================== 
        */
        private void SellItem(IBuyableItem item)
        {
            Hero.RemoveGoldCoins(item.Price.BuyingPrice);

            //TODO: skip checks? allow any type as long item implements IBuyableItem?
            if (item is IWeapon weapon)
            {
                Hero.Bag.Add(weapon);
            }
            else if (item is IArmor armor)
            {
                Hero.Bag.Add(armor);
            }
            else if (item is IShield shield)
            {
                Hero.Bag.Add(shield);
            }
            else if (item is IHealthPotion healthPotion)
            {
                Hero.Bag.Add(healthPotion);
            }
            else
            {
                throw new Exception("Unexpected item type/category");
            }

            AllBuyableItems.Remove(item);
        }


        public List<IWeapon> GetCurrentWeapons() => AllBuyableItems
                .Where(item => item is IWeapon weapon && !(Hero.IsItemInBag(weapon) || Hero.IsItemEquipped(weapon)))
                .Cast<IWeapon>()
                .ToList();

        public List<IArmor> GetCurrentArmor() => AllBuyableItems
                .Where(item => item is IArmor armor && !(Hero.IsItemInBag(armor) || Hero.IsItemEquipped(armor)))
                .Cast<IArmor>()
                .ToList();

        public List<IShield> GetCurrentShields() => AllBuyableItems
                .Where(item => item is IShield shield && !(Hero.IsItemInBag(shield) || Hero.IsItemEquipped(shield)))
                .Cast<IShield>()
                .ToList();


        /*
        ======================================================================================== 
        DisplayAllItems ---> Prints all shop items and color codes items by their category 
        ======================================================================================== 
        */
        private void DisplayAllItems()
        {
            for (var i = 1; i < AllBuyableItems.Count + 1; i++)
            {
                if (AllBuyableItems[i - 1] is IStrengthItem)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(AllBuyableItems[i - 1].ItemStatsAsString(i));
                }
                else if (AllBuyableItems[i - 1] is IDefenseItem)
                {
                    Console.ForegroundColor = AllBuyableItems[i - 1] is IArmor ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
                    Console.WriteLine(AllBuyableItems[i - 1].ItemStatsAsString(i));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(AllBuyableItems[i - 1].ItemStatsAsString(i));
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

            if (AllBuyableItems.Any())
            {
                Console.WriteLine("================[All Items]================");
                DisplayAllItems();

                Console.WriteLine("Tip: input item number to buy it");
                var isNumber = int.TryParse(Console.ReadLine().Trim(), out var inputtedIndex);


                IBuyableItem selectedItem = isNumber && inputtedIndex > 0 && inputtedIndex <= AllBuyableItems.Count ? AllBuyableItems[inputtedIndex - 1] : null;


                if (isNumber && selectedItem != null && selectedItem.Price.BuyingPrice <= Hero.GoldCoins)
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
            }
        }



        /*
        ======================================================================================== 
        BuyHeroItem ---> Allows hero to sell his/her items to the shop
        ======================================================================================== 
        */
        private void BuyHeroItem()
        {
            Console.Clear();

            var heroItems = Hero.Bag.ToList();

            if (heroItems.Any())
            {
                for (var i = 0; i < heroItems.Count; i++)
                {
                    if (heroItems[i] is IBuyableItem buyableItem)
                    {
                        if (heroItems[i] is IStrengthItem)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            if (heroItems[i] is IWeapon weapon)
                            {
                                if (weapon.IsEquipped)
                                {
                                    Console.WriteLine($"{i + 1}. Sell your {weapon.Name} for {weapon.Price.SellingPrice} Gold Coins (Currently Equipped)");
                                }
                                else
                                {
                                    Console.WriteLine($"{i + 1}. Sell your {weapon.Name} for {weapon.Price.SellingPrice} Gold Coins");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. Sell your {buyableItem.Name} for {buyableItem.Price.SellingPrice} Gold Coins");
                            }
                        }
                        else if (heroItems[i] is IDefenseItem)
                        {
                            if (heroItems[i] is IArmor armor)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                if (armor.IsEquipped)
                                {
                                    Console.WriteLine($"{i + 1}. Sell your {armor.Name} for {armor.Price.SellingPrice} Gold Coins (Currently Equipped)");
                                }
                                else
                                {
                                    Console.WriteLine($"{i + 1}. Sell your {armor.Name} for {armor.Price.SellingPrice} Gold Coins");
                                }
                            }
                            else if (heroItems[i] is IShield shield)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                if (shield.IsEquipped)
                                {
                                    Console.WriteLine($"{i + 1}. Sell your {shield.Name} for {shield.Price.SellingPrice} Gold Coins (Currently Equipped)");
                                }
                                else
                                {
                                    Console.WriteLine($"{i + 1}. Sell your {shield.Name} for {shield.Price.SellingPrice} Gold Coins");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. Sell your {buyableItem.Name} for {buyableItem.Price.SellingPrice} Gold Coins");
                            }
                        }
                        else if (heroItems[i] is IHealthPotion healthPotion)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{i + 1}. Sell your {healthPotion.Name} for {healthPotion.Price.SellingPrice} Gold Coins");
                        }
                        else
                        {
                            throw new Exception($"Unexpected Item type ({heroItems[i]})");
                        }
                    }
                    Console.ResetColor();
                }

                Console.WriteLine("\nPlease input a item number to sell it\n");

                var isNumber = int.TryParse(Console.ReadLine().Trim(), out var userIndex);

                userIndex--;

                var invalidIndex = userIndex > heroItems.Count || userIndex < 0;

                if (isNumber && !invalidIndex)
                {
                    var deleteThisItem = Hero.Bag[userIndex] as IBuyableItem;


                    if (deleteThisItem is IEquippableItem equippableItem && Hero.IsItemEquipped(equippableItem))
                    {
                        Hero.UnEquipItem(equippableItem);
                    }
                    else if (Hero.IsItemInBag(deleteThisItem))
                    {
                        Hero.Bag.Remove(deleteThisItem);
                    }

                    Hero.AddGoldCoins(deleteThisItem.Price.SellingPrice);


                    // Make sold item available in the shop again
                    AllBuyableItems.Add(deleteThisItem);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"You just sold your {deleteThisItem.Name} for {deleteThisItem.Price.SellingPrice} Gold Coins");
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
