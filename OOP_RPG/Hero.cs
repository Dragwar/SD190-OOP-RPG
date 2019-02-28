using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Hero
    {
        public string Name { get; set; }
        public int Strength { get; private set; }
        public int Defense { get; private set; }
        public int OriginalHP { get; private set; }
        public int CurrentHP { get; private set; }
        public int ExperiencePoints { get; private set; }
        public int GoldCoins { get; private set; }
        public Weapon EquippedWeapon { get; private set; }
        public Armor EquippedArmor { get; private set; }
        public Shield EquippedShield { get; private set; }
        public List<Armor> ArmorBag { get; private set; }
        public List<Weapon> WeaponsBag { get; private set; }
        public List<HealthPotion> HealthPotionBag { get; private set; }
        public List<Shield> ShieldBag { get; private set; }
        public HandleAchievements ManageAchievements { get; private set; }

        public Hero(HandleAchievements manageAchievements)
        {
            ArmorBag = new List<Armor>();
            WeaponsBag = new List<Weapon>();
            ShieldBag = new List<Shield>();
            HealthPotionBag = new List<HealthPotion>();
            Strength = 5;
            Defense = 5;
            OriginalHP = 20;
            CurrentHP = 20;
            GoldCoins = 25;
            ExperiencePoints = 10;
            ManageAchievements = manageAchievements;
        }



        /*
        ======================================================================================== 
        GetMasterInventoryList ---> Gets a list that contains all the items that the hero owns
        ======================================================================================== 
        */
        public List<IBuyableItem> GetMasterInventoryList()
        {
            List<IBuyableItem> masterList = new List<IBuyableItem>();
            masterList.AddRange(WeaponsBag);
            masterList.AddRange(ArmorBag);
            masterList.AddRange(ShieldBag);
            masterList.AddRange(HealthPotionBag);
            return masterList;
        }



        /*
        ======================================================================================== 
        ShowStats ---> Simple method prints all the current stat values
        ======================================================================================== 
        */
        public void ShowStats(bool showAchievements)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n***** {Name} *****");
            Console.ResetColor();

            string addedDefense = "";
            if (EquippedArmor != null && EquippedShield != null)
            {
                addedDefense = $"Defense: {Defense} (+ {EquippedArmor.Defense + EquippedShield.Defense})";
            }
            else if (EquippedArmor != null && EquippedShield == null)
            {
                addedDefense = $"Defense: {Defense} (+ {EquippedArmor.Defense})";
            }
            else if (EquippedShield != null && EquippedArmor == null)
            {
                addedDefense = $"Defense: {Defense} (+ {EquippedShield.Defense})";
            }
            else if (EquippedShield == null && EquippedArmor == null)
            {
                addedDefense = $"Defense: {Defense}";
            }
            else
            {
                throw new NotImplementedException("new defense item not implemented yet");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Strength: {Strength} {(EquippedWeapon != null ? $"(+ {EquippedWeapon.Strength})" : "")}");
            Console.WriteLine(addedDefense);
            Console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            Console.WriteLine($"Gold Coins: {GoldCoins}");
            Console.WriteLine($"Experience Points: {ExperiencePoints}");
            Console.WriteLine($"Achievement Points: {HandleAchievements.TotalPoints}");
            Console.ResetColor();

            if (showAchievements)
            {
                Console.WriteLine($"Achievements:");
                ManageAchievements.PrintAllAchievements();
            }
        }



        /*
        ======================================================================================== 
        ShowInventory ---> Simple method prints all the items that the user has
        ======================================================================================== 
        */
        public void ShowInventory()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n***** INVENTORY ******");
            Console.ResetColor();

            ShowInventoryWeapons();
            ShowInventoryArmor();
            ShowInventoryShield();
            ShowInventoryHealthPotions();
        }



        /*
        ======================================================================================== 
        ShowInventoryWeapons ---> Simple method that prints each weapon item that the hero has
        ======================================================================================== 
        */
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



        /*
        ======================================================================================== 
        ShowInventoryArmor ---> Simple method that prints each armor item that the hero has
        ======================================================================================== 
        */
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



        /*
        ======================================================================================== 
        ShowInventoryShield ---> Simple method that prints each shield item that the hero has
        ======================================================================================== 
        */
        public void ShowInventoryShield()
        {
            Console.WriteLine("Shields: ");

            if (ShieldBag.Any())
            {
                foreach (Shield shield in ShieldBag)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    string equippedMessage = "";

                    if (shield.IsEquipped)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    Console.WriteLine($"============({shield.Name})============");
                    Console.WriteLine($"Worth: {shield.SellingPrice} Gold Coins");
                    Console.WriteLine($"Defense: (+ {shield.Defense})");
                    Console.WriteLine(equippedMessage);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have No Shields . . .");
            }
            Console.ResetColor();
        }



        /*
        ======================================================================================== 
        ShowInventoryHealthPotions ---> Simple method that prints each potion that the hero has
        ======================================================================================== 
        */
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



        /*
        ======================================================================================== 
        ManageInventory ---> Shows INV and Handles user choosing to equip and use potions
        ======================================================================================== 
        */
        public void ManageInventory()
        {
            string userInput = "0";
            string successMessage = "";
            string errorMessage = "";

            while (userInput != "6")
            {
                Console.Title = $"{Name}'s Inventory | Stats: [> Str: {Strength} | Def: {Defense} | HP: {CurrentHP}/{OriginalHP} <]";
                ShowInventory();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(successMessage);
                Console.ResetColor();
                successMessage = "";

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
                errorMessage = "";

                Console.WriteLine("1. equip a Weapon.");
                Console.WriteLine("2. equip Armor.");
                Console.WriteLine("3. equip Shield.");
                Console.WriteLine("4. unequip all items.");
                Console.WriteLine("5. use Health Potion.");
                Console.WriteLine("6. exit\n");

                userInput = Console.ReadLine().Trim();

                if (userInput == "1")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******* Unequipped Weapons *******");
                    Console.ResetColor();

                    List<Weapon> weapons = WeaponsBag.ToList();
                    if (weapons.Any())
                    {
                        for (int i = 1; i < weapons.Count + 1; i++)
                        {
                            if (weapons[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength}) Strength (Already Equipped)");
                                Console.ResetColor();

                            }
                            else
                            {
                                Console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength}) Strength");
                            }
                        }

                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || (userIndex < 0 || userIndex >= weapons.Count))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nothing was equipped because of one of the following errors:");
                            Console.WriteLine("- did not input a number");
                            Console.WriteLine("- inputted number was too small");
                            Console.WriteLine("- inputted number was too big");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            EquipWeapon(userIndex);

                            successMessage = $"You equipped your {EquippedWeapon.Name}!";
                        }
                    }
                    else
                    {
                        errorMessage = "You have nothing to equip . . .";
                    }
                }
                else if (userInput == "2")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******* Unequipped Armor *******");
                    Console.ResetColor();

                    List<Armor> armor = ArmorBag.ToList();

                    if (armor.Any())
                    {
                        for (int i = 1; i < armor.Count + 1; i++)
                        {
                            if (armor[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {armor[i - 1].Name} --> (+ {armor[i - 1].Defense}) Defense (Already Equipped)");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"{i}. {armor[i - 1].Name} --> (+ {armor[i - 1].Defense}) Defense");
                            }
                        }

                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || (userIndex < 0 || userIndex >= armor.Count))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nothing was equipped because of one of the following errors:");
                            Console.WriteLine("- did not input a number");
                            Console.WriteLine("- inputted number was too small");
                            Console.WriteLine("- inputted number was too big");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            EquipArmor(userIndex);

                            successMessage = $"You equipped your {EquippedArmor.Name}!";
                        }
                    }
                    else
                    {
                        errorMessage = "You have nothing to equip . . .";
                    }
                }
                else if (userInput == "3")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******* Unequipped Shields *******");
                    Console.ResetColor();

                    List<Shield> shields = ShieldBag.ToList();

                    if (shields.Any())
                    {
                        for (int i = 1; i < shields.Count + 1; i++)
                        {
                            if (shields[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {shields[i - 1].Name} --> (+ {shields[i - 1].Defense}) Defense (Already Equipped)");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"{i}. {shields[i - 1].Name} --> (+ {shields[i - 1].Defense}) Defense");
                            }
                        }

                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || (userIndex < 0 || userIndex >= shields.Count))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nothing was equipped because of one of the following errors:");
                            Console.WriteLine("- did not input a number");
                            Console.WriteLine("- inputted number was too small");
                            Console.WriteLine("- inputted number was too big");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            EquipShield(userIndex);

                            successMessage = $"You equipped your {EquippedShield.Name}!";
                        }
                    }
                    else
                    {
                        errorMessage = "You have nothing to equip . . .";
                    }
                }
                else if (userInput == "4")
                {
                    UnEquipItem(EquippedWeapon);
                    UnEquipItem(EquippedArmor);
                    UnEquipItem(EquippedShield);
                }
                else if (userInput == "5")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******* Your Health Potions *******");
                    Console.ResetColor();

                    List<HealthPotion> healthPotions = HealthPotionBag.ToList();

                    if (healthPotions.Any())
                    {
                        for (int i = 1; i < healthPotions.Count + 1; i++)
                        {
                            Console.WriteLine($"{i}. {healthPotions[i - 1].Name} --> (+ {healthPotions[i - 1].HealAmount} HP)");
                        }

                        bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || (userIndex < 0 || userIndex >= healthPotions.Count))
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
                            if (CurrentHP >= OriginalHP)
                            {
                                errorMessage = "Sorry you can't heal past you Original HP\n";
                            }
                            else
                            {
                                successMessage = $"You used your {healthPotions[userIndex].Name}!";

                                UseHealthPotion(userIndex);
                            }
                        }
                    }
                    else
                    {
                        errorMessage = "You have nothing to use . . .";
                    }
                }
            }
        }



        /*
        ======================================================================================== 
        EquipWeapon ---> Simple method to equip a weapon via index
        ======================================================================================== 
        */
        public void EquipWeapon(int weaponIndex)
        {
            if (WeaponsBag.Any())
            {
                UnEquipItem(EquippedWeapon);

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



        /*
        ======================================================================================== 
        EquipArmor ---> Simple method to equip a armor via index
        ======================================================================================== 
        */
        public void EquipArmor(int armorIndex)
        {
            if (ArmorBag.Any())
            {
                UnEquipItem(EquippedArmor);

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



        /*
        ======================================================================================== 
        EquipShield ---> Simple method to equip a shield via index
        ======================================================================================== 
        */
        public void EquipShield(int shieldIndex)
        {
            if (ShieldBag.Any())
            {
                UnEquipItem(EquippedShield);

                EquippedShield = ShieldBag[shieldIndex];
                ShieldBag[shieldIndex].IsEquipped = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou don't have any shield to equip!");
                Console.ResetColor();
            }
        }



        /*
        ======================================================================================== 
        UnEquipItem ---> Simple method to unequip a armor/weapon depending on what is passed in
        ======================================================================================== 
        */
        public void UnEquipItem(IBuyableItem item)
        {
            if (item is Armor)
            {
                if (EquippedArmor != null && item.ItemId == EquippedArmor.ItemId)
                {
                    EquippedArmor.IsEquipped = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{EquippedArmor.Name} was unequipped!");
                    Console.ResetColor();
                    EquippedArmor = null;
                }
            }
            else if (item is Weapon)
            {
                if (EquippedWeapon != null && item.ItemId == EquippedWeapon.ItemId)
                {
                    EquippedWeapon.IsEquipped = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{EquippedWeapon.Name} was unequipped!");
                    Console.ResetColor();
                    EquippedWeapon = null;
                }
            }
            else if (item is Shield)
            {
                if (EquippedShield != null && item.ItemId == EquippedShield.ItemId)
                {
                    EquippedShield.IsEquipped = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{EquippedShield.Name} was unequipped!");
                    Console.ResetColor();
                    EquippedShield = null;
                }
            }
        }



        /*
        ======================================================================================== 
        AddExperiencePoints ---> Simple method to add exp to total exp
        ======================================================================================== 
        */
        public void AddExperiencePoints(int numberOfPoints)
        {
            if (numberOfPoints < 0)
            {
                throw new Exception("You can't add an int that is less than zero (exp error)");
            }
            ExperiencePoints += numberOfPoints;
        }



        /*
        ======================================================================================== 
        RemoveExperiencePoints ---> Simple method to remove exp from total exp
        ======================================================================================== 
        */
        public void RemoveExperiencePoints(int numberOfPoints)
        {
            if (numberOfPoints > ExperiencePoints)
            {
                throw new Exception("You can't spend more than what you have (exp error)");
            }
            ExperiencePoints -= numberOfPoints;
        }



        /*
        ======================================================================================== 
        AddGoldCoins ---> Simple method to add coins to total coins
        ======================================================================================== 
        */
        public void AddGoldCoins(int numberOfCoins)
        {
            if (numberOfCoins < 0)
            {
                throw new Exception("You can't add an int that is less than zero (gold coin error)");
            }
            GoldCoins += numberOfCoins;
        }



        /*
        ======================================================================================== 
        RemoveGoldCoins ---> Simple Method to remove gold coins from total coins
        ======================================================================================== 
        */
        public void RemoveGoldCoins(int numberOfCoins)
        {
            if (numberOfCoins > GoldCoins)
            {
                throw new Exception("You can't spend more than what you have (gold coin error)");
            }
            GoldCoins -= numberOfCoins;
        }



        /*
        ======================================================================================== 
        TakeDamage ---> Simple Method to remove CurrentHP from the hero
        ======================================================================================== 
        */
        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new Exception("You can't take negative damage (damage error)");
            }
            CurrentHP -= damage;
        }



        /*
        ======================================================================================== 
        UseHealthPotion ---> Selects Health potion via index and heals then removes it from bag
        ======================================================================================== 
        */
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
                Console.WriteLine("\nYou don't have any health potion to use!");
                Console.ResetColor();
            }
        }



        /*
        ======================================================================================== 
        LevelUp ---> Adds the inputed number to level up the passed in stat
        ======================================================================================== 
        */
        public int LevelUp(int heroStatValue)
        {
            bool isNumber = int.TryParse(Console.ReadLine().Trim(), out int levelAmount);

            if (isNumber && levelAmount <= ExperiencePoints)
            {
                heroStatValue += levelAmount;
                RemoveExperiencePoints(levelAmount);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nothing Leveled Up (input wasn't a int or input was greater than current exp)\n");
                Console.ResetColor();
            }
            return heroStatValue;
        }



        /*
        ======================================================================================== 
        SpendExperiencePoints ---> Allows Player To Level Up Their Character's Stats
        ======================================================================================== 
        */
        public void SpendExperiencePoints(string userInput)
        {
            if (userInput == "1")
            {
                Console.WriteLine("================[Level Up Strength]================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Current Experience Points: {ExperiencePoints}");
                Console.WriteLine($"Current Strength: {Strength}");
                Console.ResetColor();

                Console.WriteLine("Level Up Strength by:\n");
                Strength = LevelUp(Strength);
            }
            else if (userInput == "2")
            {
                Console.WriteLine("================[Level Up Defense]================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Current Experience Points: {ExperiencePoints}");
                Console.WriteLine($"Current Defense: {Defense}");
                Console.ResetColor();

                Console.WriteLine("Level Up Defense by:\n");
                Defense = LevelUp(Defense);
            }
            else if (userInput == "3")
            {
                Console.WriteLine("================[Level Up Original HP]================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Current Experience Points: {ExperiencePoints}");
                Console.WriteLine($"Current OriginalHP: {OriginalHP}");
                Console.ResetColor();

                Console.WriteLine("Level Up HP by:\n");
                OriginalHP = LevelUp(OriginalHP);
            }
            ShowStats(false);
            Console.WriteLine("\n");
        }


    }
}