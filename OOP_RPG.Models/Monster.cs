using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Interfaces;
using System;

namespace OOP_RPG.Models
{
    public class Monster : IMonster
    {
        private readonly IConsole _console;
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
            _console = new Terminal();
        }
        public Monster(IConsole console, Difficulty difficulty, DayOfWeek dayOfTheWeek, string name, int strength, int defense, int hp)
        {
            _console = console;
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
            _console.ForegroundColor = ConsoleColor.Yellow;
            _console.WriteLine($"\n***** {Name} (Enemy) *****");
            _console.ResetColor();

            _console.ForegroundColor = ConsoleColor.Red;
            _console.WriteLine($"Strength: {Strength}");
            _console.WriteLine($"Defense: {Defense}");
            _console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            _console.ResetColor();
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
        public int GetRunAwayChance(IMonster currentMonster)
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