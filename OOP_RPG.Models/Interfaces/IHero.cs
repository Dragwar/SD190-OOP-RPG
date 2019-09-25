namespace OOP_RPG.Models.Interfaces
{
    public interface IHero
    {
        ItemInventory<IItem> Bag { get; }
        int CurrentHP { get; }
        int Defense { get; }
        ItemInventory<IEquippableItem> EquippedItems { get; }
        int ExperiencePoints { get; }
        int GoldCoins { get; }
        string Name { get; set; }
        int OriginalHP { get; }
        int Strength { get; }

        void AddExperiencePoints(int numberOfPoints);
        void AddGoldCoins(int numberOfCoins);
        void Equip(IEquippableItem equippableItem);
        bool IsItemEquipped(IEquippableItem equippableItem);
        bool IsItemInBag(IItem item);
        int LevelUp(int heroStatValue);
        void ManageInventory();
        void RemoveExperiencePoints(int numberOfPoints);
        void RemoveGoldCoins(int numberOfCoins);
        void ShowInventory();
        void ShowInventoryArmor();
        void ShowInventoryHealthPotions();
        void ShowInventoryShield();
        void ShowInventoryWeapons();
        void ShowStats(bool showAchievements);
        void SpendExperiencePoints(string userInput);
        void TakeDamage(int damage);
        void UnEquipItem(IEquippableItem item);
        void UseHealthPotion(IHealthPotion healthPotion);
    }
}