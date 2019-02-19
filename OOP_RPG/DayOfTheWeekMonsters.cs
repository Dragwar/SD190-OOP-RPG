using System.Collections.Generic;

namespace OOP_RPG
{
    public class DayOfTheWeekMonsters
    {
        public readonly List<Monster> SundayMonsters = new List<Monster>()
        {
            new Monster((int)Difficulty.Easy, "Lesser Squid", 15, 5, 20, 20), // Easy (40 max points)
            new Monster((int)Difficulty.Easy, "Clam", 5, 35, 10, 10), // Easy (40 max points)
            new Monster((int)Difficulty.Medium, "Eel", 25, 5, 20, 20), // Medium (60 max points)
            new Monster((int)Difficulty.Medium, "Pirate", 35, 5, 20, 20), // Medium (60 max points)
            new Monster((int)Difficulty.Hard, "Titan", 30, 10, 40, 40), // Hard (80 max points)
        };

        public readonly List<Monster> MondayMonsters = new List<Monster>()
        {
            new Monster((int)Difficulty.Easy, "Rat", 10, 10, 25, 25), // Easy (40 max points)
            new Monster((int)Difficulty.Easy, "Wolf", 15, 10, 15, 15), // Easy (40 max points)
            new Monster((int)Difficulty.Medium, "Mountain Loin", 30, 10, 20, 20), // Medium (60 max points)
            new Monster((int)Difficulty.Medium, "Bear", 20, 25, 15, 15), // Medium (60 max points)
            new Monster((int)Difficulty.Hard, "Ent", 30, 15, 35, 35), // Hard (80 max points)
        };

        public readonly List<Monster> TuesdayMonsters = new List<Monster>()
        {
            new Monster((int)Difficulty.Easy, "Crow", 15, 0, 25, 25), // Easy (40 max points)
            new Monster((int)Difficulty.Easy, "Bee", 20, 0, 15, 15), // Easy (40 max points)
            new Monster((int)Difficulty.Medium, "Wizard", 30, 5, 25, 25), // Medium (60 max points)
            new Monster((int)Difficulty.Medium, "Goblin", 10, 25, 25, 25), // Medium (60 max points)
            new Monster((int)Difficulty.Hard, "Lesser Fire Drake", 25, 35, 20, 20), // Hard (80 max points)
        };

        public readonly List<Monster> WednesdayMonsters = new List<Monster>()
        {
            new Monster((int)Difficulty.Easy, "Spider", 15, 0, 15, 15), // Easy (40 max points)
            new Monster((int)Difficulty.Easy, "Hollow", 25, 10, 5, 5), // Easy (40 max points)
            new Monster((int)Difficulty.Medium, "Zombie", 20, 0, 30, 30), // Medium (60 max points)
            new Monster((int)Difficulty.Medium, "Wraith", 5, 0, 55, 55), // Medium (60 max points)
            new Monster((int)Difficulty.Hard, "Cave Troll", 20, 10, 50, 50), // Hard (80 max points)
        };

        public readonly List<Monster> ThursdayMonsters = new List<Monster>()
        {
            new Monster((int)Difficulty.Easy, "Peasant", 10, 0, 30, 30), // Easy (40 max points)
            new Monster((int)Difficulty.Easy, "Bandit", 10, 10, 20, 20), // Easy (40 max points)
            new Monster((int)Difficulty.Medium, "Knight", 15, 20, 25, 25), // Medium (60 max points)
            new Monster((int)Difficulty.Medium, "Mercenary", 20, 20, 20, 20), // Medium (60 max points)
            new Monster((int)Difficulty.Hard, "Veteran Knight", 35, 20, 25, 25), // Hard (80 max points)
        };

        public readonly List<Monster> FridayMonsters = new List<Monster>()
        {
            new Monster((int)Difficulty.Easy, "Slime", 1, 29, 10, 10), // Easy (40 max points)
            new Monster((int)Difficulty.Easy, "Fairy", 25, 5, 10, 10), // Easy (40 max points)
            new Monster((int)Difficulty.Medium, "Druid", 20, 15, 25, 25), // Medium (60 max points)
            new Monster((int)Difficulty.Medium, "Elf", 30, 15, 15, 15), // Medium (60 max points)
            new Monster((int)Difficulty.Hard, "Vampire", 15, 5, 60, 60), // Hard (80 max points)
        };

        public readonly List<Monster> SaturdayMonsters = new List<Monster>()
        {
            new Monster((int)Difficulty.Easy, "Lesser Ghost", 5, 5, 30, 30), // Easy (40 max points)
            new Monster((int)Difficulty.Easy, "Lesser Skeleton", 15, 15, 10, 10), // Easy (40 max points)
            new Monster((int)Difficulty.Medium, "Skeleton", 25, 15, 20, 20), // Medium (60 max points)
            new Monster((int)Difficulty.Medium, "Ghoul", 15, 5, 40, 40), // Medium (60 max points)
            new Monster((int)Difficulty.Hard, "Necromancer", 25, 10, 45, 45), // Hard (80 max points)
        };

        public List<List<Monster>> GetAllMonsters()
        {
            var allMonsters = new List<List<Monster>>()
            {
                SundayMonsters,
                MondayMonsters,
                TuesdayMonsters,
                WednesdayMonsters,
                ThursdayMonsters,
                FridayMonsters,
                SaturdayMonsters,
            };
            return allMonsters;
        }
    }

}

/*
// Monday
AddMonster((int)Difficulty.Easy, "Rat", 10, 10, 25); // Easy (40 max points)
AddMonster((int)Difficulty.Easy, "Wolf", 15, 10, 15); // Easy (40 max points)
AddMonster((int)Difficulty.Medium, "Mountain Loin", 30, 10, 20); // Medium (60 max points)
AddMonster((int)Difficulty.Medium, "Bear", 20, 25, 15); // Medium (60 max points)
AddMonster((int)Difficulty.Hard, "Ent", 30, 15, 35); // Hard (80 max points)

// Tuesday
AddMonster((int)Difficulty.Easy, "Crow", 15, 0, 25); // Easy (40 max points)
AddMonster((int)Difficulty.Easy, "Bee", 20, 0, 15); // Easy (40 max points)
AddMonster((int)Difficulty.Medium, "Wizard", 30, 5, 25); // Medium (60 max points)
AddMonster((int)Difficulty.Medium, "Goblin", 10, 25, 25); // Medium (60 max points)
AddMonster((int)Difficulty.Hard, "Lesser Fire Drake", 25, 35, 20); // Hard (80 max points)

// Wednesday
AddMonster((int)Difficulty.Easy, "Spider", 15, 0, 15); // Easy (40 max points)
AddMonster((int)Difficulty.Easy, "Hollow", 25, 10, 5); // Easy (40 max points)
AddMonster((int)Difficulty.Medium, "Zombie", 20, 0, 30); // Medium (60 max points)
AddMonster((int)Difficulty.Medium, "Wraith", 5, 0, 55); // Medium (60 max points)
AddMonster((int)Difficulty.Hard, "Cave Troll", 20, 10, 50); // Hard (80 max points)

// Thursday
AddMonster((int)Difficulty.Easy, "Peasant", 10, 0, 30); // Easy (40 max points)
AddMonster((int)Difficulty.Easy, "Bandit", 10, 10, 20); // Easy (40 max points)
AddMonster((int)Difficulty.Medium, "Knight", 15, 20, 25); // Medium (60 max points)
AddMonster((int)Difficulty.Medium, "Mercenary", 20, 20, 20); // Medium (60 max points)
AddMonster((int)Difficulty.Hard, "Veteran Knight", 35, 20, 25); // Hard (80 max points)

// Friday
AddMonster((int)Difficulty.Easy, "Slime", 1, 29, 10); // Easy (40 max points)
AddMonster((int)Difficulty.Easy, "Fairy", 25, 5, 10); // Easy (40 max points)
AddMonster((int)Difficulty.Medium, "Druid", 20, 15, 25); // Medium (60 max points)
AddMonster((int)Difficulty.Medium, "Elf", 30, 15, 15); // Medium (60 max points)
AddMonster((int)Difficulty.Hard, "Vampire", 15, 5, 60); // Hard (80 max points)

// Saturday
AddMonster((int)Difficulty.Easy, "Lesser Ghost", 5, 5, 30); // Easy (40 max points)
AddMonster((int)Difficulty.Easy, "Lesser Skeleton", 15, 15, 10); // Easy (40 max points)
AddMonster((int)Difficulty.Medium, "Skeleton", 25, 15, 20); // Medium (60 max points)
AddMonster((int)Difficulty.Medium, "Ghoul", 15, 5, 40); // Medium (60 max points)
AddMonster((int)Difficulty.Hard, "Necromancer", 25, 10, 45); // Hard (80 max points)

// Sunday
AddMonster((int)Difficulty.Easy, "Lesser Squid", 15, 5, 20); // Easy (40 max points)
AddMonster((int)Difficulty.Easy, "Clam", 5, 35, 10); // Easy (40 max points)
AddMonster((int)Difficulty.Medium, "Eel", 25, 5, 20); // Medium (60 max points)
AddMonster((int)Difficulty.Medium, "Pirate", 35, 5, 20); // Medium (60 max points)
AddMonster((int)Difficulty.Hard, "Titan", 30, 10, 40); // Hard (80 max points)
*/
