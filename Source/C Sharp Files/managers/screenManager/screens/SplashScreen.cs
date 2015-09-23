using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.managers.screenManager.screens
{
    public class SplashScreen : GameScreen
    {
        #region Variables
            public image.Image Image = new image.Image();
        #endregion

        #region LoadContent
            public override void LoadContent()
            {
                base.LoadContent();
                //adding the path this way may cause the image to load in front of the transistion
                Image.Path = "images/splashScreen/SplashScreenImage";
                Image.LoadContent();
                Image.CentreImage();
            }
        #endregion

        #region UnloadContent
            public override void UnloadContent()
            {
                base.UnloadContent();
                Image.UnloadContent();
            }
        #endregion

        #region Update
            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                Image.Update(gameTime);

                if (InputManager.Instance.KeyPressed(Keys.Enter) || InputManager.Instance.KeyPressed(Keys.Escape) || InputManager.Instance.LeftMousePressed() && InputManager.Instance.MouseOver(0.0f, Resolution.Instance.VirtualResolution.X, 0.0f, Resolution.Instance.VirtualResolution.Y))
                {
                    GameScreenManager.Instance.ChangeScreens("TitleScreen");
                    CursorManager.Instance.VirtualMousePosition = new Vector2(((int)Resolution.Instance.VirtualResolution.X / 2), ((int)Resolution.Instance.VirtualResolution.Y / 2));
                }
            }
        #endregion

        #region Draw
            public override void Draw(SpriteBatch spriteBatch)
            {
                Image.Draw(spriteBatch);
            }
        #endregion
    }
}
