using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.managers.menuManager
{
    public class MenuManager
    {
        private static MenuManager instance;

        menus.Menu menu;
        bool isTransitioning;
        string menuType;

        public int activeOption;
        public int prevActiveOption;
        public bool Quit;

        menus.menuData.TitleMenu titleMenu = new menus.menuData.TitleMenu();
        menus.menuData.NewGameMenu newGameMenu = new menus.menuData.NewGameMenu();
        menus.menuData.LoadGameMenu loadGameMenu = new menus.menuData.LoadGameMenu();
        menus.menuData.OptionMenu optionsMenu = new menus.menuData.OptionMenu();
        menus.menuData.CreditsMenu creditsMenu = new menus.menuData.CreditsMenu();

        menus.menuData.optionScreens.AudioOptionMenu audioOptionMenu = new menus.menuData.optionScreens.AudioOptionMenu();
        menus.menuData.optionScreens.VideoOptionMenu videoOptionMenu = new menus.menuData.optionScreens.VideoOptionMenu();
        menus.menuData.optionScreens.CameraOptionMenu cameraOptionMenu = new menus.menuData.optionScreens.CameraOptionMenu();
        menus.menuData.optionScreens.MouseOptionMenu mouseOptionMenu = new menus.menuData.optionScreens.MouseOptionMenu();
        menus.menuData.optionScreens.KeyboardOptionMenu keyboardOptionMenu = new menus.menuData.optionScreens.KeyboardOptionMenu();
        menus.menuData.optionScreens.ControllerOptionMenu controllerOptionMenu = new menus.menuData.optionScreens.ControllerOptionMenu();


        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuManager();
                }
                return instance;
            }
        }

        public MenuManager()
        {
            Quit = false;
            activeOption = 0;
            prevActiveOption = 0;

            menu = new menus.Menu();
            menu.OnMenuChange += menu_OnMenuChange;
        }

        //change menu
        void ChangeMenu(GameTime gameTime)
        {
            if (isTransitioning)
            {
                int oldMenuCount = menu.Items.Count;
                for (int i = 0; i < oldMenuCount; i++)
                {
                    menu.Items[i].Image.Update(gameTime);
                    float first = menu.Items[0].Image.Alpha;
                    float last = menu.Items[menu.Items.Count - 1].Image.Alpha;
                    if (first == 0.0f && last == 0.0f) { 
                        menu.ID = menu.Items[menu.ItemNumber].LinkID; }
                    else if (first == 1.0f && last == 1.0f)
                    {
                        isTransitioning = false;
                        foreach (menus.MenuItem item in menu.Items) { item.Image.RestoreEffects(); }
                    }
                }
            }
        }

        void menu_OnMenuChange(object sender, EventArgs e)
        {
            menu.UnloadContent();
            menuType = menu.ID;

            menu = new menus.Menu();
            GetMenuData();
            menu.LoadContent();
            menu.OnMenuChange += menu_OnMenuChange;
            menu.Transition(0.0f);

            foreach (menus.MenuItem item in menu.Items)
            {
                item.Image.StoreEffects();
                item.Image.ActivateEffect("image.effects.FadeEffect");
            }
        }

        void GetMenuData()
        {
            //menu.Effects = "image.effects.FadeEffect";

            switch (menuType)
            {
                case "TitleMenu": { menu.Items = titleMenu.Items; menu.MenuBackground.Path = titleMenu.Path; break; }
                case "NewGameMenu": { menu.Items = newGameMenu.Items; menu.MenuBackground.Path = newGameMenu.Path; break; }
                case "LoadGameMenu": { menu.Items = loadGameMenu.Items; menu.MenuBackground.Path = loadGameMenu.Path; break; }
                case "OptionsMenu": { menu.Items = optionsMenu.Items; menu.MenuBackground.Path = optionsMenu.Path; break; }
                case "CreditsMenu": { menu.Items = creditsMenu.Items; menu.MenuBackground.Path = creditsMenu.Path; break; }
                case "AudioOptionMenu": { menu.Items = audioOptionMenu.Items; menu.MenuBackground.Path = audioOptionMenu.Path; break; }
                case "VideoOptionMenu": { menu.Items = videoOptionMenu.Items; menu.MenuBackground.Path = videoOptionMenu.Path; break; }
                case "CameraOptionMenu": { menu.Items = cameraOptionMenu.Items; menu.MenuBackground.Path = cameraOptionMenu.Path; break; }
                case "MouseOptionMenu": { menu.Items = mouseOptionMenu.Items; menu.MenuBackground.Path = mouseOptionMenu.Path; break; }
                case "KeyboardOptionMenu": { menu.Items = keyboardOptionMenu.Items; menu.MenuBackground.Path = keyboardOptionMenu.Path; break; }
                case "ControllerOptionMenu": { menu.Items = controllerOptionMenu.Items; menu.MenuBackground.Path = controllerOptionMenu.Path; break; }
            }
        }

        void RefreshMenu()
        {
            menu.UnloadContent();
            menu = new menus.Menu();
            menu.OnMenuChange += menu_OnMenuChange;

            switch (menuType)
            {
                case "TitleMenu": { titleMenu = new menus.menuData.TitleMenu(); break; }
                case "NewGameMenu": { newGameMenu = new menus.menuData.NewGameMenu(); break; }
                case "LoadGameMenu": { loadGameMenu = new menus.menuData.LoadGameMenu(); break; }
                case "OptionsMenu": { optionsMenu = new menus.menuData.OptionMenu(); break; }
                case "CreditsMenu": { creditsMenu = new menus.menuData.CreditsMenu(); break; }
                case "AudioOptionMenu": { audioOptionMenu = new menus.menuData.optionScreens.AudioOptionMenu(); break; }
                case "VideoOptionMenu": { videoOptionMenu = new menus.menuData.optionScreens.VideoOptionMenu(); break; }
                case "CameraOptionMenu": { cameraOptionMenu = new menus.menuData.optionScreens.CameraOptionMenu(); break; }
                case "MouseOptionMenu": { mouseOptionMenu = new menus.menuData.optionScreens.MouseOptionMenu(); break; }
                case "KeyboardOptionMenu": { keyboardOptionMenu = new menus.menuData.optionScreens.KeyboardOptionMenu(); break; }
                case "ControllerOptionMenu": { controllerOptionMenu = new menus.menuData.optionScreens.ControllerOptionMenu(); break; }
            }

            GetMenuData();
            menu.LoadContent();
        }

        public void LoadContent(string menuPath)
        {
            //activates OnMenuChange
            if (menuPath != String.Empty) { 
                menu.ID = menuPath; }
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (!isTransitioning)
            {
                menu.Update(gameTime);
                if (InputManager.Instance.KeyPressed(Keys.Enter) && !isTransitioning || InputManager.Instance.LeftMousePressed() && InputManager.Instance.MouseOver(menu.Items[menu.ItemNumber].Image.Position.X, menu.Items[menu.ItemNumber].Image.Position.X + menu.Items[menu.ItemNumber].Image.SourceRect.Right, menu.Items[menu.ItemNumber].Image.Position.Y, menu.Items[menu.ItemNumber].Image.Position.Y + menu.Items[menu.ItemNumber].Image.SourceRect.Bottom) && !isTransitioning)
                {
                    switch (menu.Items[menu.ItemNumber].LinkType)
                    {
                        case "Screen": { screenManager.GameScreenManager.Instance.ChangeScreens(menu.Items[menu.ItemNumber].LinkID); break; }
                        case "Menu": 
                            {
                                isTransitioning = true;
                                menu.Transition(1.0f);
                                foreach (menus.MenuItem item in menu.Items)
                                {
                                    item.Image.StoreEffects();
                                    item.Image.ActivateEffect("image.effects.FadeEffect");
                                }
                                break; 
                            }
                        case "Option": 
                            {
                                switch (menu.Items[menu.ItemNumber].LinkID)
                                {
                                    case "Fullscreen": { Options.Instance.Fullscreen(); break; }
                                    case "Aspect": 
                                        {
                                            switch (Options.Instance.ResolutionAspect)
                                            {
                                                case Options.resAspect.FourThree: { Options.Instance.ResolutionAspect = Options.resAspect.SixteenNine; activeOption = -1; break; }
                                                case Options.resAspect.SixteenNine: { Options.Instance.ResolutionAspect = Options.resAspect.SixteenTen; activeOption = -1; break; }
                                                case Options.resAspect.SixteenTen: { Options.Instance.ResolutionAspect = Options.resAspect.FourThree; activeOption = -1; break; }
                                            }
                                            Options.Instance.ChangeAspectRatio();
                                            RefreshMenu();
                                            break; 
                                        }
                                    case "Resolution": 
                                        {
                                            if (menu.Items[menu.ItemNumber].ResolutionNumber != activeOption)
                                            {
                                                activeOption = menu.Items[menu.ItemNumber].ResolutionNumber;
                                                Options.Instance.ResolutionNumber = menu.Items[menu.ItemNumber].ResolutionNumber;
                                                Options.Instance.ChangeResolution(Options.Instance.ResolutionNumber);
                                                RefreshMenu();
                                            }
                                            break; 
                                        }
                                    case "WorldSize": 
                                        {
                                            switch (Options.Instance.WorldSize)
                                            {
                                                case Options.worldSize.Small: { Options.Instance.WorldSize = Options.worldSize.Medium; Options.Instance.WorldWidth = 512; Options.Instance.WorldHeight = Options.Instance.WorldWidth * 2; break; }
                                                case Options.worldSize.Medium: { Options.Instance.WorldSize = Options.worldSize.Large; Options.Instance.WorldWidth = 1024; Options.Instance.WorldHeight = Options.Instance.WorldWidth * 2; break; }
                                                case Options.worldSize.Large: { Options.Instance.WorldSize = Options.worldSize.Small; Options.Instance.WorldWidth = 256; Options.Instance.WorldHeight = Options.Instance.WorldWidth * 2; break; }
                                            }
                                            RefreshMenu();
                                            break; 
                                        }
                                    case "ResourceQuality": 
                                        {
                                            switch (Options.Instance.ResourceQuality)
                                            {
                                                case Options.resourceQuality.Low: { Options.Instance.ResourceQuality = Options.resourceQuality.Medium; break; }
                                                case Options.resourceQuality.Medium: { Options.Instance.ResourceQuality = Options.resourceQuality.High; break; }
                                                case Options.resourceQuality.High: { Options.Instance.ResourceQuality = Options.resourceQuality.Low; break; }
                                            }
                                            RefreshMenu();
                                            break; 
                                        }
                                    case "ResourceQuantity": 
                                        {
                                            switch (Options.Instance.ResourceQuantity)
                                            {
                                                case Options.resourceQuantity.Low: { Options.Instance.ResourceQuantity = Options.resourceQuantity.Medium; break; }
                                                case Options.resourceQuantity.Medium: { Options.Instance.ResourceQuantity = Options.resourceQuantity.High; break; }
                                                case Options.resourceQuantity.High: { Options.Instance.ResourceQuantity = Options.resourceQuantity.Low; break; }
                                            }
                                            RefreshMenu();
                                            break; 
                                        }
                                    case "AnimalDensity": 
                                        {
                                            switch (Options.Instance.AnimalDensity)
                                            {
                                                case Options.animalDensity.Low: { Options.Instance.AnimalDensity = Options.animalDensity.Medium; break; }
                                                case Options.animalDensity.Medium: { Options.Instance.AnimalDensity = Options.animalDensity.High; break; }
                                                case Options.animalDensity.High: { Options.Instance.AnimalDensity = Options.animalDensity.Low; break; }
                                            }
                                            RefreshMenu();
                                            break; 
                                        }
                                    case "AnimalAggression": 
                                        {
                                            switch (Options.Instance.AnimalAggression)
                                            {
                                                case Options.animalAggression.Low: { Options.Instance.AnimalAggression = Options.animalAggression.Medium; break; }
                                                case Options.animalAggression.Medium: { Options.Instance.AnimalAggression = Options.animalAggression.High; break; }
                                                case Options.animalAggression.High: { Options.Instance.AnimalAggression = Options.animalAggression.Low; break; }
                                            }
                                            RefreshMenu();
                                            break; 
                                        }
                                    case "PlayerAI": 
                                        {
                                            switch (Options.Instance.PlayerAI)
                                            {
                                                case Options.playerAI.One: { Options.Instance.PlayerAI = Options.playerAI.Two; break; }
                                                case Options.playerAI.Two: { Options.Instance.PlayerAI = Options.playerAI.Three; break; }
                                                case Options.playerAI.Three: { Options.Instance.PlayerAI = Options.playerAI.One; break; }
                                            }
                                            RefreshMenu();
                                            break; 
                                        }
                                    case "MouseSensitivity":
                                        {
                                            switch (Options.Instance.MouseSensitivity)
                                            {
                                                case Options.mouseSensitivity.Low: { Options.Instance.MouseSensitivity = Options.mouseSensitivity.Medium; CursorManager.Instance.mouseSensitivity = 100.0f; break; }
                                                case Options.mouseSensitivity.Medium: { Options.Instance.MouseSensitivity = Options.mouseSensitivity.High; CursorManager.Instance.mouseSensitivity = 200.0f; break; }
                                                case Options.mouseSensitivity.High: { Options.Instance.MouseSensitivity = Options.mouseSensitivity.Low; CursorManager.Instance.mouseSensitivity = 50.0f; break; }
                                            }
                                            RefreshMenu();
                                            break;
                                        }
                                }
                                break; 
                            }
                        case "Quit":
                            {
                                Quit = true;
                                break;
                            }
                    }
                }
            }
            ChangeMenu(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
