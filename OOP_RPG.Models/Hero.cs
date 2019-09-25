using OOP_RPG.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.Models
{
    public class Hero : IHero
    {
        private readonly IConsole _console;
        public string Name { get; set; }
        public int Strength { get; private set; }
        public int Defense { get; private set; }
        public int OriginalHP { get; private set; }
        public int CurrentHP { get; private set; }
        public int ExperiencePoints { get; private set; }
        public int GoldCoins { get; private set; }
        public ItemInventory<IEquippableItem> EquippedItems { get; }
        public ItemInventory<IItem> Bag { get; }
        public IQueryable<IItem> AllItems => Bag.Concat(EquippedItems).AsQueryable();
        public IAchievementManager AchievementManager { get; private set; }
        public int AchievementPoints { get => AchievementManager.TotalPoints; }

        public Hero(IConsole console, IAchievementManager achievementManager)
        {
            _console = console;
            Strength = 5;
            Defense = 5;
            OriginalHP = 20;
            CurrentHP = 20;
            GoldCoins = 25;
            ExperiencePoints = 10;
            AchievementManager = achievementManager;

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
        public void ShowStats(bool showAchievements)
        {
            _console.ForegroundColor = ConsoleColor.Yellow;
            _console.WriteLine($"\n***** {Name} *****");
            _console.ResetColor();

            var strengthItems = EquippedItems.OfType<IStrengthItem>().ToArray();
            var defenseItems = EquippedItems.OfType<IDefenseItem>().ToArray();


            _console.ForegroundColor = ConsoleColor.Green;
            _console.WriteLine($"Strength: {Strength} {(strengthItems.Any() ? $"(+ {strengthItems.Sum(s => s.Strength.BaseValue)})" : "")}");
            _console.WriteLine($"Defense: {Defense} {(defenseItems.Any() ? $"(+ {defenseItems.Sum(d => d.Defense.BaseValue)})" : "")}");
            _console.WriteLine($"Hit-points: {CurrentHP}/{OriginalHP}");
            _console.WriteLine($"Gold Coins: {GoldCoins}");
            _console.WriteLine($"Experience Points: {ExperiencePoints}");
            _console.WriteLine($"Achievement Points: {AchievementPoints}");
            _console.ResetColor();

            if (showAchievements)
            {
                _console.WriteLine($"Achievements:");
                AchievementManager.PrintAllAchievements();
            }
        }



        /*
        ======================================================================================== 
        ShowInventory ---> Simple method prints all the items that the user has
        ======================================================================================== 
        */
        public void ShowInventory()
        {
            _console.ForegroundColor = ConsoleColor.Yellow;
            _console.WriteLine("\n***** INVENTORY ******");
            _console.ResetColor();

            ShowInventoryWeapons();
            ShowInventoryArmor();
            ShowInventoryShield();
            ShowInventoryHealthPotions();
        }

        public void ShowInventory<TItem>()
            where TItem : IItem
        {
            var itemType = typeof(TItem);
            _console.WriteLine(itemType.Name + ": ");

            var items = AllItems.Where(item => item is TItem).ToArray();
            if (items.Any())
            {
                if (items.All(item => item is IEquippableItem))
                {
                    foreach (var item in items.Cast<IEquippableItem>())
                    {
                        _console.ForegroundColor = ConsoleColor.DarkGray;
                        var equippedMessage = default(string);

                        if (item.IsEquipped)
                        {
                            _console.ForegroundColor = ConsoleColor.Yellow;
                            equippedMessage = "CURRENTLY EQUIPPED\n";
                        }

                        _console.WriteLine(item.ToString());
                        _console.WriteLine(equippedMessage);
                    }
                }
                else if (items.All(item => item is IHealthPotion))
                {
                    foreach (var item in items)
                    {
                        _console.ForegroundColor = ConsoleColor.DarkGreen;
                        _console.WriteLine(item.ToString());
                        _console.WriteLine();
                    }
                }
                else
                {
                    foreach (var item in items)
                    {
                        _console.ForegroundColor = ConsoleColor.DarkGray;
                        _console.WriteLine(item.ToString());
                        _console.WriteLine();
                    }
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("You Have No Weapons . . .");
            }
            _console.ResetColor();
        }


        /*
        ======================================================================================== 
        ShowInventoryWeapons ---> Simple method that prints each weapon item that the hero has
        ======================================================================================== 
        */
        public void ShowInventoryWeapons()
        {
            _console.WriteLine("Weapons: ");

            var weapons = AllItems.OfType<IWeapon>().ToArray();
            if (weapons.Any())
            {
                foreach (var weapon in weapons)
                {
                    _console.ForegroundColor = ConsoleColor.DarkGray;
                    var equippedMessage = "";

                    if (weapon.IsEquipped)
                    {
                        _console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    _console.WriteLine($"============({weapon.Name})============");
                    _console.WriteLine($"Worth: {weapon.Price.SellingPrice} Gold Coins");
                    _console.WriteLine($"Strength: (+ {weapon.Strength.BaseValue})");
                    _console.WriteLine(equippedMessage);
                    _console.ResetColor();
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("You Have No Weapons . . .");
            }
            _console.ResetColor();
        }

        /*
        ======================================================================================== 
        ShowInventoryArmor ---> Simple method that prints each armor item that the hero has
        ======================================================================================== 
        */
        public void ShowInventoryArmor()
        {
            _console.WriteLine("Armor: ");

            var armors = AllItems.OfType<IArmor>().ToArray();
            if (armors.Any())
            {
                foreach (var armor in armors)
                {
                    _console.ForegroundColor = ConsoleColor.DarkGray;
                    var equippedMessage = "";

                    if (armor.IsEquipped)
                    {
                        _console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    _console.WriteLine($"============({armor.Name})============");
                    _console.WriteLine($"Worth: {armor.Price.SellingPrice} Gold Coins");
                    _console.WriteLine($"Defense: (+ {armor.Defense.BaseValue})");
                    _console.WriteLine(equippedMessage);
                    _console.ResetColor();
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("You Have No Armor . . .");
            }
            _console.ResetColor();
        }

        /*
        ======================================================================================== 
        ShowInventoryShield ---> Simple method that prints each shield item that the hero has
        ======================================================================================== 
        */
        public void ShowInventoryShield()
        {
            _console.WriteLine("Shields: ");

            var shields = AllItems.OfType<IShield>().ToArray();
            if (shields.Any())
            {
                foreach (var shield in shields)
                {
                    _console.ForegroundColor = ConsoleColor.DarkGray;
                    var equippedMessage = "";

                    if (shield.IsEquipped)
                    {
                        _console.ForegroundColor = ConsoleColor.Yellow;
                        equippedMessage = "CURRENTLY EQUIPPED\n";
                    }

                    _console.WriteLine($"============({shield.Name})============");
                    _console.WriteLine($"Worth: {shield.Price.SellingPrice} Gold Coins");
                    _console.WriteLine($"Defense: (+ {shield.Defense.BaseValue})");
                    _console.WriteLine(equippedMessage);
                    _console.ResetColor();
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("You Have No Shields . . .");
            }
            _console.ResetColor();
        }



        /*
        ======================================================================================== 
        ShowInventoryHealthPotions ---> Simple method that prints each potion that the hero has
        ======================================================================================== 
        */
        public void ShowInventoryHealthPotions()
        {
            _console.WriteLine("HealthPotions: ");

            var healthPotions = AllItems.OfType<IHealthPotion>().ToArray();
            if (healthPotions.Any())
            {
                foreach (var healthPotion in healthPotions)
                {
                    _console.ForegroundColor = ConsoleColor.DarkGreen;
                    _console.WriteLine($"============({healthPotion.Name})============");
                    _console.WriteLine($"Worth: {healthPotion.Price.SellingPrice} Gold Coins");
                    _console.WriteLine($"Heal Amount: (+ {healthPotion.HealAmount.BaseValue} HP)");
                    _console.WriteLine();
                    _console.ResetColor();
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("You Have No Health Potions . . .");
            }
            _console.ResetColor();
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
                _console.Title = $"{Name}'s Inventory | Stats: [> Str: {Strength} | Def: {Defense} | HP: {CurrentHP}/{OriginalHP} <]";
                ShowInventory();

                _console.ForegroundColor = ConsoleColor.Yellow;
                _console.WriteLine(successMessage);
                _console.ResetColor();
                successMessage = "";

                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine(errorMessage);
                _console.ResetColor();
                errorMessage = "";

                _console.WriteLine("1. equip a Weapon.");
                _console.WriteLine("2. equip Armor.");
                _console.WriteLine("3. equip Shield.");
                _console.WriteLine("4. unequip all items.");
                _console.WriteLine("5. use Health Potion.");
                _console.WriteLine("6. exit\n");

                userInput = _console.ReadLine().Trim();

                if (userInput == "1")
                {
                    _console.Clear();

                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine("******* Unequipped Weapons *******");
                    _console.ResetColor();

                    var weapons = Bag.OfType<IWeapon>().ToArray();
                    if (weapons.Any())
                    {
                        for (var i = 1; i < weapons.Length + 1; i++)
                        {
                            if (weapons[i - 1].IsEquipped)
                            {
                                _console.ForegroundColor = ConsoleColor.Yellow;
                                _console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength.BaseValue}) Strength (Already Equipped)");
                                _console.ResetColor();

                            }
                            else
                            {
                                _console.WriteLine($"{i}. {weapons[i - 1].Name} --> (+ {weapons[i - 1].Strength.BaseValue}) Strength");
                            }
                        }

                        var isNumber = int.TryParse(_console.ReadLine().Trim(), out var userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || userIndex < 0 || userIndex >= weapons.Length)
                        {
                            _console.ForegroundColor = ConsoleColor.Red;
                            _console.WriteLine("Nothing was equipped because of one of the following errors:");
                            _console.WriteLine("- did not input a number");
                            _console.WriteLine("- inputted number was too small");
                            _console.WriteLine("- inputted number was too big");
                            _console.ResetColor();
                            return;
                        }
                        else
                        {
                            Equip(weapons[userIndex]);

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
                    _console.Clear();

                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine("******* Unequipped Armor *******");
                    _console.ResetColor();

                    var armors = Bag.OfType<IArmor>().ToArray();

                    if (armors.Any())
                    {
                        for (var i = 1; i < armors.Length + 1; i++)
                        {
                            if (armors[i - 1].IsEquipped)
                            {
                                _console.ForegroundColor = ConsoleColor.Yellow;
                                _console.WriteLine($"{i}. {armors[i - 1].Name} --> (+ {armors[i - 1].Defense.BaseValue}) Defense (Already Equipped)");
                                _console.ResetColor();
                            }
                            else
                            {
                                _console.WriteLine($"{i}. {armors[i - 1].Name} --> (+ {armors[i - 1].Defense.BaseValue}) Defense");
                            }
                        }

                        var isNumber = int.TryParse(_console.ReadLine().Trim(), out var userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || userIndex < 0 || userIndex >= armors.Length)
                        {
                            _console.ForegroundColor = ConsoleColor.Red;
                            _console.WriteLine("Nothing was equipped because of one of the following errors:");
                            _console.WriteLine("- did not input a number");
                            _console.WriteLine("- inputted number was too small");
                            _console.WriteLine("- inputted number was too big");
                            _console.ResetColor();
                            return;
                        }
                        else
                        {
                            Equip(armors[userIndex]);

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
                    _console.Clear();

                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine("******* Unequipped Shields *******");
                    _console.ResetColor();

                    var shields = Bag.OfType<IShield>().ToArray();

                    if (shields.Any())
                    {
                        for (var i = 1; i < shields.Length + 1; i++)
                        {
                            if (shields[i - 1].IsEquipped)
                            {
                                _console.ForegroundColor = ConsoleColor.Yellow;
                                _console.WriteLine($"{i}. {shields[i - 1].Name} --> (+ {shields[i - 1].Defense.BaseValue}) Defense (Already Equipped)");
                                _console.ResetColor();
                            }
                            else
                            {
                                _console.WriteLine($"{i}. {shields[i - 1].Name} --> (+ {shields[i - 1].Defense.BaseValue}) Defense");
                            }
                        }

                        var isNumber = int.TryParse(_console.ReadLine().Trim(), out var userIndex);

                        // account for index offset of 1
                        userIndex--;

                        if (!isNumber || userIndex < 0 || userIndex >= shields.Length)
                        {
                            _console.ForegroundColor = ConsoleColor.Red;
                            _console.WriteLine("Nothing was equipped because of one of the following errors:");
                            _console.WriteLine("- did not input a number");
                            _console.WriteLine("- inputted number was too small");
                            _console.WriteLine("- inputted number was too big");
                            _console.ResetColor();
                            return;
                        }
                        else
                        {
                            Equip(shields[userIndex]);

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
                    _console.Clear();

                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine("******* Your Health Potions *******");
                    _console.ResetColor();

                    var healthPotions = Bag.OfType<IHealthPotion>().ToArray();

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
                            _console.ForegroundColor = ConsoleColor.Red;
                            _console.WriteLine("Nothing was used because of one of the following errors:");
                            _console.WriteLine("- did not input a number");
                            _console.WriteLine("- inputted number was too small");
                            _console.WriteLine("- inputted number was too big");
                            _console.ResetColor();
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

                                UseHealthPotion(healthPotions[userIndex]);
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



        public void Equip(IEquippableItem equippableItem)
        {
            if (Bag.Any(item => item is IEquippableItem))
            {
                // remove preexisting items of the same type before adding the new one
                List<IEquippableItem> unequippedItems = equippableItem switch
                {
                    IWeapon _ => EquippedItems.RemoveItemsOfType<IWeapon>().Cast<IEquippableItem>().ToList(),
                    IArmor _ => EquippedItems.RemoveItemsOfType<IArmor>().Cast<IEquippableItem>().ToList(),
                    IShield _ => EquippedItems.RemoveItemsOfType<IShield>().Cast<IEquippableItem>().ToList(),
                    _ => throw new Exception($"Unexpected type ({equippableItem})")
                };

                unequippedItems.ForEach(Bag.Add);

                if (unequippedItems.Count > 1)
                {
                    throw new Exception("Unexpected number of items removed");
                }


                if (IsItemInBag(equippableItem) && IsItemEquipped(equippableItem))
                {
                    Bag.Remove(equippableItem);
                    equippableItem.IsEquipped = true;
                }
                else if (IsItemInBag(equippableItem))
                {
                    Bag.Remove(equippableItem);
                    EquippedItems.Add(equippableItem);
                    equippableItem.IsEquipped = true;
                }
                else if (IsItemEquipped(equippableItem))
                {
                    equippableItem.IsEquipped = true;
                }
                else
                {
                    throw new Exception("Unexpected error occurred");
                }
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("\nYou don't have any items to equip!");
                _console.ResetColor();
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
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("\nYou don't have any armor to equip!");
                _console.ResetColor();
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
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("\nYou don't have any shield to equip!");
                _console.ResetColor();
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
                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine($"{armor.Name} was unequipped!");
                    _console.ResetColor();
                }
            }
            else if (item is IWeapon weapon)
            {
                if (IsItemEquipped(weapon))
                {
                    EquippedItems.Remove(weapon);
                    weapon.IsEquipped = false;
                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine($"{weapon.Name} was unequipped!");
                    _console.ResetColor();
                }
            }
            else if (item is IShield shield)
            {
                if (IsItemEquipped(shield))
                {
                    EquippedItems.Remove(shield);
                    shield.IsEquipped = false;
                    _console.ForegroundColor = ConsoleColor.Yellow;
                    _console.WriteLine($"{shield.Name} was unequipped!");
                    _console.ResetColor();
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
        public void UseHealthPotion(IHealthPotion healthPotion)
        {
            if (IsItemInBag(healthPotion))
            {
                CurrentHP += healthPotion.HealAmount.BaseValue;

                // Allows user to heal to max hp but not allow to go above original hp
                // example: curHP: 25 oriHP: 30 --> I was a health potion that heals for 7
                // then my curHP would go to 30 and not past it.
                CurrentHP = CurrentHP > OriginalHP ? OriginalHP : CurrentHP;

                _console.WriteLine($"You used {healthPotion.Name}");
                _console.WriteLine($"Your HP: {CurrentHP}/{OriginalHP}");
                Bag.Remove(healthPotion);
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("\nYou don't have any health potion to use!");
                _console.ResetColor();
            }
        }



        /*
        ======================================================================================== 
        LevelUp ---> Adds the inputed number to level up the passed in stat
        ======================================================================================== 
        */
        public int LevelUp(int heroStatValue)
        {
            var isNumber = int.TryParse(_console.ReadLine().Trim(), out var levelAmount);

            if (isNumber && levelAmount <= ExperiencePoints)
            {
                heroStatValue += levelAmount;
                RemoveExperiencePoints(levelAmount);
            }
            else
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine("Nothing Leveled Up (input wasn't a int or input was greater than current exp)\n");
                _console.ResetColor();
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
                _console.WriteLine("================[Level Up Strength]================");
                _console.ForegroundColor = ConsoleColor.Green;
                _console.WriteLine($"Current Experience Points: {ExperiencePoints}");
                _console.WriteLine($"Current Strength: {Strength}");
                _console.ResetColor();

                _console.WriteLine("Level Up Strength by:\n");
                Strength = LevelUp(Strength);
            }
            else if (userInput == "2")
            {
                _console.WriteLine("================[Level Up Defense]================");
                _console.ForegroundColor = ConsoleColor.Green;
                _console.WriteLine($"Current Experience Points: {ExperiencePoints}");
                _console.WriteLine($"Current Defense: {Defense}");
                _console.ResetColor();

                _console.WriteLine("Level Up Defense by:\n");
                Defense = LevelUp(Defense);
            }
            else if (userInput == "3")
            {
                _console.WriteLine("================[Level Up Original HP]================");
                _console.ForegroundColor = ConsoleColor.Green;
                _console.WriteLine($"Current Experience Points: {ExperiencePoints}");
                _console.WriteLine($"Current OriginalHP: {OriginalHP}");
                _console.ResetColor();

                _console.WriteLine("Level Up HP by:\n");
                OriginalHP = LevelUp(OriginalHP);
            }
            ShowStats(false);
            _console.WriteLine("\n");
        }
    }
}