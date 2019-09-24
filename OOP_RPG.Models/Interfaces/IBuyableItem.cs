namespace OOP_RPG.Models.Interfaces
{
    public interface IBuyableItem : IItem
    {
        ItemPrice Price { get; }
        string ItemStatsAsString();
        string ItemStatsAsString(int itemIndex);
    }
}
