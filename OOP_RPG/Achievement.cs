﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Achievement
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
        (
            $"\n=========({Title})=========\n" +
            $"Reward: {RewardPoints} {(RewardPoints > 1 ? "points" : "point")}\n" +
            $"Status: {(IsCompleted ? $"Completed On {CompletedDate}" : "Not Completed")}\n"
        );
    }
}
