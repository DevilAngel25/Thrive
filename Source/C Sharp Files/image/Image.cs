using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.image
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path, Effects;
        public Vector2 Position, Scale;
        public Texture2D Texture;
        public bool IsActive;
        public Color Color;
        public effects.FadeEffect FadeEffect;
        public Rectangle SourceRect;
        public Texture2D RenderedTexture;
        public ContentManager content;

        Vector2 dimensions;
        Vector2 origin;
        RenderTarget2D renderTarget;
        SpriteFont font;

        Dictionary<string, effects.ImageEffect> effectList;

        //public effects.SpriteSheetEffect SpriteSheetEffect;

        public Image()
        {
            Path = Text = Effects = String.Empty;
            FontName = "fonts/Arial";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, effects.ImageEffect>();
            Color = Color.White;
        }

        public void CentreImage()
        {
            Vector2 location = Vector2.Zero;

            location += new Vector2(SourceRect.Width, SourceRect.Height);
            location = new Vector2((managers.screenManager.GameScreenManager.Instance.Dimensions.X - location.X) / 2, (managers.screenManager.GameScreenManager.Instance.Dimensions.Y - location.Y) / 2);
            Position = new Vector2(location.X, (managers.screenManager.GameScreenManager.Instance.Dimensions.Y - SourceRect.Height) / 2);
            location += new Vector2(SourceRect.Width, SourceRect.Height);
        }

        void SetEffect<T>(ref T effect)
        {
            if (effect == null) { effect = (T)Activator.CreateInstance(typeof(T)); }
            else
            {
                (effect as effects.ImageEffect).IsActive = true;
                var obj = this;
                (effect as effects.ImageEffect).LoadContent(ref obj);
            }
            //if resolution is changed to much in fullscreen error here, key already exists.
            //effectList.Add(effect.GetType().ToString().Replace("MyGame.", ""), (effect as effects.ImageEffect));
            effectList[effect.GetType().ToString().Replace("MyGame.", "")] = (effect as effects.ImageEffect);
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public void StoreEffects()
        {
            Effects = String.Empty;
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive) { Effects += effect.Key + ":"; }
            }
            if (Effects != String.Empty) { 
                Effects.Remove(Effects.Length - 1); }
        }

        public void RestoreEffects()
        {
            foreach (var effect in effectList) { 
                DeactivateEffect(effect.Key); }

            string[] split = Effects.Split(':');
            foreach (string s in split) { ActivateEffect(s); }
        }

        public void LoadContent()
        {
            content = new ContentManager(managers.screenManager.GameScreenManager.Instance.Content.ServiceProvider, "Content");
            
            ReLoadTexture();

            SetEffect<effects.FadeEffect>(ref FadeEffect);
            //SetEffect<effects.SpriteSheetEffect>(ref SpriteSheetEffect);

            if (Effects != String.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (string item in split) { ActivateEffect(item); }
            }
        }
        
        public void ReLoadTexture()
        {
            if (Path != String.Empty) { Texture = content.Load<Texture2D>(Path); }

            font = content.Load<SpriteFont>(FontName);

            dimensions = Vector2.Zero;

            if (Texture != null) { dimensions.X += Texture.Width; }
            else { dimensions.X += font.MeasureString(Text).X; }

            if (Texture != null) { dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y); }
            else { dimensions.Y = font.MeasureString(Text).Y; }

            if (SourceRect == Rectangle.Empty) { SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y); }

            renderTarget = new RenderTarget2D(managers.screenManager.GameScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            managers.screenManager.GameScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            managers.screenManager.GameScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            managers.screenManager.GameScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null) { managers.screenManager.GameScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White); }
            else { managers.screenManager.GameScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White); }
            managers.screenManager.GameScreenManager.Instance.SpriteBatch.End();


            RenderedTexture = renderTarget;

            managers.screenManager.GameScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList) 
            {
                DeactivateEffect(effect.Key);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList) 
            { 
                if (effect.Value.IsActive) { effect.Value.Update(gameTime); } 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(RenderedTexture, Position + origin, SourceRect, Color * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
            
            if (renderTarget.IsContentLost)
            {
                managers.screenManager.GameScreenManager.Instance.SpriteBatch.End();
                ReLoadTexture();
                managers.screenManager.GameScreenManager.Instance.SpriteBatch.Begin();
            }
        }
    }
}
