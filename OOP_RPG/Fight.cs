using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Fight
    {
        private Random Random { get; }
        private List<Monster> Monsters { get; }
        private Hero Hero { get; }
        private Monster CurrentMonster { get; }
        private int MonstersEXPWorth { get; }
        private int MonstersGoldCoinWorth { get; }

        // Not Implemented yet
        // private int TotalHeroPoints { get; }

        /*
        ======================================================================================== 
        Fight ---> Initializes the fight and selects a random monster from today's monsters
        ======================================================================================== 
        */
        public Fight(Hero hero)
        {
            Hero = hero;
            Random = new Random();

            // Not Implemented yet
            // TODO: use this to up the difficulty for the monsters
            // TotalHeroPoints = Hero.OriginalHP + Hero.Strength + Hero.Defense;

            Monsters = new List<Monster>(GetTodaysMonsters());

            CurrentMonster = Monsters[Random.Next(0, Monsters.Count)];

            MonstersEXPWorth = CurrentMonster.GetMonstersEXPWorth();
            MonstersGoldCoinWorth = CurrentMonster.GetMonstersGoldCoinWorth();
        }



        /*
        ======================================================================================== 
        GetTodaysMonsters ---> Gets all the monsters and only returns today's monsters
        ======================================================================================== 
        */
        public static List<Monster> GetTodaysMonsters()
        {
            List<Monster> allMonsters = new List<Monster>(WeekDayMonsters.InitialMonsters);

            List<Monster> todaysMonsters = allMonsters
                .Where(monster => monster.DayOfTheWeek == DateTime.Now.DayOfWeek)
                .ToList();

            return todaysMonsters;
        }



        /*
        ======================================================================================== 
        Start ---> Fight menu (choose to fight, see stats, or maybe more options in the future)
        ======================================================================================== 
        */
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nA {CurrentMonster.Name}! (Strength = {CurrentMonster.Strength} | Defense = {CurrentMonster.Defense} | HP = {CurrentMonster.CurrentHP})");
            Console.ResetColor();

            while (CurrentMonster.CurrentHP > 0 && Hero.CurrentHP > 0)
            {
                Console.Title = $"FIGHT!!! ({Hero.Name} vs {CurrentMonster.Name}) Your Current HP: {Hero.CurrentHP} | Enemy Current HP: {CurrentMonster.CurrentHP}";
                Console.WriteLine($"\nWhat will you do?");
                Console.WriteLine("1. Fight");
                Console.WriteLine("2. Use Health Potion");
                Console.WriteLine("3. See The Enemy's Status and Your Status");
                Console.WriteLine("4. Flee");

                string input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    HeroTurn();
                }
                else if (input == "2")
                {
                    //UseHealthPotion();
                }
                else if (input == "3")
                {
                    Hero.ShowStats();
                    CurrentMonster.ShowStats();
                }
                else if (input == "4")
                {
                    //Flee();
                }
            }
        }



        /*
        ======================================================================================== 
        HeroTurn ---> Calculate the damage that will be dealt to the currentMonster
        ======================================================================================== 
        */
        private void HeroTurn()
        {
            int damage;
            int compare = Hero.Strength - CurrentMonster.Defense;

            if (Hero.EquippedWeapon != null)
            {
                Weapon weapon = Hero.EquippedWeapon;
                int weaponDamage = Random.Next(weapon.MinDamage, weapon.MaxDamage + 1);
                int finalDamage = Hero.Strength + weaponDamage;
                compare = finalDamage - CurrentMonster.Defense;
            }

            if (compare <= 0)
            {
                damage = 1;
                CurrentMonster.CurrentHP -= damage;
            }
            else
            {
                damage = compare;
                CurrentMonster.CurrentHP -= damage;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nYou did {damage} damage!");
            Console.WriteLine($"Monster's HP: {CurrentMonster.CurrentHP}/{CurrentMonster.OriginalHP}");
            Console.ResetColor();

            if (CurrentMonster.CurrentHP <= 0)
            {
                Win();
            }
            else
            {
                MonsterTurn();
            }
        }



        /*
        ======================================================================================== 
        MonsterTurn ---> Calculate the damage dealt to the Hero
        ======================================================================================== 
        */
        private void MonsterTurn()
        {
            int damage;
            int compare = CurrentMonster.Strength - Hero.Defense;

            if (Hero.EquippedArmor != null)
            {
                Armor armor = Hero.EquippedArmor;
                int armorDefense = Random.Next(armor.MinDefense, armor.MaxDefense + 1);
                int finalDefense = armorDefense + Hero.Defense;
                compare = CurrentMonster.Strength - finalDefense;
            }

            if (compare <= 0)
            {
                damage = 1;
                Hero.TakeDamage(damage);
            }
            else
            {
                damage = compare;
                Hero.TakeDamage(damage);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{CurrentMonster.Name} does {damage} damage!");
            Console.WriteLine($"{Hero.Name}'s HP: {Hero.CurrentHP}/{Hero.OriginalHP}");
            Console.ResetColor();

            if (Hero.CurrentHP <= 0)
            {
                Lose();
            }
        }



        /*
        ======================================================================================== 
        Win ---> Win Message and returns to the Main Menu
        ======================================================================================== 
        */
        private void Win()
        {
            Hero.AddExperiencePoints(MonstersEXPWorth);
            Hero.AddGoldCoins(MonstersGoldCoinWorth);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{CurrentMonster.Name} has been defeated! You win the battle!");
            Console.WriteLine($"(+ {MonstersGoldCoinWorth} Gold Coins)");
            Console.WriteLine($"(+ {MonstersEXPWorth} EXP)");
            Console.ResetColor();

            Hero.ShowStats();

            Console.Title = $"Main Menu";
        }



        /*
        ======================================================================================== 
        Lose ---> Lose Message and exits the game
        ======================================================================================== 
        */
        private void Lose()
        {
            Console.Title = $"Better Luck Next Time.";

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("You've been defeated! :( GAME OVER.");
            Console.ResetColor();

            Console.WriteLine("Press any key to exit the game");
            Console.ReadKey(true);
        }
    }
}