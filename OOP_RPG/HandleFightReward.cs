using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public static class HandleFightReward
    {
        public static int GetMonstersEXPWorth(int currentMonsterDifficulty)
        {
            int monstersEXPWorth;
            Random rand = new Random();

            switch (currentMonsterDifficulty)
            {
                case (int)Difficulty.Hard:
                    monstersEXPWorth = rand.Next(8, 19);
                    break;

                case (int)Difficulty.Medium:
                    monstersEXPWorth = rand.Next(4, 13);
                    break;

                default:
                    monstersEXPWorth = rand.Next(1, 5);
                    break;
            }

            return monstersEXPWorth;
        }

        public static int GetMonstersGoldCoinWorth(int currentMonsterDifficulty)
        {
            int monstersGoldCoinWorth;
            Random rand = new Random();

            switch (currentMonsterDifficulty)
            {
                case (int)Difficulty.Hard:
                    monstersGoldCoinWorth = rand.Next(22, 32);
                    break;

                case (int)Difficulty.Medium:
                    monstersGoldCoinWorth = rand.Next(12, 21);
                    break;

                default:
                    monstersGoldCoinWorth = rand.Next(1, 11);
                    break;
            }

            return monstersGoldCoinWorth;
        }


    }
}
