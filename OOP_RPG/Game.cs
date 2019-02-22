using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; }
        private Shop MyShop = new Shop();


        /*
        ======================================================================================== 
        Game ---> Instantiates the Hero property when a new game gets created (see Program.cs)
        ======================================================================================== 
        */
        public Game()
        {
            Hero = new Hero();
        }



        /*
        ======================================================================================== 
        Start ---> Gets the Hero's name from user then calls Main()
        ======================================================================================== 
        */
        public void Start()
        {
            Console.Title = $"Welcome!!!";

            Console.WriteLine("Welcome hero!");
            Console.WriteLine("Please enter your name:");

            Hero.Name = Console.ReadLine().Trim();

            while (string.IsNullOrEmpty(Hero.Name) || string.IsNullOrWhiteSpace(Hero.Name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Every Hero Has A Name . . .");
                Console.ResetColor();

                Console.WriteLine("Please enter your name:");
                Hero.Name = Console.ReadLine().Trim();
            }


            Console.WriteLine($"\nHello {Hero.Name}");

            Main();
        }



        /*
        ======================================================================================== 
        Main ---> Main Menu for this game
        ======================================================================================== 
        */
        private void Main()
        {
            Console.Title = $"Main Menu";

            string input = "0";

            while (input != "6")
            {
                Console.WriteLine("\n==============================================");
                Console.WriteLine("\t\tMain Menu");
                Console.WriteLine("==============================================\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please choose an option by entering a number.");
                Console.ResetColor();

                Console.WriteLine("1. View Stats");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. View Shop");
                Console.WriteLine("4. Spend Experience Points");
                Console.WriteLine("5. Fight Monster");
                Console.WriteLine("6. Exit");

                input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    Stats();
                }
                else if (input == "2")
                {
                    Inventory();
                }
                else if (input == "3")
                {
                    Shop();
                }
                else if (input == "4")
                {
                    SpendExperiencePoints();
                }
                else if (input == "5")
                {
                    ShowTodaysMonsters();

                    LoadingSymbol loadingSymbol = new LoadingSymbol("Searching For Monsters", "You Encountered:");
                    loadingSymbol.Excute(new Random().Next(1, 6));

                    FightMonster();
                }

                if (Hero.CurrentHP <= 0)
                {
                    return;
                }
            }
        }



        /*
        ======================================================================================== 
        ShowTodaysMonsters ---> Displays today's monsters (color coded, darker == harder)
        ======================================================================================== 
        */
        private void ShowTodaysMonsters()
        {
            Console.Clear();

            Console.Title = $"Today's Monsters";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***** Today's Monsters ******\n");

            foreach (Monster monster in Fight.GetTodaysMonsters())
            {
                switch (monster.Difficulty)
                {
                    case (int)Difficulty.Easy:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;

                    case (int)Difficulty.Medium:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                }
                Console.WriteLine($"{monster.Name} - Difficulty: {(Difficulty)monster.Difficulty}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }



        /*
        ======================================================================================== 
        Stats ---> Displays the Hero's stats
        ======================================================================================== 
        */
        private void Stats()
        {
            Console.Clear();

            Console.Title = $"{Hero.Name}'s Stats";
            Hero.ShowStats();

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(false);
            Console.Title = $"Main Menu";
        }



        /*
        ======================================================================================== 
        Stats ---> Displays the Hero's Inventory
        ======================================================================================== 
        */
        private void Inventory()
        {
            Console.Clear();

            Console.Title = $"{Hero.Name}'s Inventory";
            Hero.ShowInventory();

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(false);

            Console.Title = $"Main Menu";
        }



        /*
        ======================================================================================== 
        FightMonster ---> Initializes the fight with the passed in Hero
        ======================================================================================== 
        */
        private void FightMonster()
        {
            Fight newFight = new Fight(Hero);

            newFight.Start();
        }



        /*
        ======================================================================================== 
        SpendExperiencePoints ---> Allows Player To Level Up Their Character's Stats
        ======================================================================================== 
        */
        public void SpendExperiencePoints()
        {
            Console.Clear();

            string userInput = "";

            while (userInput != "4")
            {
                Console.Title = $"Spend Your EXP | Current Experience Points: {Hero.ExperiencePoints} | Stats: [> str: {Hero.Strength} | Def: {Hero.Defense} | HP: {Hero.CurrentHP}/{Hero.OriginalHP} <]";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"**** Manage Experience Points ****");
                Console.ResetColor();

                Console.WriteLine("1. Level Up Strength");
                Console.WriteLine("2. Level Up Defense");
                Console.WriteLine("3. Level Up Max HP");
                Console.WriteLine("4. Return To Main Menu\n");

                userInput = Console.ReadLine().Trim();


                if (userInput == "1")
                {
                    Console.WriteLine("================[Level Up Strength]================");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Current Experience Points: {Hero.ExperiencePoints}");
                    Console.WriteLine($"Current Strength: {Hero.Strength}");
                    Console.ResetColor();

                    Console.WriteLine("Level Up Strength by:\n");
                    Hero.Strength = LevelUpHero(Hero.Strength);
                }
                else if (userInput == "2")
                {
                    Console.WriteLine("================[Level Up Defense]================");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Current Experience Points: {Hero.ExperiencePoints}");
                    Console.WriteLine($"Current Defense: {Hero.Defense}");
                    Console.ResetColor();

                    Console.WriteLine("Level Up Defense by:\n");
                    Hero.Defense = LevelUpHero(Hero.Defense);
                }
                else if (userInput == "3")
                {
                    Console.WriteLine("================[Level Up OriginalHP]================");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Current Experience Points: {Hero.ExperiencePoints}");
                    Console.WriteLine($"Current OriginalHP: {Hero.OriginalHP}");
                    Console.ResetColor();

                    Console.WriteLine("Level Up OriginalHP by:\n");
                    Hero.OriginalHP = LevelUpHero(Hero.OriginalHP);
                }

                Hero.ShowStats();
                Console.WriteLine("\n");

            }

            Console.Title = $"Main Menu";
        }



        /*
        ======================================================================================== 
        LevelUpHero ---> Adds the inputed number to level up the passed in stat
        ======================================================================================== 
        */
        private int LevelUpHero(int heroStatValue)
        {
            bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int levelAmount);

            if (isNumber && levelAmount <= Hero.ExperiencePoints)
            {
                heroStatValue += levelAmount;
                Hero.RemoveExperiencePoints(levelAmount);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nothing Leveled Up (input wasn't a int or input was greater than current exp)\n");
                Console.ResetColor();
            }
            return heroStatValue;
        }



        /*
        ======================================================================================== 
        Shop ---> Where the Hero can spend his/her gold to buy weapons/armor
        ======================================================================================== 
        */
        public void Shop()
        {
            Console.Clear();

            if (MyShop.AllBuyableItems.All(item => item.Sold))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry The Entire Shop Is Out Of Stock. . .");
                Console.ResetColor();
                return;
            }

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
                    Console.Clear();

                    List<Weapon> shopWeapons = MyShop.GetCurrentWeapons();

                    if (shopWeapons.Any())
                    {
                        Console.WriteLine("================[Strength Items]================");
                        Console.WriteLine(MyShop.DisplayWeapons());

                        Console.WriteLine("Tip: input item number to buy it");
                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int inputtedIndex);


                        Weapon selectedWeapon = isNumber && inputtedIndex > 0 && inputtedIndex <= shopWeapons.Count ? shopWeapons[inputtedIndex - 1] : null;


                        if (isNumber && selectedWeapon != null && selectedWeapon.Price <= Hero.GoldCoins)
                        {
                            Hero.RemoveGoldCoins(selectedWeapon.Price);
                            MyShop.ToggleSoldProperty(selectedWeapon);
                            Hero.WeaponsBag.Add(selectedWeapon);

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
                            Console.WriteLine("- input wasn't an item number");
                            Console.WriteLine("- input was already sold\n");
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
                else if (userInput == "2")
                {
                    Console.Clear();

                    List<Armor> shopArmor = MyShop.GetCurrentArmor();

                    if (shopArmor.Any())
                    {
                        Console.WriteLine("================[Defense Items]================");
                        Console.WriteLine(MyShop.DisplayArmor());

                        Console.WriteLine("Tip: input item number to buy it");
                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int inputtedIndex);


                        Armor selectedArmor = isNumber && inputtedIndex > 0 && inputtedIndex <= shopArmor.Count ? shopArmor[inputtedIndex - 1] : null;


                        if (isNumber && selectedArmor != null && selectedArmor.Price <= Hero.GoldCoins)
                        {
                            Hero.RemoveGoldCoins(selectedArmor.Price);
                            MyShop.ToggleSoldProperty(selectedArmor);
                            Hero.ArmorsBag.Add(selectedArmor);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"You just bought a new {selectedArmor.Name}\n\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nothing Was Bought . . .");
                            Console.WriteLine("Because one of the following reasons:");
                            Console.WriteLine("- item's price was greater than your current Gold Coins");
                            Console.WriteLine("- input wasn't an item number");
                            Console.WriteLine("- input was already sold\n");
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
                else if (userInput == "3")
                {
                    Console.Clear();

                    List<IBuyableItem> buyableItems = MyShop.AllBuyableItems.Where(item => !item.Sold).ToList();

                    if (buyableItems.Any())
                    {
                        Console.WriteLine("================[All Items]================");
                        Console.WriteLine(MyShop.DisplayAllItems());

                        Console.WriteLine("Tip: input item number to buy it");
                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int inputtedIndex);


                        IBuyableItem selectedItem = isNumber && inputtedIndex > 0 && inputtedIndex <= buyableItems.Count ? buyableItems[inputtedIndex - 1] : null;


                        if (isNumber && selectedItem != null && selectedItem.Price <= Hero.GoldCoins)
                        {
                            Hero.RemoveGoldCoins(selectedItem.Price);

                            if (selectedItem.ItemCategory == ItemCategoryEnum.Strength)
                            {
                                MyShop.ToggleSoldProperty((Weapon)selectedItem);
                                Hero.WeaponsBag.Add((Weapon)selectedItem);
                            }
                            else
                            {
                                MyShop.ToggleSoldProperty((Armor)selectedItem);
                                Hero.ArmorsBag.Add((Armor)selectedItem);
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
                            Console.WriteLine("- input wasn't an item number");
                            Console.WriteLine("- input was already sold\n");
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
            } // End of While Loop

            Console.Title = $"Main Menu";
        }// End of Shop Method




    }
}
