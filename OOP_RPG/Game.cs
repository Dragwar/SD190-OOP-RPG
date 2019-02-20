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

            while (input != "5")
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
                Console.WriteLine("4. Fight Monster");
                Console.WriteLine("5. Exit");

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
    }
}