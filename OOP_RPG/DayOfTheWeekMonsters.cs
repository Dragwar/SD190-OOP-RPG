using System.Collections.Generic;

namespace OOP_RPG
{
    public static class DayOfTheWeekMonsters
    {
        public static List<Monster> InitialMonsters
        {
            get => new List<Monster>()
            {
                // Sunday (0)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Sunday, "Lesser Squid", 15, 5, 20, 20), // Easy (40 max points)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Sunday,  "Clam", 5, 35, 10, 10), // Easy (40 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Sunday,  "Eel", 25, 5, 20, 20), // Medium (60 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Sunday,  "Pirate", 35, 5, 20, 20), // Medium (60 max points)
                new Monster((int)Difficulty.Hard, (int)DaysOfTheWeek.Sunday,  "Titan", 30, 10, 40, 40), // Hard (80 max points)


                // Monday (1)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Monday, "Rat", 10, 10, 25, 25), // Easy (40 max points)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Monday, "Wolf", 15, 10, 15, 15), // Easy (40 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Monday, "Mountain Loin", 30, 10, 20, 20), // Medium (60 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Monday, "Bear", 20, 25, 15, 15), // Medium (60 max points)
                new Monster((int)Difficulty.Hard, (int)DaysOfTheWeek.Monday, "Ent", 30, 15, 35, 35), // Hard (80 max points)


                // Tuesday (2)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Tuesday, "Crow", 15, 0, 25, 25), // Easy (40 max points)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Tuesday, "Bee", 20, 0, 15, 15), // Easy (40 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Tuesday, "Wizard", 30, 5, 25, 25), // Medium (60 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Tuesday, "Goblin", 10, 25, 25, 25), // Medium (60 max points)
                new Monster((int)Difficulty.Hard, (int)DaysOfTheWeek.Tuesday, "Lesser Fire Drake", 25, 35, 20, 20), // Hard (80 max points)


                // Wednesday (3)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Wednesday, "Spider", 15, 0, 15, 15), // Easy (40 max points)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Wednesday, "Hollow", 25, 10, 5, 5), // Easy (40 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Wednesday, "Zombie", 20, 0, 30, 30), // Medium (60 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Wednesday, "Wraith", 5, 0, 55, 55), // Medium (60 max points)
                new Monster((int)Difficulty.Hard, (int)DaysOfTheWeek.Wednesday, "Cave Troll", 20, 10, 50, 50), // Hard (80 max points)


                // Thursday (4)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Thursday, "Peasant", 10, 0, 30, 30), // Easy (40 max points)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Thursday, "Bandit", 10, 10, 20, 20), // Easy (40 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Thursday, "Knight", 15, 20, 25, 25), // Medium (60 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Thursday, "Mercenary", 20, 20, 20, 20), // Medium (60 max points)
                new Monster((int)Difficulty.Hard, (int)DaysOfTheWeek.Thursday, "Veteran Knight", 35, 20, 25, 25), // Hard (80 max points)


                // Friday (5)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Friday, "Slime", 1, 29, 10, 10), // Easy (40 max points)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Friday, "Fairy", 25, 5, 10, 10), // Easy (40 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Friday, "Druid", 20, 15, 25, 25), // Medium (60 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Friday, "Elf", 30, 15, 15, 15), // Medium (60 max points)
                new Monster((int)Difficulty.Hard, (int)DaysOfTheWeek.Friday, "Vampire", 15, 5, 60, 60), // Hard (80 max points)


                // Saturday (6)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Saturday, "Lesser Ghost", 5, 5, 30, 30), // Easy (40 max points)
                new Monster((int)Difficulty.Easy, (int)DaysOfTheWeek.Saturday, "Lesser Skeleton", 15, 15, 10, 10), // Easy (40 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Saturday, "Skeleton", 25, 15, 20, 20), // Medium (60 max points)
                new Monster((int)Difficulty.Medium, (int)DaysOfTheWeek.Saturday, "Ghoul", 15, 5, 40, 40), // Medium (60 max points)
                new Monster((int)Difficulty.Hard, (int)DaysOfTheWeek.Saturday, "Necromancer", 25, 10, 45, 45), // Hard (80 max points)
            };
        }


    }
}
