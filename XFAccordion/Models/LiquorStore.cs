using System;

namespace XFAccordion.Models.LiquorStore
{
    public enum LiquorType
    {
        Brandy,
        Gin,
        Rum,
        Tequila,
        Vodka,
        Whiskey
    }

    public class Liquor
    {
        public string BrandName { get; set; }
        public LiquorType Type { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
