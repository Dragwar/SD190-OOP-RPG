using System;

namespace OOP_RPG
{
    public class Monster
    {
        public Difficulty Difficulty { get; }
        public DayOfWeek DayOfTheWeek { get; }
        public string Name { get; }
        public int Strength { get; }
        public int Defense { get; }
        public int OriginalHP { get; }
        public int CurrentHP { get; set; }

        public Monster(Difficulty difficulty, DayOfWeek dayOfTheWeek, string name, int strength, int defense, int hp)
        {
            Difficulty = difficulty;
            DayOfTheWeek = dayOfTheWeek;
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = hp;
            CurrentHP = hp;
        }



        /*
        ======================================================================================== 
        ShowStats ---> Prints all monster stats that are relevant to the user
        ======================================================================================== 
        */
        public void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n***** {Name} (Enemy) *****");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            Console.ResetColor();
        }



        /*
        ======================================================================================== 
        GetMonstersGoldCoinWorth ---> Get monster exp worth via its difficulty
        ======================================================================================== 
        */
        public int GetMonstersEXPWorth()
        {
            int monstersEXPWorth;
            Random rand = new Random();

            switch (Difficulty)
            {
                case Difficulty.Hard:
                    monstersEXPWorth = rand.Next(8, 19);
                    break;

                case Difficulty.Medium:
                    monstersEXPWorth = rand.Next(4, 13);
                    break;

                default:
                    monstersEXPWorth = rand.Next(1, 5);
                    break;
            }

            return monstersEXPWorth;
        }



        /*
        ======================================================================================== 
        GetMonstersGoldCoinWorth ---> Get monster gold coin worth via its difficulty
        ======================================================================================== 
        */
        public int GetMonstersGoldCoinWorth()
        {
            int monstersGoldCoinWorth;
            Random rand = new Random();

            switch (Difficulty)
            {
                case Difficulty.Hard:
                    monstersGoldCoinWorth = rand.Next(22, 32);
                    break;

                case Difficulty.Medium:
                    monstersGoldCoinWorth = rand.Next(12, 21);
                    break;

                default:
                    monstersGoldCoinWorth = rand.Next(1, 11);
                    break;
            }

            return monstersGoldCoinWorth;
        }



        /*
        ======================================================================================== 
        ShowTodaysMonsters ---> Displays today's monsters (color coded, darker == harder)
        ======================================================================================== 
        */
        public static void ShowTodaysMonsters()
        {
            Console.Clear();

            Console.Title = $"Today's Monsters";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***** Today's Monsters ******\n");

            foreach (Monster monster in Fight.GetTodaysMonsters())
            {
                switch (monster.Difficulty)
                {
                    case Difficulty.Easy:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;

                    case Difficulty.Medium:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                }
                Console.WriteLine($"{monster.Name} - Difficulty: {monster.Difficulty}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }


    }
}