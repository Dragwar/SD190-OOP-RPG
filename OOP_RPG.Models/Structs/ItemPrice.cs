namespace OOP_RPG.Models
{
    public struct ItemPrice
    {
        public int BuyingPrice { get; }
        public int SellingPrice { get; }

        public ItemPrice(int buyingPrice, int sellingPrice)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
        }

        public ItemPrice(int buyingPrice)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = buyingPrice / 2;
        }
    }
}
