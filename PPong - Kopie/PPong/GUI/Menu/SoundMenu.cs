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
    class SoundMenu : IGameState
    {
        #region Variable
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the settings textures and rectangles
        private IntPtr _music_onTexture;
        private SDL.SDL_Rect _music_onRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _music_offTexture;
        private SDL.SDL_Rect _music_offRect;
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergrungRect;
        public static bool music_on = true;
        #endregion

        #region Constructor
        /// <summary> 
        /// Initializes a new instance of the SoundMenu class
        /// </summary>
        /// <param name="window"> The pointer to the SDL window </param>
        /// <param name="renderer"> renderer  The pointer to the SDL renderer </param>
        public SoundMenu(IntPtr window, IntPtr renderer)
        {

            _window = window;
            _renderer = renderer;

            _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
                "Assest\\Background.png");

            _hintergrungRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = DisplaySetting.Display_Width,
                h = DisplaySetting.Display_Height
            };
            // Load the settings texture and set the settings rectangle
            _music_onTexture = SDL_image.IMG_LoadTexture(_renderer,
                "Assest\\music_on.png");
            _music_onRect = new SDL.SDL_Rect()
            {
                x = 532,
                y = 100,
                w = 150,
                h = 50
            };
            _music_offTexture = SDL_image.IMG_LoadTexture(_renderer,
                "Assest\\music_off.png");
            _music_offRect = new SDL.SDL_Rect()
            {
                x = 532,
                y = 180,
                w = 150,
                h = 50
            };
            _backTexture = SDL_image.IMG_LoadTexture(_renderer,
                "Assest\\Back.png");
            _backButtonRect = new SDL.SDL_Rect()
            {
                x = 532,
                y = 260,
                w = 150,
                h = 50
            };
        }

        #endregion

        #region

        /**
        * @brief     Updates the position of the buttons in the SoundMenu
        * @pre       None
        * @post      The positions of the buttons in the SoundMenu are updated
        **/
        public void Update()
        {
            UpdateButtonPositions();
        }

        /**
        * @brief     Draws the SoundMenu on the screen
        * @pre       None
        * @post      The SoundMenu is displayed on the screen
        **/
        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the settings texture
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergrungRect);
            SDL.SDL_RenderCopy(_renderer, _music_onTexture, IntPtr.Zero, ref _music_onRect);
            SDL.SDL_RenderCopy(_renderer, _music_offTexture, IntPtr.Zero, ref _music_offRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        }

        /**
         * <summary>
         * Handles the input from the player
         * </summary>
         * <remarks>
         * This method polls for events and checks for specific key presses, mouse clicks, and other inputs from the player.
         * Depending on the input received, it updates the game state or performs other actions.
         * </remarks>
         */
        public void HandleInput()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
                }


                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }

                // Check if the player pressed the "X" symbol with the Mouse
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
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
                // Check if the player clicked on the music_on button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _music_onRect.x && x <= _music_onRect.x + _music_onRect.w &&
                        y >= _music_onRect.y && y <= _music_onRect.y + _music_onRect.h)
                    {
                        // Transition to the main menu state
                        music_on = true;
                    }
                }
                // Check if the player clicked on the music_off button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {

                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _music_offRect.x && x <= _music_offRect.x + _music_offRect.w &&
                        y >= _music_offRect.y && y <= _music_offRect.y + _music_offRect.h)
                    {
                        // Transition to the main menu state
                        music_on = false;
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
                        GameState.SetState(new SettingsMenu(_window, _renderer));
                    }
                }
            }
        }
        /**
         * <summary>
         * Update the positions of the buttons based on the mouse position
         * </summary>
         * <remarks>
         * This method checks the current mouse position and updates the positions of the music_on, music_off, and back buttons accordingly. 
         * If the mouse is hovering over a button, it moves the button to the left. 
         * If the mouse is not hovering over a button, it moves the button back to its original position.
         * </remarks>
         */
        private void UpdateButtonPositions()
        {
            // Get the current mouse position
            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            //Check if the mouse is hovering over the music_on button
            if (mouseX >= _music_onRect.x && mouseX <= _music_onRect.x + _music_onRect.w &&
                mouseY >= _music_onRect.y && mouseY <= _music_onRect.y + _music_onRect.h && _music_onRect.x > 470)
            {
                // Move the button to the left
                _music_onRect.x -= 1;
            }
            // Check if the button is not at its original position
            else if (_music_onRect.x < 532)
            {
                // Move the button back to its original position
                _music_onRect.x += 1;
            }

            if (mouseX >= _music_offRect.x && mouseX <= _music_offRect.x + _music_offRect.w &&
                mouseY >= _music_offRect.y && mouseY <= _music_offRect.y + _music_offRect.h && _music_offRect.x > 470)
            {
                _music_offRect.x -= 1;
            }
            else if (_music_offRect.x < 532)
            {
                _music_offRect.x += 1;
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
        #endregion
    }
}
