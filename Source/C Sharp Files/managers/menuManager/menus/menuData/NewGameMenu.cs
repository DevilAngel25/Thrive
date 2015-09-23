using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData
{
    public class NewGameMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundNewGame";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public NewGameMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Option", "WorldSize", "World Size - " + Options.Instance.WorldSize));
            items.Add(new menuManager.menus.MenuItem(1, "Option", "ResourceQuality", ">>Resources Quality - " + Options.Instance.ResourceQuality + "<<"));
            items.Add(new menuManager.menus.MenuItem(2, "Option", "ResourceQuantity", ">>Resources Quantity - " + Options.Instance.ResourceQuantity + "<<"));
            items.Add(new menuManager.menus.MenuItem(3, "Option", "AnimalDensity", ">>Animal Density - " + Options.Instance.AnimalDensity + "<<"));
            items.Add(new menuManager.menus.MenuItem(4, "Option", "AnimalAggression", ">>Animal Aggression - " + Options.Instance.AnimalAggression + "<<"));
            items.Add(new menuManager.menus.MenuItem(5, "Option", "PlayerAI", ">>AI Players - " + Options.Instance.PlayerAI + "<<"));
            items.Add(new menuManager.menus.MenuItem(6, "Screen", "GameplayScreen", "Play Game"));
            items.Add(new menuManager.menus.MenuItem(7, "Menu", "TitleMenu", "Back To Main Menu"));
        }
    }
}
