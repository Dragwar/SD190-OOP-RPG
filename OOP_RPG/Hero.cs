using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Hero
    {
        // These are the Properties of our Class.
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHP { get; set; }
        public int CurrentHP { get; set; }
        public int ExperiencePoints { get; private set; }
        public int GoldCoins { get; private set; }
        public Weapon EquippedWeapon { get; private set; }
        public Armor EquippedArmor { get; private set; }
        public List<Armor> ArmorsBag { get; set; }
        public List<Weapon> WeaponsBag { get; set; }

        /*
            This is a Constructor.
            When we create a new object from our Hero class, the instance of this class, our hero, has:
            an empty List that has to contain instances of the Armor class,
            an empty List that has to contain instance of the Weapon class,
            stats of the "int" data type, including an initial strength and defense,
            original hit-points that are going to be the same as the current hit-points.
        */
        public Hero()
        {
            ArmorsBag = new List<Armor>();
            WeaponsBag = new List<Weapon>();
            Strength = 10;
            Defense = 10;
            OriginalHP = 30;
            CurrentHP = 30;
            GoldCoins = 300;
            ExperiencePoints = 10;
        }



        // These are the Methods of our Class.
        public void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n***** {Name} *****");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Strength: {Strength} ({(EquippedWeapon != null ? $"+ {EquippedWeapon.Strength}" : "+ 0")})");
            Console.WriteLine($"Defense: {Defense} ({(EquippedArmor != null ? $"+ {EquippedArmor.Defense}" : "+ 0")})");
            Console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            Console.WriteLine($"Gold Coins: {GoldCoins}");
            Console.WriteLine($"Experience Points: {ExperiencePoints}");
            Console.ResetColor();
        }



        public void ShowInventory()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n***** INVENTORY ******");

            Console.WriteLine("Weapons: ");
            Console.ResetColor();

            if (WeaponsBag.Any())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Weapon weapon in WeaponsBag)
                {
                    Console.WriteLine($"============({weapon.Name})============");
                    Console.WriteLine($"Worth: - {weapon.Price} Gold Coins");
                    Console.WriteLine($"+Strength: - {weapon.Strength}\n");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have No Weapons . . .");
            }
            Console.ResetColor();


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Armor: ");
            Console.ResetColor();

            if (ArmorsBag.Any())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Armor armor in ArmorsBag)
                {
                    Console.WriteLine($"============({armor.Name})============");
                    Console.WriteLine($"Worth: - {armor.Price} Gold Coins");
                    Console.WriteLine($"+Defense: - {armor.Defense}\n");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have No Armor . . .");
            }
            Console.ResetColor();
        }



        public void EquipWeapon()
        {
            if (WeaponsBag.Any())
            {
                EquippedWeapon = WeaponsBag[0];
            }
        }

        public void EquipArmor()
        {
            if (ArmorsBag.Any())
            {
                EquippedArmor = ArmorsBag[0];
            }
        }



        public void AddExperiencePoints(int numberOfPoints)
        {
            if (numberOfPoints < 0)
            {
                throw new Exception("You can't add an int that is less than zero (exp error)");
            }
            ExperiencePoints += numberOfPoints;
        }

        public void RemoveExperiencePoints(int numberOfPoints)
        {
            if (numberOfPoints > ExperiencePoints)
            {
                throw new Exception("You can't spend more than what you have (exp error)");
            }
            ExperiencePoints -= numberOfPoints;
        }



        public void AddGoldCoins(int numberOfCoins)
        {
            if (numberOfCoins < 0)
            {
                throw new Exception("You can't add an int that is less than zero (gold coin error)");
            }
            GoldCoins += numberOfCoins;
        }

        public void RemoveGoldCoins(int numberOfCoins)
        {
            if (numberOfCoins > GoldCoins)
            {
                throw new Exception("You can't spend more than what you have (gold coin error)");
            }
            GoldCoins -= numberOfCoins;
        }





    }
}