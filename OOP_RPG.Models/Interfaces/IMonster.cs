using System;
using OOP_RPG.Models.Enumerations;

namespace OOP_RPG.Models.Interfaces
{
    public interface IMonster
    {
        int CurrentHP { get; set; }
        DayOfWeek DayOfTheWeek { get; }
        int Defense { get; }
        Difficulty Difficulty { get; }
        string Name { get; }
        int OriginalHP { get; }
        int Strength { get; }

        int GetMonstersEXPWorth();
        int GetMonstersGoldCoinWorth();
        int GetRunAwayChance(IMonster currentMonster);
        void ShowStats();
    }
}