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
    public class GameplayScreen : GameScreen
    {
        #region Variables
            Player player;
            map.Map map;
            Viewport view;
        #endregion

        #region Init
            public GameplayScreen()
            {
                player = new Player();
                map = new map.Map();
                view = managers.screenManager.GameScreenManager.Instance.GraphicsDevice.Viewport;
            }
        #endregion

        #region LoadContent
            public override void LoadContent()
            {
                base.LoadContent();
                map.LoadContent();
                player.LoadContent();
            }
        #endregion

        #region UnloadContent
            public override void UnloadContent()
            {
                base.UnloadContent();
                player.UnloadContent();
                map.UnloadContent();
            }
        #endregion

        #region Update
            public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
            {
                base.Update(gameTime);
                player.Update(gameTime, map.MyMap);
                map.Update(gameTime);

                if (InputManager.Instance.KeyPressed(Keys.Escape))
                {
                    GameScreenManager.Instance.ChangeScreens("TitleScreen");
                }
            }
        #endregion

        #region Draw
            public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);
                map.Draw(spriteBatch, player.Vlad);
            }
        #endregion
    }
}
