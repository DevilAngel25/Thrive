using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData.optionScreens
{
    public class CameraOptionMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundCameraOptions";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public CameraOptionMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Option", "None", ">>Edge Scrolling<<"));
            items.Add(new menuManager.menus.MenuItem(1, "Option", "None", ">>Edge Scrooling Speed<<"));
            items.Add(new menuManager.menus.MenuItem(2, "Menu", "OptionsMenu", "Back To Options Menu"));
        }
    }
}
