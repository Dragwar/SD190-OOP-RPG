using OOP_RPG.Models.Interfaces;
using OOP_RPG.Models.Items;
using System;

namespace OOP_RPG.ConsoleGame
{
    public static class HandleCheatCodes
    {
        private static void CreateAndAddCheatItem(Hero hero, Shop shop, IBuyableItem cheatItem)
        {
            if (cheatItem is IWeapon weapon)
            {
                hero.Bag.Add(weapon);
                shop.AllBuyableItems.Add(weapon);
                hero.EquipWeapon(hero.Bag.IndexOf(weapon));
            }
            else if (cheatItem is IArmor armor)
            {
                hero.Bag.Add(armor);
                shop.AllBuyableItems.Add(armor);
                hero.EquipArmor(hero.Bag.IndexOf(armor));
            }
            else if (cheatItem is IShield shield)
            {
                hero.Bag.Add(shield);
                shop.AllBuyableItems.Add(shield);
                hero.EquipShield(hero.Bag.IndexOf(shield));
            }
            else
            {
                throw new NotImplementedException("item type N/A");
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
                    var steelSword = new Weapon("Steel Straight Sword", 20, 25);
                    CreateAndAddCheatItem(hero, shop, steelSword);

                    var steelArmor = new Armor("Hardened Steel Armor", 20, 45);
                    CreateAndAddCheatItem(hero, shop, steelArmor);

                    var steelShield = new Shield("Tower Steel Shield", 15, 60);
                    CreateAndAddCheatItem(hero, shop, steelShield);
                    break;

                case "john":
                case "John":
                    var rapier = new Weapon("Rapier", 40, 50);
                    CreateAndAddCheatItem(hero, shop, rapier);

                    var lightSteelArmor = new Armor("Light Steel Armor", 12, 20);
                    CreateAndAddCheatItem(hero, shop, lightSteelArmor);

                    var parryShield = new Shield("Parrying Shield", 7, 12);
                    CreateAndAddCheatItem(hero, shop, parryShield);
                    break;

                case "darius":
                case "Darius":
                    var greatWarAxe = new Weapon("Great War Axe", 120, 135);
                    CreateAndAddCheatItem(hero, shop, greatWarAxe);

                    var largeIronArmor = new Armor("Heavy Iron Armor", 35, 40);
                    CreateAndAddCheatItem(hero, shop, largeIronArmor);
                    break;

                case "jk":
                case "JK":
                    hero.AddGoldCoins(1000);
                    hero.AddExperiencePoints(1000);
                    break;

                case "guts":
                case "Guts":
                    var dragonSlayer = new Weapon("DragonSlayer", 120, 200);
                    CreateAndAddCheatItem(hero, shop, dragonSlayer);

                    var berserkerArmor = new Armor("Berserker Armor", 80, 150);
                    CreateAndAddCheatItem(hero, shop, berserkerArmor);
                    break;
            }
        }
    }
}
