using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.managers
{
    // scroll wheel may cause error int out of bounds
    public class CursorManager
    {
        #region Variables
            protected ContentManager content;
            
            image.Image cursor;
            MouseState originalMouseState;
            Vector2 currentMousePosition, virtualMousePosition, delta;
            
            public bool locked = true;
            public float mouseSensitivity = 100.0f;
        #endregion

        #region Instance
            private static CursorManager instance;

            public static CursorManager Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new CursorManager();
                    }
                    return instance;
                }
            }
        #endregion

        #region Init
            public CursorManager()
            {
                cursor = new image.Image();
                //cursor.Path = "images/cursor/Cursor16x16";
                cursor.Path = "images/cursor/Cursor32x32";
                //Starts at virtual res, is converted later.
                virtualMousePosition = new Vector2(((int)Resolution.Instance.VirtualResolution.X / 2), ((int)Resolution.Instance.VirtualResolution.Y / 2));
                //center of screen
                ResetState();
            }
        #endregion

        #region LoadContent
            public void LoadContent()
            {
                cursor.LoadContent();
            }
        #endregion

        #region Methods
            public void ResetState()
            {
                Mouse.SetPosition(((int)Resolution.Instance.CurrentResolution.X / 2), ((int)Resolution.Instance.CurrentResolution.Y / 2));
                originalMouseState = Mouse.GetState();
            }
        #endregion

        #region Update
            public void Update(GameTime gameTime)
            {
                if (locked && screenManager.GameScreenManager.Instance.IsActive)
                {
                    if (InputManager.Instance.CurrentMouseState != originalMouseState)
                    {
                        // Get the movement since the last frame (current position - center of screen)
                        currentMousePosition = new Vector2(InputManager.Instance.CurrentMouseState.X, InputManager.Instance.CurrentMouseState.Y);
                        delta.X = currentMousePosition.X - originalMouseState.X;
                        delta.Y = currentMousePosition.Y - originalMouseState.Y;
                        Mouse.SetPosition((int)originalMouseState.X, (int)originalMouseState.Y);

                        virtualMousePosition += mouseSensitivity * delta * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        virtualMousePosition.X = MathHelper.Clamp(virtualMousePosition.X, 0, Resolution.Instance.VirtualResolution.X);
                        virtualMousePosition.Y = MathHelper.Clamp(virtualMousePosition.Y, 0, Resolution.Instance.VirtualResolution.Y);
                    
                        //This no longer works, no longer based off of mouse co ords.
                        //virtualMousePosition = Resolution.Instance.ScaleMouseToScreenCoordinates(virtualMousePosition);
                    
                        cursor.Position = virtualMousePosition;
                        cursor.Update(gameTime);
                    }
                }

                if (InputManager.Instance.KeyPressed(Keys.Home))
                {
                    locked = !locked;
                    Options.Instance.IsMouseVisible = !Options.Instance.IsMouseVisible;
                    Options.Instance.HasChanged = true;
                }
            }
        #endregion

        #region Mouse Get/Set
            public Vector2 VirtualMousePosition
            {
                get { return virtualMousePosition; }
                set { virtualMousePosition = value; }
            }

            public float VirtualMouseX
            {
                get { return virtualMousePosition.X; }
            }

            public float VirtualMouseY
            {
                get { return virtualMousePosition.Y; }
            }

            public float MouseSensitivity
            {
                get { return mouseSensitivity; }
                set { mouseSensitivity = value; }
            }
        #endregion

        #region Draw
            public void Draw(SpriteBatch spriteBatch)
            {
                cursor.Draw(spriteBatch);
            }
        #endregion
    }
}
