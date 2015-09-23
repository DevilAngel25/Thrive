using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.managers.menuManager.menus.menuData
{
    public class TitleMenu
    {
        List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundTitle";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public TitleMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Menu", "NewGameMenu", "New Game"));
            items.Add(new menuManager.menus.MenuItem(1, "Menu", "LoadGameMenu", "Load Game"));
            items.Add(new menuManager.menus.MenuItem(2, "Menu", "OptionsMenu", "Options"));
            items.Add(new menuManager.menus.MenuItem(3, "Menu", "CreditsMenu", "Credits"));
            items.Add(new menuManager.menus.MenuItem(4, "Quit", "Quit", "Quit To Desktop"));
        }
    }
}