using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.managers.screenManager.screens
{
    public class TitleScreen : GameScreen
    {
        #region LoadContent
            public override void LoadContent()
            {
                base.LoadContent();           
                managers.menuManager.MenuManager.Instance.LoadContent("TitleMenu");         
            }
        #endregion

        #region UnloadContent
            public override void UnloadContent()
            {
                base.UnloadContent(); 
                managers.menuManager.MenuManager.Instance.UnloadContent(); 
            }
        #endregion

        #region Update
            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                managers.menuManager.MenuManager.Instance.Update(gameTime);
            
                if (InputManager.Instance.KeyPressed(Keys.End))
                {
                    Options.Instance.IsMouseVisible = !Options.Instance.IsMouseVisible;
                    Options.Instance.HasChanged = true;
                }
            }
        #endregion

        #region Draw
            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);
                managers.menuManager.MenuManager.Instance.Draw(spriteBatch);
            }
        #endregion
    }
}
