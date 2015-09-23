using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.managers.screenManager.screens
{
    public class GameScreen
    {
        protected ContentManager content;

        #region LoadContent
            public virtual void LoadContent()
            {
                content = new ContentManager(GameScreenManager.Instance.Content.ServiceProvider, "Content");
                CursorManager.Instance.LoadContent();
            }
        #endregion

        #region UnloadContent
            public virtual void UnloadContent()
            {
                content.Unload();
            }
        #endregion

        #region Update
            public virtual void Update(GameTime gameTime)
            {
                InputManager.Instance.Update();
                CursorManager.Instance.Update(gameTime);
            }
        #endregion

        #region Draw
            public virtual void Draw(SpriteBatch spriteBatch)
            {
                CursorManager.Instance.Draw(spriteBatch);
            }
        #endregion
    }
}
