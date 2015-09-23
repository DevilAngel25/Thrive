using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData
{
    public class LoadGameMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundLoadGame";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public LoadGameMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Option", "None", ">>Load Game 1<<"));
            items.Add(new menuManager.menus.MenuItem(1, "Option", "None", ">>Load Game 2<<"));
            items.Add(new menuManager.menus.MenuItem(2, "Option", "None", ">>Load Game 3<<"));
            items.Add(new menuManager.menus.MenuItem(3, "Menu", "TitleMenu", "Back To Main Menu"));
        }
    }
}
