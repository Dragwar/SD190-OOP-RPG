using System.Collections.Generic;

namespace OOP_RPG.Models.Interfaces
{
    public interface IShop
    {
        List<IBuyableItem> AllBuyableItems { get; }

        void DisplayAllItems();
        List<IArmor> GetCurrentArmor();
        List<IShield> GetCurrentShields();
        List<IWeapon> GetCurrentWeapons();
        void OpenShopAndTakeUserOrder();
    }
}