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
    public class InputManager
    {
        #region Variables
            protected ContentManager content;

            KeyboardState currentKeyState, prevKeyState;
            MouseState currentMouseState, prevMouseState;

            Vector2 currentMousePosition;
            int currentWheelState, prevWheelState;
        #endregion

        #region Instance
            private static InputManager instance;

            public static InputManager Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new InputManager();
                    }
                    return instance;
                }
            }
        #endregion

        #region Init
            public InputManager()
            {
                currentMousePosition = Vector2.Zero;
                
                currentWheelState = 0;
                prevWheelState = 0;
            }
        #endregion

        #region Update
            public void Update()
            {
                prevKeyState = currentKeyState;
                prevMouseState = currentMouseState;
                prevWheelState = currentWheelState;

                if (!screenManager.GameScreenManager.Instance.IsTransitioning) { currentKeyState = Keyboard.GetState(); }
                if (!screenManager.GameScreenManager.Instance.IsTransitioning) { currentMouseState = Mouse.GetState(); }
                if (!screenManager.GameScreenManager.Instance.IsTransitioning) { currentWheelState = currentMouseState.ScrollWheelValue; }
            }
        #endregion

        #region Mouse Get/Set
            public MouseState CurrentMouseState
            {
                get { return currentMouseState; }
            }

            public float MouseX
            {
                get { return currentMousePosition.X; }
            }

            public float MouseY
            {
                get { return currentMousePosition.Y; }
            }
        #endregion

        #region Keyboard Methods
            public bool KeyPressed(params Keys[] keys)
            {
                foreach (Keys key in keys)
                {
                    if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key)) { return true; }
                }
                return false;
            }

            public bool KeyReleased(params Keys[] keys)
            {
                foreach (Keys key in keys)
                {
                    if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key)) { return true; }
                }
                return false;
            }

            public bool KeyDown(params Keys[] keys)
            {
                foreach (Keys key in keys)
                {
                    if (currentKeyState.IsKeyDown(key)) { return true; }
                }
                return false;
            }
        #endregion

        #region Mouse Methods
            public bool LeftMousePressed()
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) { return true; }
                return false;
            }

            public bool LeftMouseReleased()
            {
                if (currentMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed) { return true; }
                return false;
            }

            public bool LeftMouseDown()
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed) { return true; }
                return false;
            }

            public bool RightMousePressed()
            {
                if (currentMouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released) { return true; }
                return false;
            }

            public bool RightMouseReleased()
            {
                if (currentMouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed) { return true; }
                return false;
            }

            public bool RightMouseDown()
            {
                if (currentMouseState.RightButton == ButtonState.Pressed) { return true; }
                return false;
            }

            public bool MiddleMousePressed()
            {
                if (currentMouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton == ButtonState.Released) { return true; }
                return false;
            }

            public bool MiddleMouseReleased()
            {
                if (currentMouseState.MiddleButton == ButtonState.Released && prevMouseState.MiddleButton == ButtonState.Pressed) { return true; }
                return false;
            }

            public bool MiddleMouseDown()
            {
                if (currentMouseState.MiddleButton == ButtonState.Pressed) { return true; }
                return false;
            }

            public bool ScrollUp()
            {
                if (prevWheelState < currentWheelState) { return true; }
                return false;
            }

            public bool ScrollDown()
            {
                if (prevWheelState > currentWheelState) { return true; }
                return false;
            }

            public bool MouseOver(float startX, float endX, float startY, float endY)
            {
                if (CursorManager.Instance.locked && screenManager.GameScreenManager.Instance.IsActive)
                {
                    Vector2 virtualMousePosition = CursorManager.Instance.VirtualMousePosition;
                    if (virtualMousePosition.X >= startX && virtualMousePosition.X <= endX && virtualMousePosition.Y >= startY && virtualMousePosition.Y <= endY) { return true; }
                }
                return false;
            }
        #endregion
    }
}
