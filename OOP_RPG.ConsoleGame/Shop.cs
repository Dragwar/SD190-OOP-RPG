using OOP_RPG.Models.Interfaces;
using OOP_RPG.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.ConsoleGame
{
    public class Shop : IShop
    {
        private readonly IConsole _console;
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
        private IHero Hero { get; }

        public Shop(IConsole console, IHero hero)
        {
            _console = console;
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
        public void DisplayAllItems()
        {
            for (var i = 1; i < AllBuyableItems.Count + 1; i++)
            {
                if (AllBuyableItems[i - 1] is IStrengthItem)
                {
                    _console.ForegroundColor = ConsoleColor.Cyan;
                    _console.WriteLine(AllBuyableItems[i - 1].ItemStatsAsString(i));
                }
                else if (AllBuyableItems[i - 1] is IDefenseItem)
                {
                    _console.ForegroundColor = AllBuyableItems[i - 1] is IArmor ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
                    _console.WriteLine(AllBuyableItems[i - 1].ItemStatsAsString(i));
                }
                else
                {
                    _console.ForegroundColor = ConsoleColor.DarkGreen;
                    _console.WriteLine(AllBuyableItems[i - 1].ItemStatsAsString(i));
                }
                _console.ResetColor();
            }
        }



        /*
        ======================================================================================== 
        BuyShopItem ---> Allows hero to buy the shops items
        ======================================================================================== 
        */
        private void SellShopItem()
        {
            _console.Clear();

            if (AllBuyableItems.Any())
            {
                _console.WriteLine("================[All Items]================");
                DisplayAllItems();

                _console.WriteLine("Tip: input item number to buy it");
                var isNumber = int.TryParse(_console.ReadLine().Trim(), out var inputtedIndex);


                IBuyableItem selectedItem = isNumber && inputtedIndex > 0 && inputtedIndex <= AllBuyableItems.Count ? AllBuyableItems[inputtedIndex - 1] : null;


                if (isNumber && selectedItem != null && selectedItem.Price.BuyingPrice <= Hero.GoldCoins)
                {
                    SellItem(selectedItem);

                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine($"You just bought a new {selectedItem.Name}\n\n");
                    _console.ResetColor();
                }
                else
                {
                    _console.ForegroundColor = ConsoleColor.Red;
                    _console.WriteLine("Nothing Was Bought . . .");
                    _console.WriteLine("Because one of the following reasons:");
                    _console.WriteLine("- item's price was greater than your current Gold Coins");
                    _console.WriteLine("- input wasn't an item number\n");
                    _console.ResetColor();
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("Sorry The Shop Is Out Of Stock . . .");
                _console.ResetColor();
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
                _console.Title = $"Spend Your Gold Coins | Current Gold Coins: {Hero.GoldCoins}";

                _console.ForegroundColor = ConsoleColor.Yellow;
                _console.WriteLine("***** Shop ******\n");
                _console.ResetColor();

                _console.WriteLine("1. Buy An Item");
                _console.WriteLine("2. Sell An Item");
                _console.WriteLine("3. Exit");

                userInput = _console.ReadLine().Trim();


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
            _console.Clear();

            var heroItems = Hero.Bag.ToList();

            if (heroItems.Any())
            {
                for (var i = 0; i < heroItems.Count; i++)
                {
                    if (heroItems[i] is IBuyableItem buyableItem)
                    {
                        if (heroItems[i] is IStrengthItem)
                        {
                            _console.ForegroundColor = ConsoleColor.Cyan;
                            if (heroItems[i] is IWeapon weapon)
                            {
                                if (weapon.IsEquipped)
                                {
                                    _console.WriteLine($"{i + 1}. Sell your {weapon.Name} for {weapon.Price.SellingPrice} Gold Coins (Currently Equipped)");
                                }
                                else
                                {
                                    _console.WriteLine($"{i + 1}. Sell your {weapon.Name} for {weapon.Price.SellingPrice} Gold Coins");
                                }
                            }
                            else
                            {
                                _console.WriteLine($"{i + 1}. Sell your {buyableItem.Name} for {buyableItem.Price.SellingPrice} Gold Coins");
                            }
                        }
                        else if (heroItems[i] is IDefenseItem)
                        {
                            if (heroItems[i] is IArmor armor)
                            {
                                _console.ForegroundColor = ConsoleColor.DarkBlue;
                                if (armor.IsEquipped)
                                {
                                    _console.WriteLine($"{i + 1}. Sell your {armor.Name} for {armor.Price.SellingPrice} Gold Coins (Currently Equipped)");
                                }
                                else
                                {
                                    _console.WriteLine($"{i + 1}. Sell your {armor.Name} for {armor.Price.SellingPrice} Gold Coins");
                                }
                            }
                            else if (heroItems[i] is IShield shield)
                            {
                                _console.ForegroundColor = ConsoleColor.Blue;
                                if (shield.IsEquipped)
                                {
                                    _console.WriteLine($"{i + 1}. Sell your {shield.Name} for {shield.Price.SellingPrice} Gold Coins (Currently Equipped)");
                                }
                                else
                                {
                                    _console.WriteLine($"{i + 1}. Sell your {shield.Name} for {shield.Price.SellingPrice} Gold Coins");
                                }
                            }
                            else
                            {
                                _console.WriteLine($"{i + 1}. Sell your {buyableItem.Name} for {buyableItem.Price.SellingPrice} Gold Coins");
                            }
                        }
                        else if (heroItems[i] is IHealthPotion healthPotion)
                        {
                            _console.ForegroundColor = ConsoleColor.Green;
                            _console.WriteLine($"{i + 1}. Sell your {healthPotion.Name} for {healthPotion.Price.SellingPrice} Gold Coins");
                        }
                        else
                        {
                            throw new Exception($"Unexpected Item type ({heroItems[i]})");
                        }
                    }
                    _console.ResetColor();
                }

                _console.WriteLine("\nPlease input a item number to sell it\n");

                var isNumber = int.TryParse(_console.ReadLine().Trim(), out var userIndex);

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

                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine($"You just sold your {deleteThisItem.Name} for {deleteThisItem.Price.SellingPrice} Gold Coins");
                    _console.ResetColor();
                }
                else
                {
                    _console.ForegroundColor = ConsoleColor.Red;
                    _console.WriteLine("Did not sell an item because of one of the following statements:");
                    _console.WriteLine(" - didn't input a number");
                    _console.WriteLine(" - inputted number wasn't within a valid range");
                    _console.ResetColor();
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("You Have No Items To Sell");
                _console.ResetColor();
            }
        }
    }
}
