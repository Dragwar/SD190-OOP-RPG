using OOP_RPG.Models;
using OOP_RPG.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.ConsoleGame
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
        public ItemInventory<IEquippableItem> EquippedItems { get; }
        public ItemInventory<IItem> Bag { get; }
        //public HandleAchievements ManageAchievements { get; private set; }
        //public int AchievementPoints { get => ManageAchievements.TotalPoints; }

        public Hero(/*HandleAchievements manageAchievements*/)
        {
            Strength = 5;
            Defense = 5;
            OriginalHP = 20;
            CurrentHP = 20;
            GoldCoins = 25;
            ExperiencePoints = 10;
            //ManageAchievements = manageAchievements;

            Bag = new ItemInventory<IItem>();
            Bag.OnItemAdd += Bag_OnItemAdd;
            Bag.OnItemRemove += Bag_OnItemRemove;

            EquippedItems = new ItemInventory<IEquippableItem>();
            EquippedItems.OnItemAdd += EquippedItems_OnItemAdd;
            EquippedItems.OnItemRemove += EquippedItems_OnItemRemove;
        }

        private void Bag_OnItemAdd(object sender, IItem e)
        {
        }

        private void Bag_OnItemRemove(object sender, IItem e)
        {
        }

        private void EquippedItems_OnItemAdd(object sender, IEquippableItem e)
        {
            e.IsEquipped = true;
            Bag.Remove(e);
        }

        private void EquippedItems_OnItemRemove(object sender, IEquippableItem e)
        {
            e.IsEquipped = false;
            Bag.Add(e);
        }

        public bool IsItemEquipped(IEquippableItem equippableItem) => EquippedItems.Contains(equippableItem);

        public bool IsItemInBag(IItem item) => Bag.Contains(item);




        /*
        ======================================================================================== 
        ShowStats ---> Simple method prints all the current stat values
        ======================================================================================== 
        */
        public void ShowStats(/*bool showAchievements*/)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n***** {Name} *****");
            Console.ResetColor();

            var strengthItems = EquippedItems.OfType<IStrengthItem>().ToArray();
            var defenseItems = EquippedItems.OfType<IDefenseItem>().ToArray();


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Strength: {Strength} {(strengthItems.Any() ? $"(+ {strengthItems.Sum(s => s.Strength.BaseValue)})" : "")}");
            Console.WriteLine($"Defense: {Defense} {(defenseItems.Any() ? $"(+ {defenseItems.Sum(d => d.Defense.BaseValue)})" : "")}");
            Console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            Console.WriteLine($"Gold Coins: {GoldCoins}");
            Console.WriteLine($"Experience Points: {ExperiencePoints}");
            //Console.WriteLine($"Achievement Points: {AchievementPoints}");
            Console.ResetColor();

            //if (showAchievements)
            //{
            //    Console.WriteLine($"Achievements:");
            //    ManageAchievements.PrintAllAchievements();
            //}
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

            var weapons = Bag.OfType<IWeapon>().ToArray();
            if (weapons.Any())
            {
                foreach (var weapon in weapons)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    var equippedMessage = "";

                    if (weapon.IsEquipped)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    Console.WriteLine($"============({weapon.Name})============");
                    Console.WriteLine($"Worth: {weapon.Price.SellingPrice} Gold Coins");
                    Console.WriteLine($"Strength: (+ {weapon.Strength.BaseValue})");
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

            var armors = Bag.OfType<IArmor>().ToArray();
            if (armors.Any())
            {
                foreach (var armor in armors)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    var equippedMessage = "";

                    if (armor.IsEquipped)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    Console.WriteLine($"============({armor.Name})============");
                    Console.WriteLine($"Worth: {armor.Price.SellingPrice} Gold Coins");
                    Console.WriteLine($"Defense: (+ {armor.Defense.BaseValue})");
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

            var shields = Bag.OfType<IShield>().ToArray();
            if (shields.Any())
            {
                foreach (var shield in shields)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    var equippedMessage = "";

                    if (shield.IsEquipped)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    Console.WriteLine($"============({shield.Name})============");
                    Console.WriteLine($"Worth: {shield.Price.SellingPrice} Gold Coins");
                    Console.WriteLine($"Defense: (+ {shield.Defense.BaseValue})");
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

            var healthPotions = Bag.OfType<IHealthPotion>().ToArray();
            if (healthPotions.Any())
            {
                foreach (var healthPotion in healthPotions)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"============({healthPotion.Name})============");
                    Console.WriteLine($"Worth: {healthPotion.Price.SellingPrice} Gold Coins");
                    Console.WriteLine($"Heal Amount: (+ {healthPotion.HealAmount.BaseValue} HP)");
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
            var userInput = "0";
            var successMessage = "";
            var errorMessage = "";

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

                    var weapons = Bag.OfType<IWeapon>().ToArray();
                    if (weapons.Any())
                    {
                        for (var i = 1; i < weapons.Length + 1; i++)
                        {
                            if (weapons[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength.BaseValue}) Strength (Already Equipped)");
                                Console.ResetColor();

                            }
                            else
                            {
                                Console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength.BaseValue}) Strength");
                            }
                        }

                        var isNumber = int.TryParse(Console.ReadLine().Trim(), out var userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || userIndex < 0 || userIndex >= weapons.Length)
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

                            successMessage = $"You equipped your {EquippedItems.Last().Name}!"; // TODO: improve how to get the item that was just equipped
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

                    var armor = Bag.OfType<IArmor>().ToArray();

                    if (armor.Any())
                    {
                        for (var i = 1; i < armor.Length + 1; i++)
                        {
                            if (armor[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {armor[i - 1].Name} --> (+ {armor[i - 1].Defense.BaseValue}) Defense (Already Equipped)");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"{i}. {armor[i - 1].Name} --> (+ {armor[i - 1].Defense.BaseValue}) Defense");
                            }
                        }

                        var isNumber = int.TryParse(Console.ReadLine().Trim(), out var userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || userIndex < 0 || userIndex >= armor.Length)
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

                            successMessage = $"You equipped your {EquippedItems.Last().Name}!"; // TODO: improve how to get the item that was just equipped
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

                    var shields = Bag.OfType<IShield>().ToArray();

                    if (shields.Any())
                    {
                        for (var i = 1; i < shields.Length + 1; i++)
                        {
                            if (shields[i - 1].IsEquipped)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{i}. {shields[i - 1].Name} --> (+ {shields[i - 1].Defense.BaseValue}) Defense (Already Equipped)");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"{i}. {shields[i - 1].Name} --> (+ {shields[i - 1].Defense.BaseValue}) Defense");
                            }
                        }

                        var isNumber = int.TryParse(Console.ReadLine().Trim(), out var userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || userIndex < 0 || userIndex >= shields.Length)
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

                            successMessage = $"You equipped your {EquippedItems.Last().Name}!"; // TODO: improve how to get the item that was just equipped
                        }
                    }
                    else
                    {
                        errorMessage = "You have nothing to equip . . .";
                    }
                }
                else if (userInput == "4")
                {
                    EquippedItems.Clear();
                }
                else if (userInput == "5")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******* Your Health Potions *******");
                    Console.ResetColor();

                    var healthPotions = Bag.OfType<IHealthPotion>().ToArray();

                    if (healthPotions.Any())
                    {
                        for (var i = 1; i < healthPotions.Length + 1; i++)
                        {
                            Console.WriteLine($"{i}. {healthPotions[i - 1].Name} --> (+ {healthPotions[i - 1].HealAmount} HP)");
                        }

                        var isNumber = int.TryParse(Console.ReadLine().Trim(), out var userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || userIndex < 0 || userIndex >= healthPotions.Length)
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
            var weapons = Bag.OfType<IWeapon>().ToArray();
            if (weapons.Any())
            {
                var currentEquippedWeapon = EquippedItems.OfType<IWeapon>().SingleOrDefault();
                if (currentEquippedWeapon != null)
                {
                    UnEquipItem(currentEquippedWeapon);
                }

                var foundItem = Bag[weaponIndex];

                if (foundItem is IWeapon weapon)
                {
                    EquippedItems.Add(weapon);
                    weapon.IsEquipped = true;
                }
                else
                {
                    throw new InvalidOperationException($"Error: Trying Invalid Item that isn't a {nameof(IWeapon)} type from {nameof(Bag)}. (Was at this index {weaponIndex})");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou don't have any weapons to equip!");
                Console.ResetColor();
            }
        }


        public void Equip(IEquippableItem equippableItem)
        {
            if (Bag.Any())
            {
                if (IsItemInBag(equippableItem))
                {
                    Bag.Remove(equippableItem);
                    EquippedItems.Add(equippableItem);
                    equippableItem.IsEquipped = true;
                }
                else if (IsItemInBag(equippableItem) && IsItemEquipped(equippableItem))
                {
                    Bag.Remove(equippableItem);
                    equippableItem.IsEquipped = true;
                }
                else
                {
                    throw new Exception("Unexpected error occurred");
                }
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
        EquipArmor ---> Simple method to equip a armor via index
        ======================================================================================== 
        */
        public void EquipArmor(int armorIndex)
        {
            var armors = Bag.OfType<IArmor>().ToArray();
            if (armors.Any())
            {
                var currentEquippedWeapon = EquippedItems.OfType<IArmor>().SingleOrDefault();
                if (currentEquippedWeapon != null)
                {
                    UnEquipItem(currentEquippedWeapon);
                }

                if (Bag[armorIndex] is IArmor armor)
                {
                    EquippedItems.Add(armor);
                    armor.IsEquipped = true;
                }
                else
                {
                    throw new InvalidOperationException($"Error: Trying Invalid Item that isn't a {nameof(IArmor)} type from {nameof(Bag)}. (Was at this index {armorIndex})");
                }
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
            var shields = Bag.OfType<IShield>().ToArray();
            if (shields.Any())
            {
                var currentEquippedWeapon = EquippedItems.OfType<IShield>().SingleOrDefault();
                if (currentEquippedWeapon != null)
                {
                    UnEquipItem(currentEquippedWeapon);
                }

                if (Bag[shieldIndex] is IShield shield)
                {
                    EquippedItems.Add(shield);
                    shield.IsEquipped = true;
                }
                else
                {
                    throw new InvalidOperationException($"Error: Trying Invalid Item that isn't a {nameof(IShield)} type from {nameof(Bag)}. (Was at this index {shieldIndex})");
                }
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
        public void UnEquipItem(IEquippableItem item)
        {
            if (item is IArmor armor)
            {
                if (IsItemEquipped(armor))
                {
                    EquippedItems.Remove(armor);
                    armor.IsEquipped = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{armor.Name} was unequipped!");
                    Console.ResetColor();
                }
            }
            else if (item is IWeapon weapon)
            {
                if (IsItemEquipped(weapon))
                {
                    EquippedItems.Remove(weapon);
                    weapon.IsEquipped = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{weapon.Name} was unequipped!");
                    Console.ResetColor();
                }
            }
            else if (item is IShield shield)
            {
                if (IsItemEquipped(shield))
                {
                    EquippedItems.Remove(shield);
                    shield.IsEquipped = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{shield.Name} was unequipped!");
                    Console.ResetColor();
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
            if (Bag.OfType<IHealthPotion>().Any() && Bag[healthPotionIndex] is IHealthPotion healthPotion)
            {
                CurrentHP += healthPotion.HealAmount.BaseValue;

                // Allows user to heal to max hp but not allow to go above original hp
                // example: curHP: 25 oriHP: 30 --> I was a health potion that heals for 7
                // then my curHP would go to 30 and not past it.
                CurrentHP = CurrentHP > OriginalHP ? OriginalHP : CurrentHP;

                Console.WriteLine($"You used {healthPotion.Name}");
                Console.WriteLine($"Your HP: {CurrentHP}/{OriginalHP}");
                Bag.RemoveAt(healthPotionIndex);
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
            var isNumber = int.TryParse(Console.ReadLine().Trim(), out var levelAmount);

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
            ShowStats(/*false*/);
            Console.WriteLine("\n");
        }


    }
}