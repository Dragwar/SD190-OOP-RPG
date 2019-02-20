using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; }



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
                Console.WriteLine("3. View Today's Monsters");
                Console.WriteLine("4. Spend Experience Points");
                Console.WriteLine("5. Fight Monster");
                Console.WriteLine("6. Exit");

                input = Console.ReadLine();

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
                    ShowTodaysMonsters();
                }
                else if (input == "4")
                {
                    SpendExperiencePoints();
                }
                else if (input == "5")
                {
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

            List<string> difficulties = new List<string>() { "Easy", "Medium", "Hard" };

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
                Console.WriteLine($"{monster.Name} - Difficulty: {difficulties[monster.Difficulty]}");
            }
            Console.ResetColor();

            Console.WriteLine("\nPress any key to return to main menu.");
            Console.ReadKey(false);
            Console.Title = $"Main Menu";
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
            Console.Clear();

            Fight fight = new Fight(Hero);

            fight.Start();
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

        private int LevelUpHero(int heroPropValue)
        {
            bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int levelAmount);

            if (isNumber && levelAmount <= Hero.ExperiencePoints)
            {
                heroPropValue += levelAmount;
                Hero.RemoveExperiencePoints(levelAmount);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nothing Leveled Up (input wasn't a int or input was greater than current exp)\n");
                Console.ResetColor();
            }
            return heroPropValue;
        }
    }
}