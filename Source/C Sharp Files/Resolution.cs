using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class Resolution
    {

        private GraphicsDeviceManager graphicsDevice = null;
        
        private Viewport viewport;
        private float scaleX;
        private float scaleY;
        private static Matrix scaleMatrix;
        private bool dirtyMatrix = true;
        private bool fullScreen = false;
        private Vector2 virtualMousePosition = new Vector2();

        public Vector2 CurrentResolution = new Vector2();
        public Vector2 VirtualResolution = new Vector2();

        public List<DisplayMode> DisplayModesFourThree = new List<DisplayMode>();
        public List<DisplayMode> DisplayModesSixteenNine = new List<DisplayMode>();
        public List<DisplayMode> DisplayModesSixteenTen = new List<DisplayMode>();
        public List<DisplayMode> DisplayModesOther = new List<DisplayMode>();

        private static Resolution instance;

        public static Resolution Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Resolution();
                }
                return instance;
            }
        }

        public void Initialize(ref GraphicsDeviceManager device)
        {
            graphicsDevice = device;
            CurrentResolution.X = graphicsDevice.PreferredBackBufferWidth;
            CurrentResolution.Y = graphicsDevice.PreferredBackBufferHeight;
            dirtyMatrix = true;

            foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                double aspect = Math.Round(dm.AspectRatio, 2);
                SurfaceFormat color = SurfaceFormat.Color;

                if (aspect == 1.33 && dm.Format == color) { DisplayModesFourThree.Add(dm); }
                else if (aspect == 1.77 && dm.Format == color || aspect == 1.78 && dm.Format == color) { DisplayModesSixteenNine.Add(dm); }
                else if (aspect == 1.6 && dm.Format == color) { DisplayModesSixteenTen.Add(dm); }
                else if(dm.Format == color) { DisplayModesOther.Add(dm); }
            }

            ApplyResolutionSettings();
        }

        public float ScaleX
        {
            get { return scaleX; }
        }

        public float ScaleY
        {
            get { return scaleY; }
        }

        public Matrix GetTransformationMatrix()
        {
            if (dirtyMatrix) { RecreateScaleMatrix(); }
            return scaleMatrix;
        }

        public void SetResolution(int width, int height, bool fullSc)
        {
            CurrentResolution.X = width;
            CurrentResolution.Y = height;
            fullScreen = fullSc;
            ApplyResolutionSettings();
        }

        public void SetVirtualResolution(int width, int height)
        {
            VirtualResolution.X = width;
            VirtualResolution.Y = height;
            dirtyMatrix = true;
        }

        public void ScaleMouse()
        {
            ResetViewport();
            scaleX = (float)viewport.Width / (int)VirtualResolution.X;
            scaleY = (float)viewport.Height / (int)VirtualResolution.Y;
            dirtyMatrix = true;
        }

        public float getVirtualAspectRatio()
        {
            return VirtualResolution.X / VirtualResolution.Y;
        }

        private void ApplyResolutionSettings()
        {

#if XBOX360
           _FullScreen = true;
#endif
           
            if (fullScreen == false)
            {
                if (((int)CurrentResolution.X <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && ((int)CurrentResolution.Y <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphicsDevice.PreferredBackBufferWidth = (int)CurrentResolution.X;
                    graphicsDevice.PreferredBackBufferHeight = (int)CurrentResolution.Y;
                    graphicsDevice.IsFullScreen = fullScreen;
                    graphicsDevice.ApplyChanges();
                }
            }
            else
            {
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    if ((dm.Width == (int)CurrentResolution.X) && (dm.Height == (int)CurrentResolution.Y))
                    {
                        graphicsDevice.PreferredBackBufferWidth = (int)CurrentResolution.X;
                        graphicsDevice.PreferredBackBufferHeight = (int)CurrentResolution.Y;
                        graphicsDevice.IsFullScreen = fullScreen;
                        graphicsDevice.ApplyChanges();
                    }
                }
            }

            dirtyMatrix = true;

            CurrentResolution.X = graphicsDevice.PreferredBackBufferWidth;
            CurrentResolution.Y = graphicsDevice.PreferredBackBufferHeight;
        }

        public void FullViewport()
        {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = (int)CurrentResolution.X;
            vp.Height = (int)CurrentResolution.Y;
            vp.MinDepth = 0;
            vp.MaxDepth = 1;
            //BUG. the viewport is invalid at 1680, 1050 when partialy off screen.
            graphicsDevice.GraphicsDevice.Viewport = vp;
        }

        public void BeginDraw()
        {
            FullViewport();
            graphicsDevice.GraphicsDevice.Clear(Color.Black);
            ResetViewport();
            graphicsDevice.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        private void RecreateScaleMatrix()
        {
            dirtyMatrix = false;
            scaleMatrix = Matrix.CreateScale(   (float)graphicsDevice.GraphicsDevice.Viewport.Width / (int)VirtualResolution.X, 
                                                (float)graphicsDevice.GraphicsDevice.Viewport.Width / (int)VirtualResolution.X, 
                                                1.0f);
        }

        public void ResetViewport()
        {
            float targetAspectRatio = getVirtualAspectRatio();

            int width = graphicsDevice.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;

            if (height > graphicsDevice.PreferredBackBufferHeight)
            {
                height = graphicsDevice.PreferredBackBufferHeight;
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }

            viewport = new Viewport();

            viewport.X = (graphicsDevice.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (graphicsDevice.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            if (changed) { dirtyMatrix = true; }

            graphicsDevice.GraphicsDevice.Viewport = viewport;
        }

        public Vector2 ScaleMouseToScreenCoordinates(Vector2 screenPosition)
        {
            float realX = screenPosition.X - viewport.X;
            float realY = screenPosition.Y - viewport.Y;

            virtualMousePosition.X = realX / scaleX;
            virtualMousePosition.Y = realY / scaleY;

            return virtualMousePosition;
        }
    }
}
