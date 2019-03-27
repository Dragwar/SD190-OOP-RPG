using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class HandleAchievements
    {
        public int TotalPoints { get; private set; }
        public List<Achievement> AllAchievements { get; set; }
        public List<Monster> AllKilledMonsters { get; }

        public HandleAchievements()
        {
            AllAchievements = new List<Achievement>()
            {
                new Achievement("Kill 1 Monster", AchievementEnum.KillOneMonster, 1),
                new Achievement("Kill 3 Monsters", AchievementEnum.KillThreeMonsters, 2),
                new Achievement("Kill 5 Different Monsters", AchievementEnum.KillFiveDifferentMonsters, 3),
                new Achievement("Kill 10 Monsters", AchievementEnum.KillTenMonsters, 5),
            };
            AllKilledMonsters = new List<Monster>();
        }

        public List<Achievement> GetCompletedAchievements() => AllAchievements.Where(ach => ach.IsCompleted).ToList();
        public List<Achievement> GetUnCompletedAchievements() => AllAchievements.Where(ach => !ach.IsCompleted).ToList();

        private List<Monster> GetUniqueDeadMonsters() => AllKilledMonsters
                .GroupBy(x => x.Name)
                .Select(y => y.FirstOrDefault())
                .ToList();

        public void AddDeadMonster(Monster deadMonster)
        {
            if (deadMonster.CurrentHP > 0)
            {
                throw new Exception("The monster is still alive (Achievement error)");
            }
            AllKilledMonsters.Add(deadMonster);


            // This will check and complete multiple Achievements
            // example:
            // 1st Achievement = "Kill 1 monster"
            // 2nd Achievement = "Kill 1 unique monster"
            // This for loop will loop over uncompleted Achievements then will complete both
            // *Hero Kills 1 Monster*
            // 1st Achievement and the 2nd Achievement will get completed
            int numberOfUnCompletedAchievements = GetUnCompletedAchievements().Count;
            for (int i = 0; i < numberOfUnCompletedAchievements; i++)
            {
                CheckForAllAchievements();
            }
        }

        private Achievement CheckForNumberOfKilledUniqueMonstersAchievements()
        {
            Achievement foundAchievement = null;

            List<Monster> uniqueMonsters = GetUniqueDeadMonsters();

            switch (uniqueMonsters.Count)
            {
                case 5:
                    foundAchievement = GetUnCompletedAchievements()
                       .Where(ach => ach.EnumTitle == AchievementEnum.KillFiveDifferentMonsters)
                       .FirstOrDefault();
                    break;

                default:
                    break;
            }
            return foundAchievement;
        }

        private Achievement CheckForNumberOfKilledMonstersAchievements()
        {
            Achievement foundAchievement = null;
            switch (AllKilledMonsters.Count)
            {
                case 1:
                    foundAchievement = GetUnCompletedAchievements()
                        .Where(ach => ach.EnumTitle == AchievementEnum.KillOneMonster)
                        .FirstOrDefault();
                    break;

                case 3:
                    foundAchievement = GetUnCompletedAchievements()
                        .Where(ach => ach.EnumTitle == AchievementEnum.KillThreeMonsters)
                        .FirstOrDefault();
                    break;

                case 10:
                    foundAchievement = GetUnCompletedAchievements()
                        .Where(ach => ach.EnumTitle == AchievementEnum.KillTenMonsters)
                        .FirstOrDefault();
                    break;
            }
            return foundAchievement;
        }

        public void CheckForAllAchievements()
        {
            Achievement foundAchievement = null;

            // Check for the number of killed monsters
            foundAchievement = CheckForNumberOfKilledMonstersAchievements();


            if (foundAchievement == null)
            {
                // Check for the different number of unique killed monsters
                foundAchievement = CheckForNumberOfKilledUniqueMonstersAchievements();
            }


            if (foundAchievement != null)
            {
                // Display newly completed achievement
                foundAchievement.IsCompleted = true;
                foundAchievement.CompletedDate = $"{DateTime.Now.ToShortDateString()} ({DateTime.Now.ToShortTimeString()})";
                TotalPoints += foundAchievement.RewardPoints;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou Completed An Achievement ({foundAchievement.Title})");
                Console.WriteLine($"(+ {foundAchievement.RewardPoints} AP)\n");
                Console.ResetColor();
            }
        }

        public void PrintAllAchievements()
        {
            foreach (Achievement achievement in AllAchievements)
            {
                if (achievement.IsCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.WriteLine(achievement.ToString());
                Console.ResetColor();
            }
        }
    }
}
