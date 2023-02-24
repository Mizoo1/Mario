using Galaga.GUI;
using Microsoft.VisualBasic;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Utility;

namespace PPong.GUI
{
    public class MenuOption : Galaga.Utility.Constants, IGameState
    {
        private IntPtr Texture;
        private IntPtr _renderer;
        private IntPtr _window;
        private SDL.SDL_Rect Rect;
        private String name;

        public MenuOption(String name, IntPtr _renderer, IntPtr _window)
        {
            this.name = name;
            this._renderer = _renderer;
            this._window = _window;
        }

        public void createMenu()
        {
            Texture = SDL_image.IMG_LoadTexture(_renderer, imagePaths[name]);
            switch (name)
            {
                case "background":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 0,
                        y = 0,
                        w = DisplaySetting.Display_Width,
                        h = DisplaySetting.Display_Height
                    };
                    break;
                case "start":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 532,
                        y = 100,
                        w = 150,
                        h = 50
                    };
                    break;
                case "setting":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 532,
                        y = 180,
                        w = 150,
                        h = 50
                    };
                    break;
                case "help":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 532,
                        y = 260,
                        w = 150,
                        h = 50
                    };
                    break;
                case "exit":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 532,
                        y = 340,
                        w = 150,
                        h = 50
                    };
                    break;
                case "Music_ON":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 532,
                        y = 100,
                        w = 150,
                        h = 50
                    };
                    break;
                case "Music_OFF":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 532,
                        y = 180,
                        w = 150,
                        h = 50
                    };
                    break;
                case "back":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 532,
                        y = 260,
                        w = 150,
                        h = 50
                    };
                    break;
                case "galaga":

                    Rect = new SDL.SDL_Rect()
                    {
                        x = 20,
                        y = 20,
                        w = 400,
                        h = 300
                    };
                    break;
            }
        }

        public void Draw()
        {
            SDL.SDL_RenderCopy(_renderer, Texture, IntPtr.Zero, ref Rect);
        }

        public void HandleInput()
        {
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player clicked on the start button
                EnterStart(e);
                // Check if the player clicked on the start button
                EnterSetting(e);
                // Check if the player clicked on the start button
                EnterHelp(e);
                // Check if the player clicked on the exit button
                EnterQuit(e);
                // Check if symbol X clicked 
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {

                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }
                // Check if the player pressed the q key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
                {

                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }

                // Check if the player pressed the f key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_f)
                {
                    // Toggle fullscreen mode
                    if ((SDL.SDL_GetWindowFlags(_window) & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN) == 0)
                    {
                        SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
                    }
                    else
                    {
                        SDL.SDL_SetWindowFullscreen(_window, 0);
                    }
                }

            }
        }

        public void Update()
        {
            UpdateButtonPositions();
        }
        public void EnterStart(SDL.SDL_Event e)
        {
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
            {
                int x = e.button.x;
                int y = e.button.y;
                if (x >= 400 && x <= 532 + 150 &&
                    y >= 100 && y <= 100 + 50)
                {
                    // Transition to the game state
                    GameState.SetState(new StartMenu(_window, _renderer));

                }
            }

        }

        public void EnterSetting(SDL.SDL_Event e)
        {
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
            {
                int x = e.button.x;
                int y = e.button.y;
                if (x >= 532 && x <= 532 + 150 &&
                    y >= 180 && y <= 180 + 50)
                {
                    // Transition to the game state
                    GameState.SetState(new SettingsMenu(_window, _renderer));
                }
            }
        }
        public void EnterHelp(SDL.SDL_Event e)
        {
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
            {
                int x = e.button.x;
                int y = e.button.y;
                if (x >= 532 && x <= 532 + 150 &&
                    y >= 260 && y <= 260 + 50)
                {
                    // Transition to the game state
                    GameState.SetState(new HelpMenu(_window, _renderer));

                }
            }
        }

        public void EnterQuit(SDL.SDL_Event e)
        {
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
            {
                int x = e.button.x;
                int y = e.button.y;
                if (x >= 532 && x <= 532 + 150 &&
                    y >= 340 && y <= 340 + 50)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }
            }
        }

        public void UpdateButtonPositions()
        {

            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            if (mouseX >= Rect.x && mouseX <= Rect.x + Rect.w &&
                mouseY >= Rect.y && mouseY <= Rect.y + Rect.h && Rect.x > 470)
            {
                Rect.x -= 1;
            }
            else if (Rect.x < 532)
            {
                Rect.x += 1;
            }

        }
    }
}
