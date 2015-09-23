using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData.optionScreens
{
    public class ControllerOptionMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundControllerOptions";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public ControllerOptionMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Option", "None", ">>Controls<<"));
            items.Add(new menuManager.menus.MenuItem(1, "Menu", "OptionsMenu", "Back To Options Menu"));
        }
    }
}
