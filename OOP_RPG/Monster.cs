using System;

namespace OOP_RPG
{
    public class Monster
    {
        public int Difficulty { get; }
        public int DayOfTheWeek { get; }
        public string Name { get; }
        public int Strength { get; }
        public int Defense { get; }
        public int OriginalHP { get; }
        public int CurrentHP { get; set; }

        public Monster(int difficulty, int dayOfTheWeek, string name, int strength, int defense, int originalHP, int currentHP)
        {
            Difficulty = difficulty;
            DayOfTheWeek = dayOfTheWeek;
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = originalHP;
            CurrentHP = currentHP;
        }

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
    }
}