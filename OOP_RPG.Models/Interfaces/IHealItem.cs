namespace OOP_RPG.Models.Interfaces
{
    public interface IHealItem : IItem
    {
        ItemStat HealAmount { get; }
    }
}
