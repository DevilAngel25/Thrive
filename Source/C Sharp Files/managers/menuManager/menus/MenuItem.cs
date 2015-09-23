using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.managers.menuManager.menus
{
    public class MenuItem
    {
        public string LinkType = "None", LinkID = "None";
        public int ItemNumber = -1;
        public int ResolutionNumber = -1;
        public image.Image Image = new image.Image();

        public MenuItem(int itemNumber, string text)
        {
            ItemNumber = itemNumber;
            Image.Text = text;
        }

        public MenuItem(int itemNumber, string linkType, string linkID, string text)
        {
            ItemNumber = itemNumber;
            LinkType = linkType;
            LinkID = linkID;
            Image.Text = text;
        }

        public MenuItem(int itemNumber, int resNumber, string linkType, string linkID, string text)
        {
            ItemNumber = itemNumber;
            ResolutionNumber = resNumber;
            LinkType = linkType;
            LinkID = linkID;
            Image.Text = text;
        }
    }
}
