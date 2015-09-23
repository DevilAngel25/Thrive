using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MyGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //public enum GameState { Splashscreen, MainMenu, NewgameMenu, LoadgameMenu, OptionsMenu, KeyboardMenu, ControllerMenu, MouseMenu, AudioMenu, VideoMenu, CameraMenu, Credits, Playing };
        //GameState gameState;

        //Vector2 imagePos = new Vector2(0, 0);
        //Texture2D imageSplash;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //gameState = GameState.Splashscreen;

            Resolution.Instance.Initialize(ref graphics);
            //detect current monitor resolution/aspect ratio, set acordingly
            Resolution.Instance.SetVirtualResolution(Options.Instance.VirtualWidth, Options.Instance.VirtualHeight);
            Resolution.Instance.SetResolution(Options.Instance.CurrentResolution.Width, Options.Instance.CurrentResolution.Height, Options.Instance.IsFullscreen);
            Resolution.Instance.ScaleMouse();

            Camera.Instance.Initialize(ref graphics);
            Camera.Instance.Zoom = 1.0f;
            Camera.Instance.Position = new Vector2((int)Resolution.Instance.VirtualResolution.X / 2, (int)Resolution.Instance.VirtualResolution.Y / 2);
            
            //IsMouseVisible = Options.Instance.IsMouseVisible;
            //graphics.PreferredBackBufferWidth = (int)managers.screenManager.GameScreenManager.Instance.Dimensions.X;
            //graphics.PreferredBackBufferHeight = (int)managers.screenManager.GameScreenManager.Instance.Dimensions.Y;
            //graphics.IsFullScreen = Options.Instance.IsFullscreen;
            //graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            managers.screenManager.GameScreenManager.Instance.GraphicsDevice = GraphicsDevice;
            managers.screenManager.GameScreenManager.Instance.SpriteBatch = spriteBatch;
            managers.screenManager.GameScreenManager.Instance.LoadContent(Content);
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            managers.screenManager.GameScreenManager.Instance.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //check if game is active (not minimized)
            if (this.IsActive) { managers.screenManager.GameScreenManager.Instance.IsActive = true; }
            else { managers.screenManager.GameScreenManager.Instance.IsActive = false; }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || managers.menuManager.MenuManager.Instance.Quit)
                this.Exit();

            if (Options.Instance.HasChanged)
            {
                IsMouseVisible = Options.Instance.IsMouseVisible;
                Options.Instance.HasChanged = false;
            }

            Camera.Instance.Update(gameTime);
            managers.screenManager.GameScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            Resolution.Instance.BeginDraw();
            
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Camera.Instance.GetViewTransformationMatrix());
            managers.screenManager.GameScreenManager.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
