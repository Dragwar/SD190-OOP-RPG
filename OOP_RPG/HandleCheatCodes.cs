namespace OOP_RPG
{
    public static class HandleCheatCodes
    {
        public static void HeroNameCheat(Hero hero, Shop shop, string heroName)
        {
            switch (heroName)
            {
                case "Everett":
                    hero.AddGoldCoins(1000);
                    hero.AddExperiencePoints(1000);
                    break;

                case "Gui":
                    Weapon steelSword = new Weapon("Steel Straight Sword", 20, 25) { Sold = true };
                    hero.WeaponsBag.Add(steelSword);
                    shop.AllBuyableItems.Add(steelSword);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(steelSword));

                    Armor steelArmor = new Armor("Hardened Steel Armor", 20, 45) { Sold = true };
                    hero.ArmorBag.Add(steelArmor);
                    shop.AllBuyableItems.Add(steelArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(steelArmor));

                    Shield steelShield = new Shield("Tower Steel Shield", 15, 60) { Sold = true };
                    hero.ShieldBag.Add(steelShield);
                    shop.AllBuyableItems.Add(steelShield);
                    hero.EquipShield(hero.ShieldBag.IndexOf(steelShield));
                    break;

                case "John":
                    Weapon rapier = new Weapon("Rapier", 40, 50) { Sold = true };
                    hero.WeaponsBag.Add(rapier);
                    shop.AllBuyableItems.Add(rapier);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(rapier));

                    Armor lightSteelArmor = new Armor("Light Steel Armor", 12, 20) { Sold = true };
                    hero.ArmorBag.Add(lightSteelArmor);
                    shop.AllBuyableItems.Add(lightSteelArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(lightSteelArmor));

                    Shield parryShield = new Shield("Parrying Shield", 7, 12) { Sold = true };
                    hero.ShieldBag.Add(parryShield);
                    shop.AllBuyableItems.Add(parryShield);
                    hero.EquipShield(hero.ShieldBag.IndexOf(parryShield));
                    break;

                case "Darius":
                    Weapon greatWarAxe = new Weapon("Great War Axe", 120, 135) { Sold = true };
                    hero.WeaponsBag.Add(greatWarAxe);
                    shop.AllBuyableItems.Add(greatWarAxe);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(greatWarAxe));

                    Armor largeIronArmor = new Armor("Heavy Iron Armor", 35, 40) { Sold = true };
                    hero.ArmorBag.Add(largeIronArmor);
                    shop.AllBuyableItems.Add(largeIronArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(largeIronArmor));
                    break;

                case "JK":
                    hero.AddGoldCoins(1000);
                    hero.AddExperiencePoints(1000);
                    break;

                case "Guts":
                    Weapon dragonSlayer = new Weapon("DragonSlayer", 120, 200) { Sold = true };
                    hero.WeaponsBag.Add(dragonSlayer);
                    shop.AllBuyableItems.Add(dragonSlayer);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(dragonSlayer));

                    Armor berserkerArmor = new Armor("Berserker Armor", 80, 150) { Sold = true };
                    hero.ArmorBag.Add(berserkerArmor);
                    shop.AllBuyableItems.Add(berserkerArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(berserkerArmor));
                    break;
            }
        }
    }
}
