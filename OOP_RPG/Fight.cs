using System;
using System.Collections.Generic;
using System.Threading;

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


        /*
        ======================================================================================== 
        Fight
        ======================================================================================== 
        */
        public Fight(Hero game)
        {
            Hero = game;
            Monsters = new List<Monster>(GetTodaysMonsters());

            CurrentMonster = Monsters[new Random().Next(0, (Monsters.Count - 1))];
        }


        /*
        ======================================================================================== 
        GetTodaysMonsters
        ======================================================================================== 
        */
        private List<Monster> GetTodaysMonsters()
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
        AddMonster (+1 overloads)  NOT IN USE
        ======================================================================================== 
        */
        // NOT IN USE
        /*
        private void AddMonster(int difficulty, string name, int strength, int defense, int hp)
        {
            var monster = new Monster(difficulty, name, strength, defense, hp, hp);

            Monsters.Add(monster);
        }

        private void AddMonster(Monster monster)
        {
            Monsters.Add(monster);
        }
        */


        /*
        ======================================================================================== 
        Start
        ======================================================================================== 
        */
        public void Start()
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nYou've encountered a {CurrentMonster.Name}! (Strength = {CurrentMonster.Strength} | Defense = {CurrentMonster.Defense} | HP = {CurrentMonster.CurrentHP})");
            Console.ResetColor();

            DayOfTheWeekMonsters a = new DayOfTheWeekMonsters();

            while (CurrentMonster.CurrentHP > 0 && Hero.CurrentHP > 0)
            {
                Console.Title = $"FIGHT!!! ({Hero.Name} vs {CurrentMonster.Name}) --> Your Current HP: {Hero.CurrentHP} | Enemy Current HP: {CurrentMonster.CurrentHP}";
                Console.WriteLine($"\nWhat will you do?");
                Console.WriteLine("1. Fight");
                Console.WriteLine("2. See The Enemy's Status and Your Status");

                var input = Console.ReadLine();

                if (input == "1")
                {
                    HeroTurn();
                }
                else if (input == "2")
                {
                    Hero.ShowStats();
                    CurrentMonster.ShowStats();
                }
            }
        }


        /*
        ======================================================================================== 
        HeroTurn
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
        MonsterTurn
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
            Console.ResetColor();

            if (Hero.CurrentHP <= 0)
            {
                Lose();
            }
        }

        /*
        ======================================================================================== 
        Win
        ======================================================================================== 
        */
        private void Win()
        {
            Console.Title = $"VICTORY!!!";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{CurrentMonster.Name} has been defeated! You win the battle!");
            Console.ResetColor();


            Hero.ShowStats();

            Thread.Sleep(1000);
            Console.Title = $"Main Menu";
        }


        /*
        ======================================================================================== 
        Lose
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