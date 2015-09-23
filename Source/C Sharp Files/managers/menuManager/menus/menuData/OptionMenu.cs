using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.managers.menuManager.menus.menuData
{
    public class OptionMenu
    {
        private List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundOptions";

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public OptionMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(0, "Menu", "AudioOptionMenu", "Audio"));
            items.Add(new menuManager.menus.MenuItem(1, "Menu", "VideoOptionMenu", "Video"));
            items.Add(new menuManager.menus.MenuItem(2, "Menu", "CameraOptionMenu", "Camera"));
            items.Add(new menuManager.menus.MenuItem(3, "Menu", "MouseOptionMenu", "Mouse"));
            items.Add(new menuManager.menus.MenuItem(4, "Menu", "KeyboardOptionMenu", "KeyBoard"));
            items.Add(new menuManager.menus.MenuItem(5, "Menu", "ControllerOptionMenu", "Controller"));
            items.Add(new menuManager.menus.MenuItem(6, "Menu", "TitleMenu", "Back To Main Menu"));
        }
    }
}