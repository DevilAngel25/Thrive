using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.map
{
    class Map
    {
        protected ContentManager content;

        private SpriteFont pericles6;
        private Texture2D hilight;

        tile.TileMap myMap;

        bool coOrds = false;
        int squaresAcross, squaresDown, baseOffsetX, baseOffsetY;
        float heightRowDepthMod;

        public Map()
        {
            squaresAcross = ((int)Resolution.Instance.VirtualResolution.X / 64) + 2;
            squaresDown = (int)Resolution.Instance.VirtualResolution.Y / 16 + 3;
            baseOffsetX = -32;
            baseOffsetY = -64;
            heightRowDepthMod = 0.0000001f;
        }

        public tile.TileMap MyMap
        {
            get { return myMap; }
        }

        public void LoadContent()
        {
            content = new ContentManager(managers.screenManager.GameScreenManager.Instance.Content.ServiceProvider, "Content");
            tile.Tile.TileSetTexture = content.Load<Texture2D>("images/gameplay/tileSheets/part4_tileset");
            myMap = new tile.TileMap(content.Load<Texture2D>("images/gameplay/tileSheets/mousemap"), content.Load<Texture2D>("images/gameplay/tileSheets/part9_slopemaps"));
            pericles6 = content.Load<SpriteFont>("fonts/Pericles6");
            hilight = content.Load<Texture2D>("images/gameplay/tileSheets/hilight");

            Camera.Instance.WorldWidth = ((myMap.MapWidth - 2) * tile.Tile.TileStepX);
            Camera.Instance.WorldHeight = ((myMap.MapHeight - 2) * tile.Tile.TileStepY);
            Camera.Instance.DisplayOffset = new Vector2(baseOffsetX, baseOffsetY);
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (managers.InputManager.Instance.KeyDown(Keys.LeftShift)) { coOrds = true; }
            else { coOrds = false; }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteAnimation vlad)
        {
            Vector2 firstSquare, squareOffset, hilightLoc;
            Point vladMapPoint, vladStandingOn, hilightPoint;
            int firstX, firstY, offsetX, offsetY, rowOffset, mapx, mapy, heightRow, vladHeight, hilightrowOffset;
            float maxdepth, depthOffset;
            
            maxdepth = ((myMap.MapWidth + 1) + ((myMap.MapHeight + 1) * tile.Tile.TileWidth)) * 10;

            firstSquare = new Vector2(Camera.Instance.Location.X / tile.Tile.TileStepX, Camera.Instance.Location.Y / tile.Tile.TileStepY);
            firstX = (int)firstSquare.X;
            firstY = (int)firstSquare.Y;

            squareOffset = new Vector2(Camera.Instance.Location.X % tile.Tile.TileStepX, Camera.Instance.Location.Y % tile.Tile.TileStepY);
            offsetX = (int)squareOffset.X;
            offsetY = (int)squareOffset.Y;

            // calculate squares down / across

            for (int y = 0; y < squaresDown; y++)
            {
                rowOffset = 0;
                if ((firstY + y) % 2 == 1) { rowOffset = tile.Tile.OddRowXOffset; }
                for (int x = 0; x < squaresAcross; x++)
                {
                    mapx = (firstX + x);
                    mapy = (firstY + y);
                    vladMapPoint = myMap.WorldToMapCell(new Point((int)vlad.Position.X, (int)vlad.Position.Y));

                    depthOffset = 0.7f - ((mapx + (mapy * tile.Tile.TileWidth)) / maxdepth);

                    if ((mapx >= myMap.MapWidth) || (mapy >= myMap.MapHeight)) { continue; }

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        spriteBatch.Draw(   tile.Tile.TileSetTexture, 
                                            Camera.Instance.WorldToScreen(new Vector2((mapx * tile.Tile.TileStepX) + rowOffset, mapy * tile.Tile.TileStepY)), 
                                            tile.Tile.GetSourceRectangle(tileID), 
                                            Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 
                                            1.0f);
                    }

                    heightRow = 0;
                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(   tile.Tile.TileSetTexture, 
                                            Camera.Instance.WorldToScreen(new Vector2((mapx * tile.Tile.TileStepX) + rowOffset, mapy * tile.Tile.TileStepY - (heightRow * tile.Tile.HeightTileOffset))),
                                            tile.Tile.GetSourceRectangle(tileID), 
                                            Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 
                                            depthOffset - ((float)heightRow * heightRowDepthMod));
                        heightRow++;
                    }

                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {
                        spriteBatch.Draw(   tile.Tile.TileSetTexture,
                                            Camera.Instance.WorldToScreen(new Vector2((mapx * tile.Tile.TileStepX) + rowOffset, mapy * tile.Tile.TileStepY)),
                                            tile.Tile.GetSourceRectangle(tileID),
                                            Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None,
                                            depthOffset - ((float)heightRow * heightRowDepthMod));
                    }

                    if ((mapx == vladMapPoint.X) && (mapy == vladMapPoint.Y))
                    {
                        vlad.DrawDepth = depthOffset - (float)(heightRow + 2) * heightRowDepthMod;
                    }

                    if (coOrds)
                    {
                        spriteBatch.DrawString( pericles6, 
                                                (x + firstX).ToString() + ", " + (y + firstY).ToString(),
                                                new Vector2((x * tile.Tile.TileStepX) - offsetX + rowOffset + baseOffsetX + 24,
                                                (y * tile.Tile.TileStepY) - offsetY + baseOffsetY + 48), 
                                                Color.White, 0f, Vector2.Zero,1.0f, SpriteEffects.None, 
                                                0.0f);
                    }
                }
            }

            vladStandingOn = myMap.WorldToMapCell(new Point((int)vlad.Position.X, (int)vlad.Position.Y));
            vladHeight = myMap.Rows[vladStandingOn.Y].Columns[vladStandingOn.X].HeightTiles.Count * tile.Tile.HeightTileOffset;

            vlad.Draw(spriteBatch, 0, -myMap.GetOverallHeight(vlad.Position));

            hilightLoc = Camera.Instance.ScreenToWorld(new Vector2(managers.CursorManager.Instance.VirtualMouseX, managers.CursorManager.Instance.VirtualMouseY));
            hilightPoint = myMap.WorldToMapCell(new Point((int)hilightLoc.X, (int)hilightLoc.Y));

            hilightrowOffset = 0;
            if ((hilightPoint.Y) % 2 == 1) { hilightrowOffset = map.tile.Tile.OddRowXOffset; }

            spriteBatch.Draw(   hilight, 
                                Camera.Instance.WorldToScreen(new Vector2((hilightPoint.X * tile.Tile.TileStepX) + hilightrowOffset, (hilightPoint.Y + 2) * tile.Tile.TileStepY)),
                                new Rectangle(0, 0, 64, 32),
                                Color.White * 0.3f, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None,
                                0.0f);
        }
    }
}
