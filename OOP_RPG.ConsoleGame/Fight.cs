using OOP_RPG.ConsoleGame.Utilities;
using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.ConsoleGame
{
    public class Fight
    {
        private HandleAchievements ManageAchievements { get; }
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
        public Fight(HandleAchievements manageAchievements, Hero hero)
        {
            Hero = hero;
            ManageAchievements = manageAchievements;

            // Not Implemented yet
            // TODO: use this to up the difficulty for the monsters
            // TotalHeroPoints = Hero.OriginalHP + Hero.Strength + Hero.Defense;

            Monsters = new List<Monster>(GetTodaysMonsters());

            CurrentMonster = Monsters[RNG.Next(0, Monsters.Count)];

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
            var allMonsters = new List<Monster>(WeekDayMonsters.InitialMonsters);

            var todaysMonsters = allMonsters
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
                Console.Title = $"FIGHT!!! ({Hero.Name} vs {CurrentMonster.Name}) Stats: [> Str: {Hero.Strength} | Def: {Hero.Defense} | HP: {Hero.CurrentHP}/{Hero.OriginalHP} <] | Enemy Current HP: {CurrentMonster.CurrentHP}";
                Console.WriteLine($"\nWhat will you do?");
                Console.WriteLine("1. Fight");
                Console.WriteLine("2. Use Health Potion");
                Console.WriteLine("3. Flee");
                Console.WriteLine("4. See The Enemy's Status and Your Status");

                var input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    HeroTurn();
                }
                else if (input == "2")
                {
                    UseHealthPotion();
                }
                else if (input == "3")
                {
                    Flee();
                }
                else if (input == "4")
                {
                    Hero.ShowStats(false);
                    CurrentMonster.ShowStats();
                }
            }
        }



        /*
        ======================================================================================== 
        Flee ---> Try to run from monster (if fails to flee hero is still in the fight)
        ======================================================================================== 
        */
        private void Flee()
        {
            var randNum = RNG.Next(0, 100);
            var chance = CurrentMonster.GetRunAwayChance(CurrentMonster);
            var hasFled = false;

            if (randNum <= chance)
            {
                hasFled = true;
            }
            else if (randNum > chance)
            {
                hasFled = false;
            }
            else
            {
                throw new Exception("Oof");
            }

            if (hasFled)
            {
                CurrentMonster.CurrentHP -= CurrentMonster.CurrentHP; // This will skip past the while loop in Start()
                Win(WinConditionEnum.Flee);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have failed to flee the battle");
                Console.ResetColor();
                MonsterTurn();
            }
        }



        /*
        ======================================================================================== 
        HeroTurn ---> Calculate the damage that will be dealt to the currentMonster
        ======================================================================================== 
        */
        private void UseHealthPotion()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******* Your Health Potions *******");
            Console.ResetColor();

            var healthPotions = Hero.HealthPotionBag.ToList();

            if (healthPotions.Any())
            {
                for (var i = 1; i < healthPotions.Count + 1; i++)
                {
                    Console.WriteLine($"{i}. {healthPotions[i - 1].Name} --> (+ {healthPotions[i - 1].HealAmount} HP)");
                }

                var isNumber = int.TryParse(Console.ReadLine().Trim(), out var userIndex);

                // account for index offset of 1
                userIndex--;

                if (!isNumber || userIndex < 0 || userIndex >= healthPotions.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nothing was used because of one of the following errors:");
                    Console.WriteLine("- did not input a number");
                    Console.WriteLine("- inputted number was too small");
                    Console.WriteLine("- inputted number was too big");
                    Console.ResetColor();
                    return;
                }
                else
                {
                    if (Hero.CurrentHP >= Hero.OriginalHP)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sorry you can't heal past you Original HP\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"You used your {healthPotions[userIndex].Name}!");
                        Console.ResetColor();

                        Hero.UseHealthPotion(userIndex);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have nothing to use. . .");
                Console.ResetColor();
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
            var compare = Hero.Strength - CurrentMonster.Defense;

            if (Hero.EquippedWeapon != null)
            {
                Weapon weapon = Hero.EquippedWeapon;
                var weaponDamage = RNG.Next(weapon.MinDamage, weapon.MaxDamage + 1);
                var finalDamage = Hero.Strength + weaponDamage;
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
                Win(WinConditionEnum.Kill);
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
            var compare = CurrentMonster.Strength - Hero.Defense;

            if (Hero.EquippedArmor != null && Hero.EquippedShield != null)
            {
                Armor armor = Hero.EquippedArmor;
                Shield shield = Hero.EquippedShield;
                var armorDefense = RNG.Next(armor.MinDefense, armor.MaxDefense + 1);
                var shieldDefense = RNG.Next(shield.MinDefense, shield.MaxDefense + 1);
                var finalDefense = shieldDefense + armorDefense + Hero.Defense;
                compare = CurrentMonster.Strength - finalDefense;
            }
            else if (Hero.EquippedArmor != null)
            {
                Armor armor = Hero.EquippedArmor;
                var armorDefense = RNG.Next(armor.MinDefense, armor.MaxDefense + 1);
                var finalDefense = armorDefense + Hero.Defense;
                compare = CurrentMonster.Strength - finalDefense;
            }
            else if (Hero.EquippedShield != null)
            {
                Shield shield = Hero.EquippedShield;
                var shieldDefense = RNG.Next(shield.MinDefense, shield.MaxDefense + 1);
                var finalDefense = shieldDefense + Hero.Defense;
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
        private void Win(WinConditionEnum howHeroWon)
        {
            if (howHeroWon == WinConditionEnum.Kill)
            {
                Hero.AddExperiencePoints(MonstersEXPWorth);
                Hero.AddGoldCoins(MonstersGoldCoinWorth);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{CurrentMonster.Name} has been defeated! You win the battle!");
                Console.WriteLine($"(+ {MonstersGoldCoinWorth} Gold Coins)");
                Console.WriteLine($"(+ {MonstersEXPWorth} EXP)");
                Console.ResetColor();
                ManageAchievements.AddDeadMonster(CurrentMonster);
            }
            else if (howHeroWon == WinConditionEnum.Flee)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"You have successfully fled the battle!");
                Console.ResetColor();
            }
            Hero.ShowStats(false);

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