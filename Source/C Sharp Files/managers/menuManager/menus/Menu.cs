using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.managers.menuManager.menus
{
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string Axis, Effects;
        public List<MenuItem> Items;
        public int ItemNumber;
        public image.Image MenuBackground;

        string id;

        public string ID
        {
            get { return id; }
            set { id = value; OnMenuChange(this, null); }
        }

        public void Transition(float alpha)
        {
            foreach (MenuItem item in Items)
            {
                item.Image.IsActive = true;
                item.Image.Alpha = alpha;
                if (alpha == 0.0f)  { item.Image.FadeEffect.Increase = true; }
                else                { item.Image.FadeEffect.Increase = false; }
            }
        }

        public void AlignMenuItems()
        {
            Vector2 dimentions = Vector2.Zero;
            foreach (MenuItem item in Items) { dimentions += new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height); }

            dimentions = new Vector2((screenManager.GameScreenManager.Instance.Dimensions.X - dimentions.X) / 2, screenManager.GameScreenManager.Instance.Dimensions.Y / 3);//(screenManager.GameScreenManager.Instance.Dimensions.Y - dimentions.Y) / 2);
            
            foreach (MenuItem item in Items) 
            {
                if (Axis == "X") { item.Image.Position = new Vector2(dimentions.X, (screenManager.GameScreenManager.Instance.Dimensions.Y - item.Image.SourceRect.Height) / 2); }
                else if (Axis == "Y") { item.Image.Position = new Vector2((screenManager.GameScreenManager.Instance.Dimensions.X - item.Image.SourceRect.Width) / 2, dimentions.Y); }

                dimentions += new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height);
            }
        }

        public Menu()
        {
            id = String.Empty;
            ItemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            //Effects = "image.effects.FadeEffect";
            Items = new List<MenuItem>();

            MenuBackground = new image.Image();
            MenuBackground.Path = "images/splashScreen/MenuBackgroundTitle";
        }

        public void LoadContent()
        {
            string[] split = Effects.Split(':');
            foreach (MenuItem item in Items)
            {
                item.Image.LoadContent();
                foreach (string s in split) { item.Image.ActivateEffect(s); }
            }
            AlignMenuItems();
            MenuBackground.LoadContent();
            MenuBackground.CentreImage();
        }

        public void UnloadContent()
        {
            foreach (MenuItem item in Items) { item.Image.UnloadContent(); }
            
            if (MenuBackground.content != null)
            {
                MenuBackground.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            MenuBackground.Update(gameTime);

            if(Axis == "X") 
            {
                if (InputManager.Instance.KeyPressed(Keys.Right) || InputManager.Instance.ScrollDown()) { ItemNumber++; }
                else if (InputManager.Instance.KeyPressed(Keys.Left) || InputManager.Instance.ScrollUp()) { ItemNumber--; }
            }
            else if(Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down) || InputManager.Instance.ScrollDown()) { ItemNumber++; }
                else if (InputManager.Instance.KeyPressed(Keys.Up) || InputManager.Instance.ScrollUp()) { ItemNumber--; }
            }

            //mouse over
            foreach (MenuItem item in Items)
            {
                if (InputManager.Instance.MouseOver(item.Image.Position.X, item.Image.Position.X + item.Image.SourceRect.Right, item.Image.Position.Y, item.Image.Position.Y + item.Image.SourceRect.Bottom))
                {
                    item.Image.Color = Color.White;
                    ItemNumber = item.ItemNumber; 
                }
                else 
                {
                    if (item.LinkID == "Resolution") 
                    { 
                        if ((item.ResolutionNumber) == managers.menuManager.MenuManager.Instance.activeOption) { item.Image.Color = Color.Green; }
                        else { item.Image.Color = Color.LightGray; }
                    }
                    else 
                    {
                        item.Image.Color = Color.LightGray;
                    }
                }
            }

            if (ItemNumber < 0) { ItemNumber = 0; }
            else if (ItemNumber > Items.Count - 1) { ItemNumber = Items.Count - 1; }

            //arrow keys
            for (int i = 0; i < Items.Count; i++)
            {
                if (i == ItemNumber)
                { 
                    //update is active

                    //This is more of a hack job to get colors to change, this needs to be changed, improved.

                    Items[i].Image.IsActive = true; //the image (text) is active (selected). this will make the text / image fade in and out.

                    if (Items[i].LinkID == "Resolution")    //if the item has a resolution number.
                    {
                        if ((Items[i].ResolutionNumber) == managers.menuManager.MenuManager.Instance.activeOption) { Items[i].Image.Color = Color.Green; } // if the resolution number (+1 to compensate for title option) of the item is the same as the option selected, turn green
                        else { Items[i].Image.Color = Color.White; } // or it isn't active so blue.
                    }
                    else { Items[i].Image.Color = Color.White; }// if no res number, set color to blue of selected.
                }
                else 
                { 
                    Items[i].Image.IsActive = false;
                    if (Items[i].LinkID == "Resolution")
                    {
                        if ((Items[i].ResolutionNumber) == managers.menuManager.MenuManager.Instance.activeOption) { Items[i].Image.Color = Color.Green; }
                        else { Items[i].Image.Color = Color.LightGray; }
                    }
                    else { Items[i].Image.Color = Color.LightGray; }
                }

                Items[i].Image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MenuBackground.Draw(spriteBatch);

            foreach (MenuItem item in Items)
            {
                item.Image.Draw(spriteBatch);
            }
        }
    }
}
