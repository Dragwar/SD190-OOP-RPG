using OOP_RPG.Models.Enumerations;

namespace OOP_RPG.Models.Interfaces
{
    public interface IAchievement
    {
        string CompletedDate { get; set; }
        AchievementEnum EnumTitle { get; }
        bool IsCompleted { get; set; }
        int RewardPoints { get; }
        string Title { get; }

        string ToString();
    }
}