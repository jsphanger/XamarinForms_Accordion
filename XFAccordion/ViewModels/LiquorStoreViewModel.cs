using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using XFAccordion.Models.LiquorStore;

namespace XFAccordion.ViewModels
{
    public class LiquorStoreViewModel : BaseViewModel
    {
        private IList _inventory;

        public IList Inventory { get { return _inventory; } set { _inventory = value; OnPropertyChanged(nameof(Inventory)); } }

        public LiquorStoreViewModel()
        {
            GetUngroupedInventory();
            //GetGroupedInventory();
        }

        public void GetUngroupedInventory()
        {
            Inventory = new List<Liquor>();

            Inventory.Add(new Liquor() { BrandName = "Tito's Handmade Vodka", Type = LiquorType.Vodka, Size = "1.75 L", Price = 28.99, Quantity = 5 });
            Inventory.Add(new Liquor() { BrandName = "Ketel One Vodka", Type = LiquorType.Vodka, Size = "1.75 L", Price = 32.99, Quantity = 2 });
            Inventory.Add(new Liquor() { BrandName = "Grey Goose Vodka", Type = LiquorType.Vodka, Size = "750 ml", Price = 24.99, Quantity = 4 });

            Inventory.Add(new Liquor() { BrandName = "Bacardi Gold Rum", Type = LiquorType.Rum, Size = "1.75 L", Price = 16.99, Quantity = 1 });
            Inventory.Add(new Liquor() { BrandName = "Captain Morgan Spiced Rum", Type = LiquorType.Rum, Size = "750 ml", Price = 13.99, Quantity = 4 });
            Inventory.Add(new Liquor() { BrandName = "Malibu Coconut Rum", Type = LiquorType.Rum, Size = "750 ml", Price = 14.99, Quantity = 0 });

            Inventory.Add(new Liquor() { BrandName = "Bombay Sapphire Dry Gin", Type = LiquorType.Gin, Size = "750 ml", Price = 22.99, Quantity = 2 });
            Inventory.Add(new Liquor() { BrandName = "Empress 1908 Gin", Type = LiquorType.Gin, Size = "750 ml", Price = 34.99, Quantity = 4 });

            Inventory.Add(new Liquor() { BrandName = "Bulleit Bourbon", Type = LiquorType.Whiskey, Size = "1.75 L", Price = 38.99, Quantity = 2 });
            Inventory.Add(new Liquor() { BrandName = "Jameson Blended Irish Whiskey", Type = LiquorType.Whiskey, Size = "1.75 L", Price = 39.99, Quantity = 2 });
            Inventory.Add(new Liquor() { BrandName = "Maker's Mark Bourbon", Type = LiquorType.Whiskey, Size = "750 ml", Price = 24.99, Quantity = 3 });
            Inventory.Add(new Liquor() { BrandName = "Crown Royal Canadian Whiskey", Type = LiquorType.Whiskey, Size = "1.75 L", Price = 38.99, Quantity = 1 });
            Inventory.Add(new Liquor() { BrandName = "Bulleit Bourbon", Type = LiquorType.Whiskey, Size = "750 ml", Price = 27.99, Quantity = 4 });
            Inventory.Add(new Liquor() { BrandName = "Jack Daniel's Old No 7 Tennessee Whiskey", Type = LiquorType.Whiskey, Size = "750 ml", Price = 21.99, Quantity = 3 });

            Inventory = ((List<Liquor>)Inventory).OrderBy(x => (LiquorType)x.Type).ThenBy(x => x.Size).ThenBy(x => x.BrandName).ToList();
        }
        public void GetGroupedInventory()
        {
            Inventory = new List<InventoryGroup>();

            var vodkaGroup = new List<Liquor>();
            vodkaGroup.Add(new Liquor() { BrandName = "Tito's Handmade Vodka", Type = LiquorType.Vodka, Size = "1.75 L", Price = 28.99, Quantity = 5 });
            vodkaGroup.Add(new Liquor() { BrandName = "Ketel One Vodka", Type = LiquorType.Vodka, Size = "1.75 L", Price = 32.99, Quantity = 2 });
            vodkaGroup.Add(new Liquor() { BrandName = "Grey Goose Vodka", Type = LiquorType.Vodka, Size = "750 ml", Price = 24.99, Quantity = 4 });
            vodkaGroup = vodkaGroup.OrderBy(x => x.Size).ThenBy(x => x.BrandName).ToList();

            var rumGroup = new List<Liquor>();
            rumGroup.Add(new Liquor() { BrandName = "Bacardi Gold Rum", Type = LiquorType.Rum, Size = "1.75 L", Price = 16.99, Quantity = 1 });
            rumGroup.Add(new Liquor() { BrandName = "Captain Morgan Spiced Rum", Type = LiquorType.Rum, Size = "750 ml", Price = 13.99, Quantity = 4 });
            rumGroup.Add(new Liquor() { BrandName = "Malibu Coconut Rum", Type = LiquorType.Rum, Size = "750 ml", Price = 14.99, Quantity = 0 });
            rumGroup = rumGroup.OrderBy(x => x.Size).ThenBy(x => x.BrandName).ToList();

            var ginGroup = new List<Liquor>();
            ginGroup.Add(new Liquor() { BrandName = "Bombay Sapphire Dry Gin", Type = LiquorType.Gin, Size = "750 ml", Price = 22.99, Quantity = 2 });
            ginGroup.Add(new Liquor() { BrandName = "Empress 1908 Gin", Type = LiquorType.Gin, Size = "750 ml", Price = 34.99, Quantity = 4 });
            ginGroup = ginGroup.OrderBy(x => x.Size).ThenBy(x => x.BrandName).ToList();

            var whiskeyGroup = new List<Liquor>();
            whiskeyGroup.Add(new Liquor() { BrandName = "Bulleit Bourbon", Type = LiquorType.Whiskey, Size = "1.75 L", Price = 38.99, Quantity = 2 });
            whiskeyGroup.Add(new Liquor() { BrandName = "Jameson Blended Irish Whiskey", Type = LiquorType.Whiskey, Size = "1.75 L", Price = 39.99, Quantity = 2 });
            whiskeyGroup.Add(new Liquor() { BrandName = "Maker's Mark Bourbon", Type = LiquorType.Whiskey, Size = "750 ml", Price = 24.99, Quantity = 3 });
            whiskeyGroup.Add(new Liquor() { BrandName = "Crown Royal Canadian Whiskey", Type = LiquorType.Whiskey, Size = "1.75 L", Price = 38.99, Quantity = 1 });
            whiskeyGroup.Add(new Liquor() { BrandName = "Bulleit Bourbon", Type = LiquorType.Whiskey, Size = "750 ml", Price = 27.99, Quantity = 4 });
            whiskeyGroup.Add(new Liquor() { BrandName = "Jack Daniel's Old No 7 Tennessee Whiskey", Type = LiquorType.Whiskey, Size = "750 ml", Price = 21.99, Quantity = 3 });
            whiskeyGroup = whiskeyGroup.OrderBy(x => x.Size).ThenBy(x => x.BrandName).ToList();

            Inventory.Add(new InventoryGroup(vodkaGroup) { GroupTitle = "Vodka" });
            Inventory.Add(new InventoryGroup(rumGroup) { GroupTitle = "Rum" });
            Inventory.Add(new InventoryGroup(ginGroup) { GroupTitle = "Gin" });
            Inventory.Add(new InventoryGroup(whiskeyGroup) { GroupTitle = "Whiskey" });
        }
    }

    public class InventoryGroup : List<Liquor>
    {
        public string GroupTitle { get; set; }

        public InventoryGroup(List<Liquor> liquorList) : base(liquorList) { }
    }
}
