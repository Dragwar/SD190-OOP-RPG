using OOP_RPG.Models.Enumerations;
using OOP_RPG.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.Models
{
    public class Fight
    {
        private readonly IConsole _console;
        private IAchievementManager ManageAchievements { get; }
        private List<IMonster> Monsters { get; }
        private IHero Hero { get; }
        private IMonster CurrentMonster { get; }
        private int MonstersEXPWorth { get; }
        private int MonstersGoldCoinWorth { get; }

        // Not Implemented yet
        // private int TotalHeroPoints { get; }

        /*
        ======================================================================================== 
        Fight ---> Initializes the fight and selects a random monster from today's monsters
        ======================================================================================== 
        */
        public Fight(IConsole console, IAchievementManager manageAchievements, IHero hero)
        {
            _console = console;
            Hero = hero;
            ManageAchievements = manageAchievements;

            // Not Implemented yet
            // TODO: use this to up the difficulty for the monsters
            // TotalHeroPoints = Hero.OriginalHP + Hero.Strength + Hero.Defense;

            Monsters = new List<IMonster>(GetTodaysMonsters());

            CurrentMonster = Monsters[RNG.Next(0, Monsters.Count)];

            MonstersEXPWorth = CurrentMonster.GetMonstersEXPWorth();
            MonstersGoldCoinWorth = CurrentMonster.GetMonstersGoldCoinWorth();
        }



        /*
        ======================================================================================== 
        GetTodaysMonsters ---> Gets all the monsters and only returns today's monsters
        ======================================================================================== 
        */
        public static List<IMonster> GetTodaysMonsters()
        {
            var allMonsters = new List<IMonster>(WeekDayMonsterManager.InitialMonsters);

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
            _console.TextColor = ConsoleColor.Cyan;
            _console.WriteLine($"\nA {CurrentMonster.Name}! (Strength = {CurrentMonster.Strength} | Defense = {CurrentMonster.Defense} | HP = {CurrentMonster.CurrentHP})");
            _console.ResetColor();

            while (CurrentMonster.CurrentHP > 0 && Hero.CurrentHP > 0)
            {
                _console.Title = $"FIGHT!!! ({Hero.Name} vs {CurrentMonster.Name}) Stats: [> Str: {Hero.Strength} | Def: {Hero.Defense} | HP: {Hero.CurrentHP}/{Hero.OriginalHP} <] | Enemy Current HP: {CurrentMonster.CurrentHP}";
                _console.WriteLine($"\nWhat will you do?");
                _console.WriteLine("1. Fight");
                _console.WriteLine("2. Use Health Potion");
                _console.WriteLine("3. Flee");
                _console.WriteLine("4. See The Enemy's Status and Your Status");

                var input = _console.ReadLine().Trim();

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

            bool hasFled;
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
                _console.TextColor = ConsoleColor.Red;
                _console.WriteLine("You have failed to flee the battle");
                _console.ResetColor();
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
            _console.Clear();

            _console.TextColor = ConsoleColor.Yellow;
            _console.WriteLine("******* Your Health Potions *******");
            _console.ResetColor();

            var healthPotions = Hero.Bag.OfType<IHealthPotion>().ToArray();

            if (healthPotions.Any())
            {
                for (var i = 1; i < healthPotions.Length + 1; i++)
                {
                    _console.WriteLine($"{i}. {healthPotions[i - 1].Name} --> (+ {healthPotions[i - 1].HealAmount} HP)");
                }

                var isNumber = int.TryParse(_console.ReadLine().Trim(), out var userIndex);

                // account for index offset of 1
                userIndex--;

                if (!isNumber || userIndex < 0 || userIndex >= healthPotions.Length)
                {
                    _console.TextColor = ConsoleColor.Red;
                    _console.WriteLine("Nothing was used because of one of the following errors:");
                    _console.WriteLine("- did not input a number");
                    _console.WriteLine("- inputted number was too small");
                    _console.WriteLine("- inputted number was too big");
                    _console.ResetColor();
                }
                else
                {
                    if (Hero.CurrentHP >= Hero.OriginalHP)
                    {
                        _console.TextColor = ConsoleColor.Red;
                        _console.WriteLine("Sorry you can't heal past you Original HP\n");
                        _console.ResetColor();
                    }
                    else
                    {
                        _console.TextColor = ConsoleColor.Yellow;
                        _console.WriteLine($"You used your {healthPotions[userIndex].Name}!");
                        _console.ResetColor();

                        Hero.UseHealthPotion(healthPotions[userIndex]);
                    }
                }
            }
            else
            {
                _console.TextColor = ConsoleColor.Red;
                _console.WriteLine("You have nothing to use. . .");
                _console.ResetColor();
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

            if (Hero.EquippedItems.SingleOrDefault(item => item is IWeapon) is IWeapon foundWeapon)
            {
                var weaponDamage = RNG.Next(foundWeapon.Strength.MinValue, foundWeapon.Strength.MaxValue + 1);
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

            _console.TextColor = ConsoleColor.Blue;
            _console.WriteLine($"\nYou did {damage} damage!");
            _console.WriteLine($"Monster's HP: {CurrentMonster.CurrentHP}/{CurrentMonster.OriginalHP}");
            _console.ResetColor();

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

            var armor = Hero.EquippedItems.SingleOrDefault(item => item is IArmor) as IArmor;
            var shield = Hero.EquippedItems.SingleOrDefault(item => item is IShield) as IShield;

            if (armor != null && shield != null)
            {
                var armorDefense = RNG.Next(armor.Defense.MinValue, armor.Defense.MaxValue + 1);
                var shieldDefense = RNG.Next(shield.Defense.MinValue, shield.Defense.MaxValue + 1);
                var finalDefense = shieldDefense + armorDefense + Hero.Defense;
                compare = CurrentMonster.Strength - finalDefense;
            }
            else if (armor != null)
            {
                var armorDefense = RNG.Next(armor.Defense.MinValue, armor.Defense.MaxValue + 1);
                var finalDefense = armorDefense + Hero.Defense;
                compare = CurrentMonster.Strength - finalDefense;
            }
            else if (shield != null)
            {
                var shieldDefense = RNG.Next(shield.Defense.MinValue, shield.Defense.MaxValue + 1);
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

            _console.TextColor = ConsoleColor.Red;
            _console.WriteLine($"\n{CurrentMonster.Name} does {damage} damage!");
            _console.WriteLine($"{Hero.Name}'s HP: {Hero.CurrentHP}/{Hero.OriginalHP}");
            _console.ResetColor();

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

                _console.TextColor = ConsoleColor.Yellow;
                _console.WriteLine($"{CurrentMonster.Name} has been defeated! You win the battle!");
                _console.WriteLine($"(+ {MonstersGoldCoinWorth} Gold Coins)");
                _console.WriteLine($"(+ {MonstersEXPWorth} EXP)");
                _console.ResetColor();
                ManageAchievements.AddDeadMonster(CurrentMonster);
            }
            else if (howHeroWon == WinConditionEnum.Flee)
            {
                _console.TextColor = ConsoleColor.Yellow;
                _console.WriteLine($"You have successfully fled the battle!");
                _console.ResetColor();
            }
            Hero.ShowStats(false);

            _console.Title = $"Main Menu";
        }



        /*
        ======================================================================================== 
        Lose ---> Lose Message and exits the game
        ======================================================================================== 
        */
        private void Lose()
        {
            _console.Title = $"Better Luck Next Time.";

            _console.TextColor = ConsoleColor.DarkRed;
            _console.WriteLine("You've been defeated! :( GAME OVER.");
            _console.ResetColor();

            _console.WriteLine("Press any key to exit the game");
            _console.ReadKey(true);
        }
    }
}