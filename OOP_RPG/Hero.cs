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
        public List<Armor> ArmorBag { get; set; }
        public List<Weapon> WeaponsBag { get; set; }
        public List<HealthPotion> HealthPotionBag { get; set; }

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
            ArmorBag = new List<Armor>();
            WeaponsBag = new List<Weapon>();
            HealthPotionBag = new List<HealthPotion>();
            Strength = 10;
            Defense = 10;
            OriginalHP = 30;
            CurrentHP = 30;
            GoldCoins = 300;
            ExperiencePoints = 50;
        }

        // These are the Methods of our Class.
        public void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n***** {Name} *****");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Strength: {Strength} {(EquippedWeapon != null ? $"(+ {EquippedWeapon.Strength})" : "")}");
            Console.WriteLine($"Defense: {Defense} {(EquippedArmor != null ? $"(+ {EquippedArmor.Defense})" : "")}");
            Console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            Console.WriteLine($"Gold Coins: {GoldCoins}");
            Console.WriteLine($"Experience Points: {ExperiencePoints}");
            Console.ResetColor();
        }



        public void ShowInventory()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n***** INVENTORY ******");
            Console.ResetColor();

            ShowInventoryWeapons();
            ShowInventoryArmor();
            ShowInventoryHealthPotions();
        }

        public void ShowInventoryWeapons()
        {
            Console.WriteLine("Weapons: ");

            if (WeaponsBag.Any())
            {
                foreach (Weapon weapon in WeaponsBag)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    string equippedMessage = "";

                    if (weapon.IsEquipped)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    Console.WriteLine($"============({weapon.Name})============");
                    Console.WriteLine($"Worth: {weapon.SellingPrice} Gold Coins");
                    Console.WriteLine($"Strength: (+ {weapon.Strength})");
                    Console.WriteLine(equippedMessage);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have No Weapons . . .");
            }
            Console.ResetColor();
        }

        public void ShowInventoryArmor()
        {
            Console.WriteLine("Armor: ");

            if (ArmorBag.Any())
            {
                foreach (Armor armor in ArmorBag)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    string equippedMessage = "";

                    if (armor.IsEquipped)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    Console.WriteLine($"============({armor.Name})============");
                    Console.WriteLine($"Worth: {armor.SellingPrice} Gold Coins");
                    Console.WriteLine($"Defense: (+ {armor.Defense})");
                    Console.WriteLine(equippedMessage);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have No Armor . . .");
            }
            Console.ResetColor();
        }

        public void ShowInventoryHealthPotions()
        {
            Console.WriteLine("HealthPotions: ");

            if (HealthPotionBag.Any())
            {
                foreach (HealthPotion healthPotion in HealthPotionBag)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"============({healthPotion.Name})============");
                    Console.WriteLine($"Worth: {healthPotion.SellingPrice} Gold Coins");
                    Console.WriteLine($"Heal Amount: (+ {healthPotion.HealAmount} HP)");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have No Health Potions . . .");
            }
            Console.ResetColor();
        }



        public void EquipWeapon(int weaponIndex)
        {
            if (WeaponsBag.Any())
            {
                UnEquipWeapon(EquippedWeapon);

                EquippedWeapon = WeaponsBag[weaponIndex];
                WeaponsBag[weaponIndex].IsEquipped = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou don't have any weapons to equip!");
                Console.ResetColor();
            }
        }

        public void EquipArmor(int armorIndex)
        {
            if (ArmorBag.Any())
            {
                UnEquipArmor(EquippedArmor);

                EquippedArmor = ArmorBag[armorIndex];
                ArmorBag[armorIndex].IsEquipped = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou don't have any armor to equip!");
                Console.ResetColor();
            }
        }



        public void UnEquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                EquippedArmor.IsEquipped = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{EquippedArmor} was unequipped!");
                Console.ResetColor();
            }
        }

        public void UnEquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                EquippedWeapon.IsEquipped = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{EquippedWeapon} was unequipped!");
                Console.ResetColor();
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


        public void UseHealthPotion(int healthPotionIndex)
        {
            if (HealthPotionBag.Any())
            {
                CurrentHP += HealthPotionBag[healthPotionIndex].HealAmount;

                // Allows user to heal to max hp but not allow to go above original hp
                // example: curHP: 25 oriHP: 30 --> I was a health potion that heals for 7
                // then my curHP would go to 30 and not past it.
                CurrentHP = CurrentHP > OriginalHP ? OriginalHP : CurrentHP;

                Console.WriteLine($"You used {HealthPotionBag[healthPotionIndex].Name}");
                Console.WriteLine($"Your HP: {CurrentHP}/{OriginalHP}");
                HealthPotionBag.RemoveAt(healthPotionIndex);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou don't have any armor to equip!");
                Console.ResetColor();
            }
        }


    }
}