using System.Collections.Generic;
using static OOP_RPG.Models.Enumerations.Difficulty;
using static System.DayOfWeek;

namespace OOP_RPG.ConsoleGame
{
    public static class WeekDayMonsters
    {
        public static List<Monster> InitialMonsters
        {
            get => new List<Monster>()
            {
                // Sunday (0)
                new Monster(Easy, Sunday, "Lesser Squid", 15, 5, 20), // Easy (40 max points)
                new Monster(Easy, Sunday,  "Clam", 5, 35, 10), // Easy (40 max points)
                new Monster(Medium, Sunday,  "Eel", 25, 5, 20), // Medium (60 max points)
                new Monster(Medium, Sunday,  "Pirate", 35, 5, 20), // Medium (60 max points)
                new Monster(Hard, Sunday,  "Titan", 30, 10, 40), // Hard (80 max points)


                // Monday (1)
                new Monster(Easy, Monday, "Rat", 10, 10, 25), // Easy (40 max points)
                new Monster(Easy, Monday, "Wolf", 15, 10, 15), // Easy (40 max points)
                new Monster(Medium, Monday, "Mountain Loin", 30, 10, 20), // Medium (60 max points)
                new Monster(Medium, Monday, "Bear", 20, 25, 15), // Medium (60 max points)
                new Monster(Hard, Monday, "Ent", 30, 15, 35), // Hard (80 max points)


                // Tuesday (2)
                new Monster(Easy, Tuesday, "Crow", 15, 0, 25), // Easy (40 max points)
                new Monster(Easy, Tuesday, "Bee", 20, 0, 15), // Easy (40 max points)
                new Monster(Medium, Tuesday, "Wizard", 30, 5, 25), // Medium (60 max points)
                new Monster(Medium, Tuesday, "Goblin", 10, 25, 25), // Medium (60 max points)
                new Monster(Hard, Tuesday, "Lesser Fire Drake", 25, 35, 20), // Hard (80 max points)


                // Wednesday (3)
                new Monster(Easy, Wednesday, "Spider", 15, 0, 15), // Easy (40 max points)
                new Monster(Easy, Wednesday, "Hollow", 25, 10, 5), // Easy (40 max points)
                new Monster(Medium, Wednesday, "Zombie", 20, 0, 30), // Medium (60 max points)
                new Monster(Medium, Wednesday, "Wraith", 5, 0, 55), // Medium (60 max points)
                new Monster(Hard, Wednesday, "Cave Troll", 20, 10, 50), // Hard (80 max points)


                // Thursday (4)
                new Monster(Easy, Thursday, "Peasant", 10, 0, 30), // Easy (40 max points)
                new Monster(Easy, Thursday, "Bandit", 10, 10, 20), // Easy (40 max points)
                new Monster(Medium, Thursday, "Knight", 15, 20, 25), // Medium (60 max points)
                new Monster(Medium, Thursday, "Mercenary", 20, 20, 20), // Medium (60 max points)
                new Monster(Hard, Thursday, "Veteran Knight", 35, 20, 25), // Hard (80 max points)


                // Friday (5)
                new Monster(Easy, Friday, "Slime", 1, 29, 10), // Easy (40 max points)
                new Monster(Easy, Friday, "Fairy", 25, 5, 10), // Easy (40 max points)
                new Monster(Medium, Friday, "Druid", 20, 15, 25), // Medium (60 max points)
                new Monster(Medium, Friday, "Elf", 30, 15, 15), // Medium (60 max points)
                new Monster(Hard, Friday, "Vampire", 15, 5, 60), // Hard (80 max points)


                // Saturday (6)
                new Monster(Easy, Saturday, "Lesser Ghost", 5, 5, 30), // Easy (40 max points)
                new Monster(Easy, Saturday, "Lesser Skeleton", 15, 15, 10), // Easy (40 max points)
                new Monster(Medium, Saturday, "Skeleton", 25, 15, 20), // Medium (60 max points)
                new Monster(Medium, Saturday, "Ghoul", 15, 5, 40), // Medium (60 max points)
                new Monster(Hard, Saturday, "Necromancer", 25, 10, 45), // Hard (80 max points)
            };
        }


    }
}
