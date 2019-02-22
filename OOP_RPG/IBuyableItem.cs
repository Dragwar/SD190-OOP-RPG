using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public interface IBuyableItem
    {
        ItemCategoryEnum ItemCategory { get; }
        Guid ItemId { get; }
        string Name { get; }
        int Price { get; }
        int ModifiesHeroStat { get; }
        bool Sold { get; set; }
    }
}
