using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class SpriteAnimation
    {
        Texture2D t2dTexture;
        Color colorTint = Color.White;

        Vector2 v2Position = new Vector2(0, 0);
        Vector2 v2LastPosition = new Vector2(0, 0);
        Vector2 v2Center;

        Dictionary<string, FrameAnimation> faAnimations = new Dictionary<string, FrameAnimation>();

        string sCurrentAnimation = null;
        bool bAnimating = true;
        bool bRotateByPosition = false;
        float fRotation = 0f;
        int iWidth;
        int iHeight;

        public Vector2 DrawOffset { get; set; }
        public float DrawDepth { get; set; }

        public Vector2 Position
        {
            get { return v2Position; }
            set
            {
                v2LastPosition = v2Position;
                v2Position = value;
                UpdateRotation();
            }
        }

        public int X
        {
            get { return (int)v2Position.X; }
            set
            {
                v2LastPosition.X = v2Position.X;
                v2Position.X = value;
                UpdateRotation();
            }
        }

        public int Y
        {
            get { return (int)v2Position.Y; }
            set
            {
                v2LastPosition.Y = v2Position.Y;
                v2Position.Y = value;
                UpdateRotation();
            }
        }

        public int Width
        {
            get { return iWidth; }
        }

        public int Height
        {
            get { return iHeight; }
        }

        public bool AutoRotate
        {
            get { return bRotateByPosition; }
            set { bRotateByPosition = value; }
        }

        public float Rotation
        {
            get { return fRotation; }
            set { fRotation = value; }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle(X, Y, iWidth, iHeight); }
        }

        public Texture2D Texture
        {
            get { return t2dTexture; }
        }

        public Color Tint
        {
            get { return colorTint; }
            set { colorTint = value; }
        }

        public bool IsAnimating
        {
            get { return bAnimating; }
            set { bAnimating = value; }
        }

        public FrameAnimation CurrentFrameAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(sCurrentAnimation))
                    return faAnimations[sCurrentAnimation];
                else
                    return null;
            }
        }

        public string CurrentAnimation
        {
            get { return sCurrentAnimation; }
            set
            {
                if (faAnimations.ContainsKey(value))
                {
                    sCurrentAnimation = value;
                    faAnimations[sCurrentAnimation].CurrentFrame = 0;
                    faAnimations[sCurrentAnimation].PlayCount = 0;
                }
            }
        }

        public SpriteAnimation(Texture2D Texture)
        {
            t2dTexture = Texture;
            DrawOffset = Vector2.Zero;
            DrawDepth = 0.0f;
        }

        void UpdateRotation()
        {
            if (bRotateByPosition)
            {
                fRotation = (float)Math.Atan2(v2Position.Y - v2LastPosition.Y, v2Position.X - v2LastPosition.X);
            }
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames,
           float FrameLength, string NextAnimation)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength, NextAnimation));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }

        public FrameAnimation GetAnimationByName(string Name)
        {
            if (faAnimations.ContainsKey(Name))
            {
                return faAnimations[Name];
            }
            else
            {
                return null;
            }
        }

        public void MoveBy(int x, int y)
        {
            v2LastPosition = v2Position;
            v2Position.X += x;
            v2Position.Y += y;
            UpdateRotation();
        }

        public void Update(GameTime gameTime)
        {
            // Don't do anything if the sprite is not animating
            if (bAnimating)
            {
                // If there is not a currently active animation
                if (CurrentFrameAnimation == null)
                {
                    // Make sure we have an animation associated with this sprite
                    if (faAnimations.Count > 0)
                    {
                        // Set the active animation to the first animation
                        // associated with this sprite
                        string[] sKeys = new string[faAnimations.Count];
                        faAnimations.Keys.CopyTo(sKeys, 0);
                        CurrentAnimation = sKeys[0];
                    }
                    else
                    {
                        return;
                    }
                }

                // Run the Animation's update method
                CurrentFrameAnimation.Update(gameTime);

                // Check to see if there is a "followup" animation named for this animation
                if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
                {
                    // If there is, see if the currently playing animation has
                    // completed a full animation loop
                    if (CurrentFrameAnimation.PlayCount > 0)
                    {
                        // If it has, set up the next animation
                        CurrentAnimation = CurrentFrameAnimation.NextAnimation;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset)
        {
            if (bAnimating)
                spriteBatch.Draw(t2dTexture,
                    Camera.Instance.WorldToScreen(v2Position) + v2Center + DrawOffset + new Vector2(XOffset, YOffset),
                    CurrentFrameAnimation.FrameRectangle, colorTint,
                    fRotation, v2Center, 1f, SpriteEffects.None, DrawDepth);
        }
    }
}
