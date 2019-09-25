using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Interfaces;

namespace OOP_RPG.Models
{
    public class Achievement : IAchievement
    {
        public string Title { get; }
        public AchievementEnum EnumTitle { get; }
        public int RewardPoints { get; }
        public bool IsCompleted { get; set; }
        public string CompletedDate { get; set; }

        public Achievement(string title, AchievementEnum enumTitle, int rewardPoints)
        {
            Title = title;
            EnumTitle = enumTitle;
            RewardPoints = rewardPoints;
            IsCompleted = false;
            CompletedDate = null;
        }

        public override string ToString() =>
            $"\n=========({Title})=========\n" +
            $"Reward: {RewardPoints} {(RewardPoints > 1 ? "points" : "point")}\n" +
            $"Status: {(IsCompleted ? $"Completed On {CompletedDate}" : "Not Completed")}\n"
        ;
    }
}
