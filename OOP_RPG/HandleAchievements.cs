using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class HandleAchievements
    {
        public static int TotalPoints { get; private set; }
        public List<Achievement> AllAchievements { get; }
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

        public void AddDeadMonster(Monster deadMonster)
        {
            if (deadMonster.CurrentHP > 0)
            {
                throw new Exception("The monster is still alive (Achievement error)");
            }
            AllKilledMonsters.Add(deadMonster);
            CheckForAllAchievements();
        }

        private Achievement CheckForNumberOfKilledUniqueMonstersAchievements()
        {
            Achievement foundAchievement = null;

            List<Monster> uniqueMonsters = AllKilledMonsters
                .GroupBy(x => x.Name)
                .Select(y => y.FirstOrDefault())
                .ToList();

            if (uniqueMonsters.Count >= 5)
            {
                foundAchievement = AllAchievements
                    .Where(ach => ach.EnumTitle == AchievementEnum.KillFiveDifferentMonsters)
                    .FirstOrDefault();
            }

            return foundAchievement;
        }

        // YOU CAN ONLY COMPLETE ONE ACHIEVENMENT AT A TIME
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
            foreach(Achievement achievement in AllAchievements)
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
