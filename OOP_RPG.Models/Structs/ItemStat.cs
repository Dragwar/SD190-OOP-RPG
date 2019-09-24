namespace OOP_RPG.Models
{
    public readonly struct ItemStat
    {
        public readonly int BaseValue { get; }
        public readonly int MinValue { get; }
        public readonly int MaxValue { get; }

        public ItemStat(int baseValue, int minValue, int maxValue)
        {
            BaseValue = baseValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public ItemStat(int baseValue)
        {
            BaseValue = baseValue;
            MinValue = (int)(baseValue * 0.5);
            MaxValue = (int)(baseValue * 1.5);
        }

        public static (int Min, int Max) CalcDefaultMinAndMaxFromBase(int baseValue) => (Min: (int)(baseValue * 0.5), Max: (int)(baseValue * 1.5));
    }
}
