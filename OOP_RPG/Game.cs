using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; }
        private Shop MyShop { get; }

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
        }// End of the Start Method



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
                    Monster.ShowTodaysMonsters();

                    LoadingSymbol loadingSymbol = new LoadingSymbol("Searching For Monsters", "You Encountered:");
                    loadingSymbol.Excute(new Random().Next(2, 7));

                    FightMonster();
                }

                if (Hero.CurrentHP <= 0)
                {
                    return;
                }
            }
        }// End of the Main Method



        /*
        ======================================================================================== 
        Stats ---> Displays the Hero's stats
        ======================================================================================== 
        */
        private void Stats()
        {
            Console.Clear();

            Console.Title = $"{Hero.Name}'s Stats: [> Str: {Hero.Strength} | Def: {Hero.Defense} | HP: {Hero.CurrentHP}/{Hero.OriginalHP} <]";
            Hero.ShowStats();

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);

            Console.Title = $"Main Menu";
        }// End of the Stats Method



        /*
        ======================================================================================== 
        Inventory ---> Displays the Hero's Inventory and Equip Items from Inventory
        ======================================================================================== 
        */
        private void Inventory()
        {
            Console.Clear();

            Hero.ManageInventory();

            Console.Title = $"Main Menu";
        }// End of the Inventory Method



        /*
        ======================================================================================== 
        FightMonster ---> Initializes the fight with the passed in Hero
        ======================================================================================== 
        */
        private void FightMonster()
        {
            Fight newFight = new Fight(Hero);

            newFight.Start();
        }// End of the FightMonster Method



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
                Console.Title = $"Spend Your EXP | Current Experience Points: {Hero.ExperiencePoints} | Stats: [> Str: {Hero.Strength} | Def: {Hero.Defense} | HP: {Hero.CurrentHP}/{Hero.OriginalHP} <]";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"**** Manage Experience Points ****");
                Console.ResetColor();

                Console.WriteLine("1. Level Up Strength");
                Console.WriteLine("2. Level Up Defense");
                Console.WriteLine("3. Level Up Original HP");
                Console.WriteLine("4. Return To Main Menu\n");

                userInput = Console.ReadLine().Trim();

                Hero.SpendExperiencePoints(userInput);

                Console.Title = $"Main Menu";
            }// End of the SpendExperiencePoints Method
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
        }// End of the Shop Method


    }
}
