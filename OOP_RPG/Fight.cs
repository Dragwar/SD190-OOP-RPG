using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public enum Difficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }

    public enum DaysOfTheWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednsday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
    }

    public class Fight
    {
        private List<Monster> Monsters { get; }
        private Hero Hero { get; }
        private Monster CurrentMonster { get; }
        private int MonstersEXPWorth { get; }

        /*
        ======================================================================================== 
        Fight ---> Initializes the fight and selects a random monster from today's monsters
        ======================================================================================== 
        */
        public Fight(Hero game)
        {
            Hero = game;
            Monsters = new List<Monster>(GetTodaysMonsters());
            Random rand = new Random();
            CurrentMonster = Monsters[rand.Next(0, Monsters.Count)];

            switch (CurrentMonster.Difficulty)
            {
                case (int)Difficulty.Hard:
                    MonstersEXPWorth = rand.Next(8, 18);
                    break;
                case (int)Difficulty.Medium:
                    MonstersEXPWorth = rand.Next(4, 12);
                    break;

                default:
                    MonstersEXPWorth = rand.Next(1, 4);
                    break;
            }
        }



        /*
        ======================================================================================== 
        GetTodaysMonsters ---> Gets all the monsters and only returns today's monsters
        ======================================================================================== 
        */
        public static List<Monster> GetTodaysMonsters()
        {
            List<Monster> todaysMonsters;

            int currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;

            DayOfTheWeekMonsters myMonsters = new DayOfTheWeekMonsters();
            List<List<Monster>> allMonstersLists = myMonsters.GetAllMonsters();

            switch (currentDayOfTheWeek)
            {
                case (int)DayOfWeek.Monday:
                    todaysMonsters = allMonstersLists[(int)DayOfWeek.Monday];
                    break;

                case (int)DayOfWeek.Tuesday:
                    todaysMonsters = allMonstersLists[(int)DayOfWeek.Tuesday];
                    break;

                case (int)DayOfWeek.Wednesday:
                    todaysMonsters = allMonstersLists[(int)DayOfWeek.Wednesday];
                    break;

                case (int)DayOfWeek.Thursday:
                    todaysMonsters = allMonstersLists[(int)DayOfWeek.Thursday];
                    break;

                case (int)DayOfWeek.Friday:
                    todaysMonsters = allMonstersLists[(int)DayOfWeek.Friday];
                    break;

                case (int)DayOfWeek.Saturday:
                    todaysMonsters = allMonstersLists[(int)DayOfWeek.Saturday];
                    break;

                default:
                    todaysMonsters = allMonstersLists[(int)DayOfWeek.Sunday];
                    break;
            }
            return todaysMonsters;
        }



        /*
        ======================================================================================== 
        Start ---> Fight menu (choose to fight, see stats, or maybe more options in the future)
        ======================================================================================== 
        */
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nYou've encountered a {CurrentMonster.Name}! (Strength = {CurrentMonster.Strength} | Defense = {CurrentMonster.Defense} | HP = {CurrentMonster.CurrentHP})");
            Console.ResetColor();

            while (CurrentMonster.CurrentHP > 0 && Hero.CurrentHP > 0)
            {
                Console.Title = $"FIGHT!!! ({Hero.Name} vs {CurrentMonster.Name}) Your Current HP: {Hero.CurrentHP} | Enemy Current HP: {CurrentMonster.CurrentHP}";
                Console.WriteLine($"\nWhat will you do?");
                Console.WriteLine("1. See The Enemy's Status and Your Status");
                Console.WriteLine("2. Fight");

                var input = Console.ReadLine();

                if (input == "1")
                {
                    Hero.ShowStats();
                    CurrentMonster.ShowStats();
                }
                else if (input == "2")
                {
                    HeroTurn();
                }
            }
        }



        /*
        ======================================================================================== 
        HeroTurn ---> Calculate the damage that will be dealt to the currentMonster
        ======================================================================================== 
        */
        private void HeroTurn()
        {
            var compare = Hero.Strength - CurrentMonster.Defense;
            int damage;

            if (compare <= 0)
            {
                damage = 1;
                CurrentMonster.CurrentHP -= damage;
            }
            else
            {
                damage = compare;
                CurrentMonster.CurrentHP -= damage;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nYou did {damage} damage!");
            Console.WriteLine($"Monster's HP: {CurrentMonster.CurrentHP}/{CurrentMonster.OriginalHP}");
            Console.ResetColor();

            if (CurrentMonster.CurrentHP <= 0)
            {
                Win();
            }
            else
            {
                MonsterTurn();
            }
        }



        /*
        ======================================================================================== 
        MonsterTurn ---> Calculate the damage dealt to the Hero
        ======================================================================================== 
        */
        private void MonsterTurn()
        {
            int damage;
            var compare = CurrentMonster.Strength - Hero.Defense;

            if (compare <= 0)
            {
                damage = 1;
                Hero.CurrentHP -= damage;
            }
            else
            {
                damage = compare;
                Hero.CurrentHP -= damage;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{CurrentMonster.Name} does {damage} damage!");
            Console.WriteLine($"{Hero.Name}'s HP: {Hero.CurrentHP}/{Hero.OriginalHP}");
            Console.ResetColor();

            if (Hero.CurrentHP <= 0)
            {
                Lose();
            }
        }



        /*
        ======================================================================================== 
        Win ---> Win Message and returns to the Main Menu
        ======================================================================================== 
        */
        private void Win()
        {
            Hero.AddExperiencePoints(MonstersEXPWorth);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{CurrentMonster.Name} has been defeated! (+ {MonstersEXPWorth} EXP) You win the battle!");
            Console.ResetColor();

            Hero.ShowStats();

            Console.Title = $"Main Menu";
        }



        /*
        ======================================================================================== 
        Lose ---> Lose Message and exits the game
        ======================================================================================== 
        */
        private void Lose()
        {
            Console.Title = $"Better Luck Next Time.";

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("You've been defeated! :( GAME OVER.");
            Console.ResetColor();

            Console.WriteLine("Press any key to exit the game");
            Console.ReadKey();
        }
    }
}