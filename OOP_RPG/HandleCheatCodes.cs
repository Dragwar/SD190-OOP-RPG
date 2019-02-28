namespace OOP_RPG
{
    public static class HandleCheatCodes
    {
        public static void HeroNameCheat(Hero hero, string heroName)
        {
            switch (heroName)
            {
                case "Everett":
                    hero.AddGoldCoins(1000);
                    hero.AddExperiencePoints(1000);
                    break;

                case "Gui":
                    Weapon steelSword = new Weapon("Steel Straight Sword", 20, 25);
                    hero.WeaponsBag.Add(steelSword);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(steelSword));

                    Armor steelArmor = new Armor("Hardened Steel Armor", 20, 45);
                    hero.ArmorBag.Add(steelArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(steelArmor));

                    Shield steelShield = new Shield("Tower Steel Shield", 15, 60);
                    hero.ShieldBag.Add(steelShield);
                    hero.EquipShield(hero.ShieldBag.IndexOf(steelShield));
                    break;

                case "John":
                    Weapon rapier = new Weapon("Rapier", 40, 50);
                    hero.WeaponsBag.Add(rapier);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(rapier));

                    Armor lightSteelArmor = new Armor("Light Steel Armor", 12, 20);
                    hero.ArmorBag.Add(lightSteelArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(lightSteelArmor));

                    Shield parryShield = new Shield("Parrying Shield", 7, 12);
                    hero.ShieldBag.Add(parryShield);
                    hero.EquipShield(hero.ShieldBag.IndexOf(parryShield));
                    break;

                case "Darius":
                    Weapon greatWarAxe = new Weapon("Great War Axe", 120, 135);
                    hero.WeaponsBag.Add(greatWarAxe);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(greatWarAxe));

                    Armor largeIronArmor = new Armor("Heavy Iron Armor", 35, 40);
                    hero.ArmorBag.Add(largeIronArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(largeIronArmor));
                    break;
                    
                case "JK":
                    hero.AddGoldCoins(1000);
                    hero.AddExperiencePoints(1000);
                    break;

                case "Guts":
                    Weapon dragonSlayer = new Weapon("DragonSlayer", 120, 200);
                    hero.WeaponsBag.Add(dragonSlayer);
                    hero.EquipWeapon(hero.WeaponsBag.IndexOf(dragonSlayer));

                    Armor berserkerArmor = new Armor("Berserker Armor", 80, 150);
                    hero.ArmorBag.Add(berserkerArmor);
                    hero.EquipArmor(hero.ArmorBag.IndexOf(berserkerArmor));
                    break;
            }
        }
    }
}
