using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class Options
    {
        private bool hasChanged;
        private bool isMouseVisible;
        private bool isFullscreen;
        private int resNumber;
        private int virtualWidth;
        private int virtualHeight;

        //--ENUM--\\
        public enum resAspect { FourThree, SixteenNine, SixteenTen, Other };

        //world
        public enum worldSize { Small, Medium, Large };
        //resources
        public enum resourceQuality { Low, Medium, High };
        public enum resourceQuantity { Low, Medium, High };
        //animals
        public enum animalDensity { Low, Medium, High };
        public enum animalAggression { Low, Medium, High };
        //AI
        public enum playerAI { One, Two, Three };

        public enum mouseSensitivity { Low, Medium, High };

        public resAspect ResolutionAspect = resAspect.FourThree;
        public worldSize WorldSize = worldSize.Small;
        
        public resourceQuality ResourceQuality = resourceQuality.Medium;
        public resourceQuantity ResourceQuantity = resourceQuantity.Medium;
        //animals
        public animalDensity AnimalDensity = animalDensity.Medium;
        public animalAggression AnimalAggression = animalAggression.Medium;
        //AI
        public playerAI PlayerAI = playerAI.Two;

        public mouseSensitivity MouseSensitivity = mouseSensitivity.Medium;

        public DisplayMode CurrentResolution;
        public int WorldWidth;
        public int WorldHeight;

        private static Options instance;

        public static Options Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Options();
                }
                return instance;
            }
        }

        public Options()
        {
            hasChanged = false;
            isMouseVisible = false;
            isFullscreen = false;
            resNumber = 0;
            virtualWidth = 1366;
            virtualHeight = 768;
            CurrentResolution = Resolution.Instance.DisplayModesSixteenNine[resNumber];

            WorldWidth = 256;
            WorldHeight = WorldWidth * 2;
        }

        public bool HasChanged
        {
            get { return hasChanged; }
            set { hasChanged = value; }
        }

        public bool IsMouseVisible
        {
            get { return isMouseVisible; }
            set { isMouseVisible = value; }
        }

        public bool IsFullscreen
        {
            get { return isFullscreen; }
            set { isFullscreen = value; }
        }

        public int ResolutionNumber
        {
            get { return resNumber; }
            set { resNumber = value; }
        }

        public int VirtualWidth
        {
            get { return virtualWidth; }
        }

        public int VirtualHeight
        {
            get { return virtualHeight; }
        }

        public void Fullscreen()
        {
            isFullscreen = !isFullscreen;
            Resolution.Instance.SetResolution(CurrentResolution.Width, CurrentResolution.Height, isFullscreen);
        }

        public void ChangeAspectRatio()
        {
            switch (ResolutionAspect)
            {
                case resAspect.FourThree:
                    {
                        virtualWidth = Resolution.Instance.DisplayModesFourThree[Resolution.Instance.DisplayModesFourThree.Count - 1].Width;
                        virtualHeight = Resolution.Instance.DisplayModesFourThree[Resolution.Instance.DisplayModesFourThree.Count - 1].Height;
                        break;
                    }
                case resAspect.SixteenNine:
                    {
                        virtualWidth = Resolution.Instance.DisplayModesSixteenNine[Resolution.Instance.DisplayModesSixteenNine.Count - 1].Width;
                        virtualHeight = Resolution.Instance.DisplayModesSixteenNine[Resolution.Instance.DisplayModesSixteenNine.Count - 1].Height;
                        break;
                    }
                case resAspect.SixteenTen:
                    {
                        virtualWidth = Resolution.Instance.DisplayModesSixteenTen[Resolution.Instance.DisplayModesSixteenTen.Count - 1].Width;
                        virtualHeight = Resolution.Instance.DisplayModesSixteenTen[Resolution.Instance.DisplayModesSixteenTen.Count - 1].Height;
                        break;
                    }
            }
        }

        public void ChangeResolution(int i)
        {
            switch (ResolutionAspect)
            {
                case resAspect.FourThree: { CurrentResolution = Resolution.Instance.DisplayModesFourThree[i]; break; }
                case resAspect.SixteenNine: { CurrentResolution = Resolution.Instance.DisplayModesSixteenNine[i]; break; }
                case resAspect.SixteenTen: { CurrentResolution = Resolution.Instance.DisplayModesSixteenTen[i]; break; }
            }

            managers.screenManager.GameScreenManager.Instance.Dimensions = new Vector2(virtualWidth, virtualHeight);
            Resolution.Instance.SetVirtualResolution(virtualWidth, virtualHeight);
            Resolution.Instance.SetResolution(CurrentResolution.Width, CurrentResolution.Height, isFullscreen);
           
            Resolution.Instance.ScaleMouse();
            managers.CursorManager.Instance.ResetState();

            Camera.Instance.Position = new Vector2((int)Resolution.Instance.VirtualResolution.X / 2, (int)Resolution.Instance.VirtualResolution.Y / 2);
            Camera.Instance.isViewTransformationDirty = true;
        }
    }
}
