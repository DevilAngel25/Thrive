using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData
{
    public class CreditsMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundCredits";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public CreditsMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "LLoyd Fitzroy-Smith"));
            items.Add(new menuManager.menus.MenuItem(1, "Menu", "TitleMenu", "Back To Main Menu"));
        }
    }
}
