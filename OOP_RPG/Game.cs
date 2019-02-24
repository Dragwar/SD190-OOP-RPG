using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; }
        private Shop MyShop { get; }


        /*
        ======================================================================================== 
        Game ---> Instantiates the Hero property when a new game gets created (see Program.cs)
        ======================================================================================== 
        */
        public Game()
        {
            Hero = new Hero(); // only one hero per game
            MyShop = new Shop(Hero); // only one shop per game
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
                Console.WriteLine("2. View and Manage Inventory");
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
            Console.ReadKey(true);

            Console.Title = $"Main Menu";
        }



        /*
        ======================================================================================== 
        Stats ---> Displays the Hero's Inventory and allows Hero to Equip Items from Inventory
        ======================================================================================== 
        */
        private void Inventory()
        {
            Console.Clear();


            string userInput = "0";

            while (userInput != "3")
            {
                Console.Title = $"{Hero.Name}'s Inventory";
                Hero.ShowInventory();


                Console.WriteLine("1. equip a weapon.");
                Console.WriteLine("2. equip armor.");
                Console.WriteLine("3. exit\n");

                userInput = Console.ReadLine().Trim();

                if (userInput == "1")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******* Unequipped Weapons *******");
                    Console.ResetColor();

                    List<Weapon> weapons = Hero.WeaponsBag.ToList();
                    if (weapons.Any())
                    {
                        for (int i = 1; i < weapons.Count + 1; i++)
                        {
                            if (weapons[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength}) Strength (Already Equipped)");
                                Console.ResetColor();

                            }
                            else
                            {
                                Console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength}) Strength");
                            }
                        }

                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || (userIndex < 0 || userIndex >= weapons.Count))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nothing was equipped because of one of the following errors:");
                            Console.WriteLine("- did not input a number");
                            Console.WriteLine("- inputted number was too small");
                            Console.WriteLine("- inputted number was too big");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            Hero.EquipWeapon(userIndex);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"You equipped your {Hero.EquippedWeapon.Name}!");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have nothing to equip. . .");
                        Console.ResetColor();
                    }
                }
                else if (userInput == "2")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******* Unequipped Armor *******");
                    Console.ResetColor();

                    List<Armor> armor = Hero.ArmorsBag.ToList();

                    if (armor.Any())
                    {
                        for (int i = 1; i < armor.Count + 1; i++)
                        {
                            if (armor[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {armor[i - 1].Name} --> (+ {armor[i - 1].Defense}) Defense (Already Equipped)");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"{i}. {armor[i - 1].Name} --> (+ {armor[i - 1].Defense}) Defense");
                            }
                        }

                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || (userIndex < 0 || userIndex >= armor.Count))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nothing was equipped because of one of the following errors:");
                            Console.WriteLine("- did not input a number");
                            Console.WriteLine("- inputted number was too small");
                            Console.WriteLine("- inputted number was too big");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            Hero.EquipArmor(userIndex);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"You equipped your {Hero.EquippedArmor.Name}!");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have nothing to equip. . .");
                        Console.ResetColor();
                    }
                }
            }

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

            bool isAnyStockLeft = MyShop.CheckIfThereIsAnyStock();

            if (isAnyStockLeft)
            {
                MyShop.OpenShopAndTakeUserOrder();
            }

            Console.Title = $"Main Menu";
        }// End of Shop Method


    }
}
