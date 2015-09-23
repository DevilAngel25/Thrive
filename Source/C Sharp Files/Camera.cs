using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Camera
    {
        private static Camera instance;
        private GraphicsDeviceManager graphicsDevice = null;
        
        private float zoom;
        private float rotation;
        private Vector2 position;
        private Vector2 location = Vector2.Zero;
        private Matrix transform = Matrix.Identity;
        private Matrix camTranslationMatrix = Matrix.Identity;
        private Matrix camRotationMatrix = Matrix.Identity;
        private Matrix camScaleMatrix = Matrix.Identity;
        private Matrix resTranslationMatrix = Matrix.Identity;
 
        private Vector3 camTranslationVector = Vector3.Zero;
        private Vector3 camScaleVector = Vector3.Zero;
        private Vector3 resTranslationVector = Vector3.Zero;

        private int worldWidth;
        private int worldHeight;
        private int viewWidth;
        private int viewHeight;

        public Vector2 DisplayOffset;
        public bool isViewTransformationDirty = true;

        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }
                return instance;
            }
        }

        public Camera()
        {
            zoom = 0.1f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        public void Initialize(ref GraphicsDeviceManager device)
        {
            graphicsDevice = device;
            viewWidth = graphicsDevice.GraphicsDevice.PresentationParameters.BackBufferWidth;
            viewHeight = graphicsDevice.GraphicsDevice.PresentationParameters.BackBufferHeight;
            isViewTransformationDirty = true;
        }

        public Vector2 Location
        {
            get { return location; }
            set
            {
                location = new Vector2( MathHelper.Clamp(value.X, 0f, WorldWidth - ViewWidth),
                                        MathHelper.Clamp(value.Y, 0f, WorldHeight - ViewHeight));
            }
        }

        public int WorldWidth
        {
            get { return worldWidth; }
            set { worldWidth = value; }
        }

        public int WorldHeight
        {
            get { return worldHeight; }
            set { worldHeight = value; }
        }

        public int ViewWidth
        {
            get { return viewWidth; }
            set { viewWidth = value; }
        }

        public int ViewHeight
        {
            get { return viewHeight; }
            set { viewHeight = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                isViewTransformationDirty = true;
            }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f) { zoom = 1.0f; }
                if (zoom > 10.0f) { zoom = 10.0f; }
                isViewTransformationDirty = true;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                isViewTransformationDirty = true;
            }
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return worldPosition - Location + DisplayOffset;
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return screenPosition + Location - DisplayOffset;
        }

        public void Move(Vector2 offset)
        {
            Location += offset;
        }

        public void MoveCamera(Vector2 amount)
        {
            Position += amount;
        }
 
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public Matrix GetViewTransformationMatrix()
        {
            if (isViewTransformationDirty)
            {
                camTranslationVector.X = -position.X;
                camTranslationVector.Y = -position.Y;
 
                Matrix.CreateTranslation(ref camTranslationVector, out camTranslationMatrix);
                Matrix.CreateRotationZ(rotation, out camRotationMatrix);
 
                camScaleVector.X = zoom;
                camScaleVector.Y = zoom;
                camScaleVector.Z = 1;
 
                Matrix.CreateScale(ref camScaleVector, out camScaleMatrix);

                resTranslationVector.X = (int)Resolution.Instance.VirtualResolution.X * 0.5f;
                resTranslationVector.Y = (int)Resolution.Instance.VirtualResolution.Y * 0.5f;
                resTranslationVector.Z = 0;
 
                Matrix.CreateTranslation(ref resTranslationVector, out resTranslationMatrix);
 
                transform = camTranslationMatrix *
                            camRotationMatrix *
                            camScaleMatrix *
                            resTranslationMatrix *
                            Resolution.Instance.GetTransformationMatrix();
 
                isViewTransformationDirty = false;
            }
 
            return transform;
        }

        public void Update(GameTime gameTme)
        {
            if (managers.InputManager.Instance.ScrollUp())
            {
                Zoom = zoom + 1.0f;
            }
            if (managers.InputManager.Instance.ScrollDown())
            {
                Zoom = zoom - 1.0f;
            }
        }

    }
}
