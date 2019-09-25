using System.Collections.Generic;

namespace OOP_RPG.Models.Interfaces
{
    public interface IAchievementManager
    {
        List<IAchievement> AllAchievements { get; set; }
        List<IMonster> AllKilledMonsters { get; }
        int TotalPoints { get; }

        void AddDeadMonster(IMonster deadMonster);
        void CheckForAllAchievements();
        List<IAchievement> GetCompletedAchievements();
        List<IAchievement> GetUnCompletedAchievements();
        void PrintAllAchievements();
    }
}