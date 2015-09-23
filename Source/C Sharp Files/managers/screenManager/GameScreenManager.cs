using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.managers.screenManager
{
    public class GameScreenManager
    {
        #region Variables
            public image.Image Image;
            public Vector2 Dimensions;
            public ContentManager Content { private set; get; }
            public GraphicsDevice GraphicsDevice;
            public SpriteBatch SpriteBatch;
            public bool IsTransitioning { get; private set; }
            public bool IsActive = true;

            screens.GameScreen currentScreen, newScreen;
        #endregion

        #region Instance
            private static GameScreenManager instance;

            public static GameScreenManager Instance
            {
                get
                {
                    if (instance == null) 
                    {
                        instance = new GameScreenManager();
                    }
                    return instance;
                }
            }
        #endregion

        #region Init
            public GameScreenManager()
            {
                Dimensions = Resolution.Instance.VirtualResolution;
                currentScreen = new screens.SplashScreen();
            
                Image = new image.Image();
                Image.Path = "screenManager/FadeImage";
                Image.Effects = "image.effects.FadeEffect";
            }
        #endregion

        #region Methods
            public void ChangeScreens(string screenName)
            {
                newScreen = (screens.GameScreen)Activator.CreateInstance(Type.GetType("MyGame.managers.screenManager.screens." + screenName));
                Image.IsActive = true;
                Image.FadeEffect.Increase = true;
                Image.Alpha = 0.0f;
                IsTransitioning = true;
            }

            void Transition(GameTime gameTime)
            {
                if (IsTransitioning)
                {
                    Image.Update(gameTime);
                    if (Image.Alpha == 1.0f)
                    {
                        currentScreen.UnloadContent();
                        currentScreen = newScreen;
                        currentScreen.LoadContent();
                    }
                    else if (Image.Alpha == 0.0f)
                    {
                        Image.IsActive = false;
                        IsTransitioning = false;
                    }
                }
            }
        #endregion

        #region LoadContent
            public void LoadContent(ContentManager Content)
            {
                this.Content = new ContentManager(Content.ServiceProvider, "Content");
                currentScreen.LoadContent();

                //size of fade effect image
                Image.Scale = Resolution.Instance.VirtualResolution;
                Image.LoadContent();
            
            }
        #endregion

        #region UnloadContent
            public void UnloadContent()
            {
                currentScreen.UnloadContent();
                Image.UnloadContent();
            }
        #endregion

        #region Update
            public void Update(GameTime gameTime)
            {
                currentScreen.Update(gameTime);
                Transition(gameTime);
            }
        #endregion

        #region Draw
            public void Draw(SpriteBatch spriteBatch)
            {
                currentScreen.Draw(spriteBatch);
                if (IsTransitioning) { Image.Draw(spriteBatch); }
            }
        #endregion
    }
}
