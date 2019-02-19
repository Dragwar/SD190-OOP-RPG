using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; }

        public Game()
        {
            Hero = new Hero();
        }

        public void Start()
        {
            Console.Title = $"Welcome!!!";

            Console.WriteLine("Welcome hero!");
            Console.WriteLine("Please enter your name:");

            Hero.Name = Console.ReadLine();

            Console.WriteLine($"\nHello {Hero.Name}");

            Main();
        }

        private void Main()
        {
            Console.Title = $"Main Menu";

            var input = "0";

            while (input != "4")
            {
                Console.WriteLine("\n==============================================");
                Console.WriteLine("\t\tMain Menu");
                Console.WriteLine("==============================================\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please choose an option by entering a number.");
                Console.ResetColor();

                Console.WriteLine("1. View Stats");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Fight Monster");
                Console.WriteLine("4. Exit");

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
                    Fight();
                }

                if (Hero.CurrentHP <= 0)
                {
                    return;
                }
            }
        }

        private void Stats()
        {
            Console.Clear();

            Console.Title = $"{Hero.Name}'s Stats";
            Hero.ShowStats();

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey();
            Console.Title = $"Main Menu";
        }

        private void Inventory()
        {
            Console.Clear();

            Console.Title = $"{Hero.Name}'s Inventory";
            Hero.ShowInventory();

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey();

            Console.Title = $"Main Menu";
        }

        private void Fight()
        {
            Console.Clear();

            var fight = new Fight(Hero);

            fight.Start();
        }
    }
}