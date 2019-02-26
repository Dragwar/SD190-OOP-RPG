using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public static class WeekDayMonsters
    {
        public static List<Monster> InitialMonsters
        {
            get => new List<Monster>()
            {
                // Sunday (0)
                new Monster(Difficulty.Easy, DayOfWeek.Sunday, "Lesser Squid", 15, 5, 20), // Easy (40 max points)
                new Monster(Difficulty.Easy, DayOfWeek.Sunday,  "Clam", 5, 35, 10), // Easy (40 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Sunday,  "Eel", 25, 5, 20), // Medium (60 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Sunday,  "Pirate", 35, 5, 20), // Medium (60 max points)
                new Monster(Difficulty.Hard, DayOfWeek.Sunday,  "Titan", 30, 10, 40), // Hard (80 max points)


                // Monday (1)
                new Monster(Difficulty.Easy, DayOfWeek.Monday, "Rat", 10, 10, 25), // Easy (40 max points)
                new Monster(Difficulty.Easy, DayOfWeek.Monday, "Wolf", 15, 10, 15), // Easy (40 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Monday, "Mountain Loin", 30, 10, 20), // Medium (60 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Monday, "Bear", 20, 25, 15), // Medium (60 max points)
                new Monster(Difficulty.Hard, DayOfWeek.Monday, "Ent", 30, 15, 35), // Hard (80 max points)


                // Tuesday (2)
                new Monster(Difficulty.Easy, DayOfWeek.Tuesday, "Crow", 15, 0, 25), // Easy (40 max points)
                new Monster(Difficulty.Easy, DayOfWeek.Tuesday, "Bee", 20, 0, 15), // Easy (40 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Tuesday, "Wizard", 30, 5, 25), // Medium (60 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Tuesday, "Goblin", 10, 25, 25), // Medium (60 max points)
                new Monster(Difficulty.Hard, DayOfWeek.Tuesday, "Lesser Fire Drake", 25, 35, 20), // Hard (80 max points)


                // Wednesday (3)
                new Monster(Difficulty.Easy, DayOfWeek.Wednesday, "Spider", 15, 0, 15), // Easy (40 max points)
                new Monster(Difficulty.Easy, DayOfWeek.Wednesday, "Hollow", 25, 10, 5), // Easy (40 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Wednesday, "Zombie", 20, 0, 30), // Medium (60 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Wednesday, "Wraith", 5, 0, 55), // Medium (60 max points)
                new Monster(Difficulty.Hard, DayOfWeek.Wednesday, "Cave Troll", 20, 10, 50), // Hard (80 max points)


                // Thursday (4)
                new Monster(Difficulty.Easy, DayOfWeek.Thursday, "Peasant", 10, 0, 30), // Easy (40 max points)
                new Monster(Difficulty.Easy, DayOfWeek.Thursday, "Bandit", 10, 10, 20), // Easy (40 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Thursday, "Knight", 15, 20, 25), // Medium (60 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Thursday, "Mercenary", 20, 20, 20), // Medium (60 max points)
                new Monster(Difficulty.Hard, DayOfWeek.Thursday, "Veteran Knight", 35, 20, 25), // Hard (80 max points)


                // Friday (5)
                new Monster(Difficulty.Easy, DayOfWeek.Friday, "Slime", 1, 29, 10), // Easy (40 max points)
                new Monster(Difficulty.Easy, DayOfWeek.Friday, "Fairy", 25, 5, 10), // Easy (40 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Friday, "Druid", 20, 15, 25), // Medium (60 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Friday, "Elf", 30, 15, 15), // Medium (60 max points)
                new Monster(Difficulty.Hard, DayOfWeek.Friday, "Vampire", 15, 5, 60), // Hard (80 max points)


                // Saturday (6)
                new Monster(Difficulty.Easy, DayOfWeek.Saturday, "Lesser Ghost", 5, 5, 30), // Easy (40 max points)
                new Monster(Difficulty.Easy, DayOfWeek.Saturday, "Lesser Skeleton", 15, 15, 10), // Easy (40 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Saturday, "Skeleton", 25, 15, 20), // Medium (60 max points)
                new Monster(Difficulty.Medium, DayOfWeek.Saturday, "Ghoul", 15, 5, 40), // Medium (60 max points)
                new Monster(Difficulty.Hard, DayOfWeek.Saturday, "Necromancer", 25, 10, 45), // Hard (80 max points)
            };
        }


    }
}
