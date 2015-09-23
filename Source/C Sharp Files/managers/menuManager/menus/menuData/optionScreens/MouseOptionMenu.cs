using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData.optionScreens
{
    public class MouseOptionMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundMouseOptions";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public MouseOptionMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Option", "MouseSensitivity", "Mouse Sensitivity - " + Options.Instance.MouseSensitivity));
            items.Add(new menuManager.menus.MenuItem(1, "Option", "None", ">>Invert Mouse X<<"));
            items.Add(new menuManager.menus.MenuItem(2, "Option", "None", ">>Invert Mouse Y<<"));
            items.Add(new menuManager.menus.MenuItem(3, "Menu", "OptionsMenu", "Back To Options Menu"));
        }
    }
}
