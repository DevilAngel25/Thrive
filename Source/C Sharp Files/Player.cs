using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class Player
    {
        protected ContentManager content;
        SpriteAnimation vlad;

        public Player()
        {
            
        }

        public SpriteAnimation Vlad
        {
            get { return vlad; }
        }

        public void LoadContent()
        {
            content = new ContentManager(managers.screenManager.GameScreenManager.Instance.Content.ServiceProvider, "Content");

            vlad = new SpriteAnimation(content.Load<Texture2D>("images/gameplay/players/T_Vlad_Sword_Walking_48x48"));

            vlad.AddAnimation("WalkEast", 0, 48 * 0, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorth", 0, 48 * 1, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthEast", 0, 48 * 2, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthWest", 0, 48 * 3, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouth", 0, 48 * 4, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthEast", 0, 48 * 5, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthWest", 0, 48 * 6, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkWest", 0, 48 * 7, 48, 48, 8, 0.1f);

            vlad.AddAnimation("IdleEast", 0, 48 * 0, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorth", 0, 48 * 1, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthEast", 0, 48 * 2, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthWest", 0, 48 * 3, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouth", 0, 48 * 4, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthEast", 0, 48 * 5, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthWest", 0, 48 * 6, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleWest", 0, 48 * 7, 48, 48, 1, 0.2f);

            vlad.Position = new Vector2(100, 100);
            vlad.DrawOffset = new Vector2(-24, -38);
            vlad.CurrentAnimation = "WalkEast";
            vlad.IsAnimating = true;
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime, map.tile.TileMap myMap)
        {
            Vector2 moveVector = Vector2.Zero;
            Vector2 moveDir = Vector2.Zero;
            string animation = "";

            if (managers.InputManager.Instance.KeyDown(Keys.W) && managers.InputManager.Instance.KeyDown(Keys.A))
            {
                moveDir = new Vector2(-2, -1);
                animation = "WalkNorthWest";
                moveVector += new Vector2(-2, -1);
            }
            else if (managers.InputManager.Instance.KeyDown(Keys.W) && managers.InputManager.Instance.KeyDown(Keys.D))
            {
                moveDir = new Vector2(2, -1);
                animation = "WalkNorthEast";
                moveVector += new Vector2(2, -1);
            }
            else if (managers.InputManager.Instance.KeyDown(Keys.D) && managers.InputManager.Instance.KeyDown(Keys.S))
            {
                moveDir = new Vector2(2, 1);
                animation = "WalkSouthEast";
                moveVector += new Vector2(2, 1);
            }
            else if (managers.InputManager.Instance.KeyDown(Keys.S) && managers.InputManager.Instance.KeyDown(Keys.A))
            {
                moveDir = new Vector2(-2, 1);
                animation = "WalkSouthWest";
                moveVector += new Vector2(-2, 1);
            }
            else if (managers.InputManager.Instance.KeyDown(Keys.W))
            {
                moveDir = new Vector2(0, -1);
                animation = "WalkNorth";
                moveVector += new Vector2(0, -1);
            }
            else if (managers.InputManager.Instance.KeyDown(Keys.D))
            {
                moveDir = new Vector2(2, 0);
                animation = "WalkEast";
                moveVector += new Vector2(2, 0);
            }
            else if (managers.InputManager.Instance.KeyDown(Keys.S))
            {
                moveDir = new Vector2(0, 1);
                animation = "WalkSouth";
                moveVector += new Vector2(0, 1);
            }
            else if (managers.InputManager.Instance.KeyDown(Keys.A))
            {
                moveDir = new Vector2(-2, 0);
                animation = "WalkWest";
                moveVector += new Vector2(-2, 0);
            }

            if (myMap.GetCellAtWorldPoint(vlad.Position + moveDir).Walkable == false)
            {
                moveDir = Vector2.Zero;
            }

            if (Math.Abs(myMap.GetOverallHeight(vlad.Position) - myMap.GetOverallHeight(vlad.Position + moveDir)) > 10)
            {
                moveDir = Vector2.Zero;
            }

            if (moveDir.Length() != 0)
            {
                vlad.MoveBy((int)moveDir.X, (int)moveDir.Y);
                if (vlad.CurrentAnimation != animation)
                    vlad.CurrentAnimation = animation;
            }
            else
            {
                vlad.CurrentAnimation = "Idle" + vlad.CurrentAnimation.Substring(4);
            }

            float vladX = MathHelper.Clamp(
                vlad.Position.X, 64 + vlad.DrawOffset.X, Camera.Instance.WorldWidth);
            float vladY = MathHelper.Clamp(
                vlad.Position.Y, 132 + vlad.DrawOffset.Y, Camera.Instance.WorldHeight);

            vlad.Position = new Vector2(vladX, vladY);

            Vector2 testPosition = Camera.Instance.WorldToScreen(vlad.Position);

            if (testPosition.X < 100)
            {
                Camera.Instance.Move(new Vector2(testPosition.X - 100, 0));
            }

            if (testPosition.X > (Camera.Instance.ViewWidth - 100))
            {
                Camera.Instance.Move(new Vector2(testPosition.X - (Camera.Instance.ViewWidth - 100), 0));
            }

            if (testPosition.Y < 100)
            {
                Camera.Instance.Move(new Vector2(0, testPosition.Y - 100));
            }

            if (testPosition.Y > (Camera.Instance.ViewHeight - 100))
            {
                Camera.Instance.Move(new Vector2(0, testPosition.Y - (Camera.Instance.ViewHeight - 100)));
            }
            
            vlad.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
