using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus.menuData.optionScreens
{
    public class VideoOptionMenu
    {
        private List<menuManager.menus.MenuItem> items;
        public string Path = "images/splashScreen/MenuBackgroundVideoOptions";

        private int itemNumber = 0;
        private int width = 0;
        private int height = 0;
        private int count = 0;

        public List<menuManager.menus.MenuItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public VideoOptionMenu()
        {
            items = new List<menuManager.menus.MenuItem>();
            ScreenData();
        }

        void ScreenData()
        {
            items.Add(new menuManager.menus.MenuItem(itemNumber, "Option", "Fullscreen", "Toggle Fullscreen : " + Options.Instance.IsFullscreen));
            items.Add(new menuManager.menus.MenuItem(++itemNumber, "Option", "None", ">>Graphics Quality<<"));
            
            if (Options.Instance.ResolutionAspect == Options.resAspect.FourThree)
            {
                items.Add(new menuManager.menus.MenuItem(++itemNumber, "Option", "Aspect", "Aspect Ratio - 4:3"));
                count = Resolution.Instance.DisplayModesFourThree.Count();
            }
            else if (Options.Instance.ResolutionAspect == Options.resAspect.SixteenNine)
            {
                items.Add(new menuManager.menus.MenuItem(++itemNumber, "Option", "Aspect", "Aspect Ratio - 16:9"));
                count = Resolution.Instance.DisplayModesSixteenNine.Count();
            }
            else if (Options.Instance.ResolutionAspect == Options.resAspect.SixteenTen)
            {
                items.Add(new menuManager.menus.MenuItem(++itemNumber, "Option", "Aspect", "Aspect Ratio - 16:10"));
                count = Resolution.Instance.DisplayModesSixteenTen.Count();
            }

            for (int j = 0; j < count; j++)
            {
                switch (Options.Instance.ResolutionAspect)
                {
                    case Options.resAspect.FourThree:
                        {
                            width = Resolution.Instance.DisplayModesFourThree[j].Width;
                            height = Resolution.Instance.DisplayModesFourThree[j].Height;
                            break;
                        }
                    case Options.resAspect.SixteenNine:
                        {
                            width = Resolution.Instance.DisplayModesSixteenNine[j].Width;
                            height = Resolution.Instance.DisplayModesSixteenNine[j].Height;
                            break;
                        }
                    case Options.resAspect.SixteenTen:
                        {
                            width = Resolution.Instance.DisplayModesSixteenTen[j].Width;
                            height = Resolution.Instance.DisplayModesSixteenTen[j].Height;
                            break;
                        }
                    case Options.resAspect.Other:
                        {
                            width = Resolution.Instance.DisplayModesOther[j].Width;
                            height = Resolution.Instance.DisplayModesOther[j].Height;
                            break;
                        }
                }

                items.Add(new menuManager.menus.MenuItem(++itemNumber, j, "Option", "Resolution", width + " x " + height));
            }
            

            items.Add(new menuManager.menus.MenuItem(++itemNumber, "Menu", "OptionsMenu", "Back To Options Menu"));
        }
    }
}
