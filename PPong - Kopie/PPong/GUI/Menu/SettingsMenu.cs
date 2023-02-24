using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.GUI;
using PPong;
using SDL2;

namespace PPong
{
    class SettingsMenu : IGameState
    {
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the settings textures and rectangles
        private IntPtr _soundTexture;
        private SDL.SDL_Rect _soundRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _levelTexture;
        private SDL.SDL_Rect _levelButtonRect;
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergrungRect;

        // Constructor
        public SettingsMenu(IntPtr window, IntPtr renderer)
        {

            _window = window;
            _renderer = renderer;

            _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Background.png");

            _hintergrungRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = DisplaySetting.Display_Width,
                h = DisplaySetting.Display_Height
            };
            // Load the settings texture and set the settings rectangle
            _soundTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Sound.png");
            _soundRect = new SDL.SDL_Rect()
            {
                x = 532,
                y = 100,
                w = 150,
                h = 50
            };
            _levelTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Level.png");
            _levelButtonRect = new SDL.SDL_Rect()
            {
                x = 532,
                y = 180,
                w = 150,
                h = 50
            };
            _backTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Back.png");
            _backButtonRect = new SDL.SDL_Rect()
            {
                x = 532,
                y = 260,
                w = 150,
                h = 50
            };
        }

        // Method to display the settings screen
        public void Update()
        {
            UpdateButtonPositions();

        }


        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the settings texture
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergrungRect);
            SDL.SDL_RenderCopy(_renderer, _soundTexture, IntPtr.Zero, ref _soundRect);
            SDL.SDL_RenderCopy(_renderer, _levelTexture, IntPtr.Zero, ref _levelButtonRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        }

        // Method to handle player input
        public void HandleInput()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {

                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    music();
                    // Transition to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
                }


                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
                {
                    // Quit the game
                    music();
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }

                // Check if the player pressed the "X" symbol with the Mouse
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    // Quit the game
                    music();
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }

                // Check if the player pressed the f key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_f)
                {
                    music();
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
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _soundRect.x && x <= _soundRect.x + _soundRect.w &&
                        y >= _soundRect.y && y <= _soundRect.y + _soundRect.h)
                    {
                        // Transition to the main menu state
                        GameState.SetState(new SoundMenu(_window, _renderer));
                    }
                }
                // Check if the player clicked on the level button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _levelButtonRect.x && x <= _levelButtonRect.x + _levelButtonRect.w &&
                       y >= _levelButtonRect.y && y <= _levelButtonRect.y + _levelButtonRect.h)
                    {
                        // Transition to the main menu state
                        GameState.SetState(new LevelMenu(_window, _renderer));
                    }
                }
                // Check if the player clicked on the back button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {

                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _backButtonRect.x && x <= _backButtonRect.x + _backButtonRect.w &&
                        y >= _backButtonRect.y && y <= _backButtonRect.y + _backButtonRect.h)
                    {
                        // Transition to the main menu state
                        GameState.SetState(new MainMenu(_window, _renderer));
                    }
                }
            }
        }
        private void UpdateButtonPositions()
        {
            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            if (mouseX >= _soundRect.x && mouseX <= _soundRect.x + _soundRect.w &&
                mouseY >= _soundRect.y && mouseY <= _soundRect.y + _soundRect.h && _soundRect.x > 470)
            {

                _soundRect.x -= 1;
            }
            else if (_soundRect.x < 532)
            {
                _soundRect.x += 1;
            }


            if (mouseX >= _levelButtonRect.x && mouseX <= _levelButtonRect.x + _levelButtonRect.w &&
                mouseY >= _levelButtonRect.y && mouseY <= _levelButtonRect.y + _levelButtonRect.h && _levelButtonRect.x > 470)
            {
                _levelButtonRect.x -= 1;
            }
            else if (_levelButtonRect.x < 532)
            {
                _levelButtonRect.x += 1;
            }

            if (mouseX >= _backButtonRect.x && mouseX <= _backButtonRect.x + _backButtonRect.w &&
                mouseY >= _backButtonRect.y && mouseY <= _backButtonRect.y + _backButtonRect.h && _backButtonRect.x > 470)
            {
                _backButtonRect.x -= 1;
            }
            else if (_backButtonRect.x < 532)
            {
                _backButtonRect.x += 1;
            }
        }
        private void music()
        {
            //TODO
        }
    }
}