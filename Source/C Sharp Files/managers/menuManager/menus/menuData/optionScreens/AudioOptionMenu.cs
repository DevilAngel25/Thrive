using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData.optionScreens
{
    public class AudioOptionMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundAudioOptions";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public AudioOptionMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Option", "None", ">>Music Volume<<"));
            items.Add(new menuManager.menus.MenuItem(1, "Option", "None", ">>Ambient Volume<<"));
            items.Add(new menuManager.menus.MenuItem(2, "Option", "None", ">>Sound Effects Volume<<"));
            items.Add(new menuManager.menus.MenuItem(3, "Menu", "OptionsMenu", "Back To Options Menu"));
        }
    }
}
