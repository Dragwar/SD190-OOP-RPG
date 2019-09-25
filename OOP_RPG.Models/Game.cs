using OOP_RPG.ConsoleGame;
using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Interfaces;
using System;

namespace OOP_RPG.Models
{
    public class Game
    {
        private readonly IConsole _console;
        public IShop MyShop { get; }
        public IHero Hero { get; }
        public IAchievementManager AchievementManager { get; }

        public Game(IConsole console, IHero hero, IShop shop, IAchievementManager achievementManager)
        {
            _console = console;
            AchievementManager = achievementManager; // only one HandleAchievements per game
            Hero = hero; // only one hero per game
            MyShop = shop; // only one shop per game
        }

        /*
        ======================================================================================== 
        ShowTodaysMonsters ---> Displays today's monsters (color coded, darker == harder)
        ======================================================================================== 
        */
        public void ShowTodaysMonsters()
        {
            _console.Clear();

            _console.Title = $"Today's Monsters";

            _console.ForegroundColor = ConsoleColor.Yellow;
            _console.WriteLine("***** Today's Monsters ******\n");

            foreach (var monster in Fight.GetTodaysMonsters())
            {
                switch (monster.Difficulty)
                {
                    case Difficulty.Easy:
                        _console.ForegroundColor = ConsoleColor.Cyan;
                        break;

                    case Difficulty.Medium:
                        _console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;

                    default:
                        _console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                }
                _console.WriteLine($"{monster.Name} - Difficulty: {monster.Difficulty}");
            }
            _console.ResetColor();
            _console.WriteLine();
        }


        /*
        ======================================================================================== 
        Start ---> Gets the Hero's name from user then calls Main()
        ======================================================================================== 
        */
        public void Start()
        {
            _console.Title = $"Welcome!!!";

            _console.WriteLine("Welcome hero!");
            _console.WriteLine("Please enter your name:");

            Hero.Name = _console.ReadLine().Trim();

            while (string.IsNullOrEmpty(Hero.Name) || string.IsNullOrWhiteSpace(Hero.Name))
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("Every Hero Has A Name . . .");
                _console.ResetColor();

                _console.WriteLine("Please enter your name:");
                Hero.Name = _console.ReadLine().Trim();
            }

            CheatCodeManager.HeroNameCheat(Hero, MyShop);

            _console.WriteLine($"\nHello {Hero.Name}");
            Main();
        }



        /*
        ======================================================================================== 
        Main ---> Main Menu for this game
        ======================================================================================== 
        */
        private void Main()
        {
            _console.Title = $"Main Menu";

            var input = "0";

            while (input != "6")
            {
                _console.WriteLine("\n==============================================");
                _console.WriteLine("\t\tMain Menu");
                _console.WriteLine("==============================================\n");

                _console.ForegroundColor = ConsoleColor.Yellow;
                _console.WriteLine("Please choose an option by entering a number.");
                _console.ResetColor();

                _console.WriteLine("1. View Stats");
                _console.WriteLine("2. View and Manage Inventory");
                _console.WriteLine("3. View Shop");
                _console.WriteLine("4. Spend Experience Points");
                _console.WriteLine("5. Fight Monster");
                _console.WriteLine("6. Exit");

                input = _console.ReadLine().Trim();

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

                    var loadingSymbol = new LoadingSymbol("Searching For Monsters", "You Encountered:");
                    loadingSymbol.Excute(RNG.Next(2, 7));

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
        Stats ---> Displays the Hero's stats
        ======================================================================================== 
        */
        private void Stats()
        {
            _console.Clear();

            _console.Title = $"{Hero.Name}'s Stats: [> Str: {Hero.Strength} | Def: {Hero.Defense} | HP: {Hero.CurrentHP}/{Hero.OriginalHP} <]";
            Hero.ShowStats(true);

            _console.WriteLine("Press any key to return to main menu.");
            _console.ReadKey(true);
        }



        /*
        ======================================================================================== 
        Inventory ---> Displays the Hero's Inventory and Equip Items from Inventory
        ======================================================================================== 
        */
        private void Inventory()
        {
            _console.Clear();

            Hero.ManageInventory();
        }



        /*
        ======================================================================================== 
        FightMonster ---> Initializes the fight with the passed in Hero
        ======================================================================================== 
        */
        private void FightMonster()
        {
            var newFight = new Fight(_console, AchievementManager, Hero);

            newFight.Start();
        }



        /*
        ======================================================================================== 
        SpendExperiencePoints ---> Allows Player To Level Up Their Character's Stats
        ======================================================================================== 
        */
        public void SpendExperiencePoints()
        {
            _console.Clear();

            var userInput = "";

            while (userInput != "4")
            {
                _console.Title = $"Spend Your EXP | Current Experience Points: {Hero.ExperiencePoints} | Stats: [> Str: {Hero.Strength} | Def: {Hero.Defense} | HP: {Hero.CurrentHP}/{Hero.OriginalHP} <]";

                _console.ForegroundColor = ConsoleColor.Yellow;
                _console.WriteLine($"**** Manage Experience Points ****");
                _console.ResetColor();

                _console.WriteLine("1. Level Up Strength");
                _console.WriteLine("2. Level Up Defense");
                _console.WriteLine("3. Level Up Original HP");
                _console.WriteLine("4. Return To Main Menu\n");

                userInput = _console.ReadLine().Trim();

                Hero.SpendExperiencePoints(userInput);
            }
        }


        /*
        ======================================================================================== 
        Shop ---> Where the Hero can spend his/her gold to buy weapons/armor
        ======================================================================================== 
        */
        public void Shop()
        {
            _console.Clear();

            MyShop.OpenShopAndTakeUserOrder();
        }
    }
}
