using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.Models
{
    public class AchievementManager : IAchievementManager
    {
        private readonly IConsole _console;
        public int TotalPoints { get; private set; }
        public List<IAchievement> AllAchievements { get; set; }
        public List<IMonster> AllKilledMonsters { get; }

        public AchievementManager(IConsole console)
        {
            _console = console;
            AllAchievements = new List<IAchievement>()
            {
                new Achievement("Kill 1 Monster", AchievementEnum.KillOneMonster, 1),
                new Achievement("Kill 3 Monsters", AchievementEnum.KillThreeMonsters, 2),
                new Achievement("Kill 5 Different Monsters", AchievementEnum.KillFiveDifferentMonsters, 3),
                new Achievement("Kill 10 Monsters", AchievementEnum.KillTenMonsters, 5),
            };
            AllKilledMonsters = new List<IMonster>();
        }

        public List<IAchievement> GetCompletedAchievements() => AllAchievements.Where(ach => ach.IsCompleted).ToList();
        public List<IAchievement> GetUnCompletedAchievements() => AllAchievements.Where(ach => !ach.IsCompleted).ToList();

        private List<IMonster> GetUniqueDeadMonsters() => AllKilledMonsters
                .GroupBy(x => x.Name)
                .Select(y => y.FirstOrDefault())
                .ToList();

        public void AddDeadMonster(IMonster deadMonster)
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
            var numberOfUnCompletedAchievements = GetUnCompletedAchievements().Count;
            for (var i = 0; i < numberOfUnCompletedAchievements; i++)
            {
                CheckForAllAchievements();
            }
        }

        private IAchievement CheckForNumberOfKilledUniqueMonstersAchievements()
        {
            IAchievement foundAchievement = null;

            List<IMonster> uniqueMonsters = GetUniqueDeadMonsters();

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

        private IAchievement CheckForNumberOfKilledMonstersAchievements()
        {
            IAchievement foundAchievement = null;
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
            // Check for the number of killed monsters
            IAchievement foundAchievement = CheckForNumberOfKilledMonstersAchievements();


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
                _console.TextColor = ConsoleColor.Yellow;
                _console.WriteLine($"\nYou Completed An Achievement ({foundAchievement.Title})");
                _console.WriteLine($"(+ {foundAchievement.RewardPoints} AP)\n");
                _console.ResetColor();
            }
        }

        public void PrintAllAchievements()
        {
            foreach (IAchievement achievement in AllAchievements)
            {
                if (achievement.IsCompleted)
                {
                    _console.TextColor = ConsoleColor.Yellow;
                }
                else
                {
                    _console.TextColor = ConsoleColor.DarkGray;
                }
                _console.WriteLine(achievement.ToString());
                _console.ResetColor();
            }
        }
    }
}
