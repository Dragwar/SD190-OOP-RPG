using OOP_RPG.ConsoleGame.Utilities;
using OOP_RPG.Models.Enumerations;
using System;

namespace OOP_RPG.ConsoleGame
{
    public class Monster
    {
        public Difficulty Difficulty { get; }
        public DayOfWeek DayOfTheWeek { get; }
        public string Name { get; }
        public int Strength { get; }
        public int Defense { get; }
        public int OriginalHP { get; }
        public int CurrentHP { get; set; }

        public Monster(Difficulty difficulty, DayOfWeek dayOfTheWeek, string name, int strength, int defense, int hp)
        {
            Difficulty = difficulty;
            DayOfTheWeek = dayOfTheWeek;
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = hp;
            CurrentHP = hp;
        }



        /*
        ======================================================================================== 
        ShowStats ---> Prints all monster stats that are relevant to the user
        ======================================================================================== 
        */
        public void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n***** {Name} (Enemy) *****");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            Console.ResetColor();
        }



        /*
        ======================================================================================== 
        GetMonstersGoldCoinWorth ---> Get monster exp worth via its difficulty
        ======================================================================================== 
        */
        public int GetMonstersEXPWorth()
        {
            switch (Difficulty)
            {
                case Difficulty.Hard:
                    return RNG.Next(8, 19);

                case Difficulty.Medium:
                    return RNG.Next(4, 13);

                case Difficulty.Easy:
                default:
                    return RNG.Next(1, 5);
            }
        }



        /*
        ======================================================================================== 
        GetMonstersGoldCoinWorth ---> Get monster gold coin worth via its difficulty
        ======================================================================================== 
        */
        public int GetMonstersGoldCoinWorth()
        {
            switch (Difficulty)
            {
                case Difficulty.Hard:
                    return RNG.Next(22, 32);

                case Difficulty.Medium:
                    return RNG.Next(12, 21);

                case Difficulty.Easy:
                default:
                    return RNG.Next(1, 11);
            }
        }



        /*
        ======================================================================================== 
        ShowTodaysMonsters ---> Displays today's monsters (color coded, darker == harder)
        ======================================================================================== 
        */
        public static void ShowTodaysMonsters()
        {
            Console.Clear();

            Console.Title = $"Today's Monsters";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***** Today's Monsters ******\n");

            foreach (Monster monster in Fight.GetTodaysMonsters())
            {
                switch (monster.Difficulty)
                {
                    case Difficulty.Easy:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;

                    case Difficulty.Medium:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                }
                Console.WriteLine($"{monster.Name} - Difficulty: {monster.Difficulty}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }



        /*
        ======================================================================================== 
        ShowTodaysMonsters ---> Displays today's monsters (color coded, darker == harder)
        ======================================================================================== 
        */
        public int GetRunAwayChance(Monster currentMonster)
        {
            int chance;
            switch (currentMonster.Difficulty)
            {
                case Difficulty.Easy:
                    chance = 50;
                    break;

                case Difficulty.Medium:
                    chance = 25;
                    break;

                case Difficulty.Hard:
                    chance = 5;
                    break;

                default:
                    throw new Exception("This shouldn't happen (flee error)");
            }
            return chance;
        }
    }
}