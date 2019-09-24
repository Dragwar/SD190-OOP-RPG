namespace OOP_RPG.Models.Interfaces
{
    public interface IEquippableItem : IItem
    {
        bool IsEquipped { get; set; } //TODO: Find a better way (don't make set public)
    }
}
