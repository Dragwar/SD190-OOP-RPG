using OOP_RPG.Models.Interfaces;
using OOP_RPG.Models.Items;

namespace OOP_RPG.ConsoleGame
{
    public static class HandleCheatCodes
    {
        private static void CreateAndAddCheatItem(Hero hero, Shop shop, IBuyableItem cheatItem)
        {
            if (cheatItem is Weapon)
            {
                hero.WeaponsBag.Add((Weapon)cheatItem);
                shop.AllBuyableItems.Add(cheatItem);
                hero.EquipWeapon(hero.WeaponsBag.IndexOf((Weapon)cheatItem));
            }
            else if (cheatItem is Armor)
            {
                hero.ArmorBag.Add((Armor)cheatItem);
                shop.AllBuyableItems.Add(cheatItem);
                hero.EquipArmor(hero.ArmorBag.IndexOf((Armor)cheatItem));
            }
            else if (cheatItem is Shield)
            {
                hero.ShieldBag.Add((Shield)cheatItem);
                shop.AllBuyableItems.Add(cheatItem);
                hero.EquipShield(hero.ShieldBag.IndexOf((Shield)cheatItem));
            }
            else
            {
                throw new System.NotImplementedException("item type N/A");
            }
        }

        public static void HeroNameCheat(Hero hero, Shop shop, string heroName)
        {
            switch (heroName)
            {
                case "everett":
                case "Everett":
                    hero.AddGoldCoins(1000);
                    hero.AddExperiencePoints(1000);
                    break;

                case "gui":
                case "Gui":
                    var steelSword = new Weapon("Steel Straight Sword", 20, 25) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, steelSword);

                    var steelArmor = new Armor("Hardened Steel Armor", 20, 45) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, steelArmor);

                    var steelShield = new Shield("Tower Steel Shield", 15, 60) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, steelShield);
                    break;

                case "john":
                case "John":
                    var rapier = new Weapon("Rapier", 40, 50) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, rapier);

                    var lightSteelArmor = new Armor("Light Steel Armor", 12, 20) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, lightSteelArmor);

                    var parryShield = new Shield("Parrying Shield", 7, 12) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, parryShield);
                    break;

                case "darius":
                case "Darius":
                    var greatWarAxe = new Weapon("Great War Axe", 120, 135) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, greatWarAxe);

                    var largeIronArmor = new Armor("Heavy Iron Armor", 35, 40) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, largeIronArmor);
                    break;

                case "jk":
                case "JK":
                    hero.AddGoldCoins(1000);
                    hero.AddExperiencePoints(1000);
                    break;

                case "guts":
                case "Guts":
                    var dragonSlayer = new Weapon("DragonSlayer", 120, 200) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, dragonSlayer);

                    var berserkerArmor = new Armor("Berserker Armor", 80, 150) { Sold = true };
                    CreateAndAddCheatItem(hero, shop, berserkerArmor);
                    break;
            }
        }
    }
}
