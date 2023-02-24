using PPong;
using SDL2;

namespace Galaga.GUI;

class StartMenu : IGameState
    {
        // Fields to store the SDL window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the game textures and rectangles
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergroundRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _doublePlayTexture;
        private SDL.SDL_Rect _doublePlayRect;
        private IntPtr _singlePlayTexture;
        private SDL.SDL_Rect _singlePlayRect;

        // Constructor
        public StartMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;

            // Load the game texture and set the game rectangle
            _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
                "Assest\\Background.png");
            _hintergroundRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = DisplaySetting.Display_Width,
                h = DisplaySetting.Display_Height
            };
            _singlePlayTexture = SDL_image.IMG_LoadTexture(_renderer,
                "Assest\\Play.png");
            _singlePlayRect = new SDL.SDL_Rect()
            {
                x = 532,
                y = 100,
                w = 150,
                h = 50
            };
            
            _doublePlayTexture = SDL_image.IMG_LoadTexture(_renderer,
                "Assest\\Multiplay.png");
            _doublePlayRect = new SDL.SDL_Rect()
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

        // Method to display the game screen
        public void Update()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the back button
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition back to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
                }
            }
        }

        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);

            // Render the game texture
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergroundRect);
            SDL.SDL_RenderCopy(_renderer, _doublePlayTexture, IntPtr.Zero, ref _doublePlayRect);
            SDL.SDL_RenderCopy(_renderer, _singlePlayTexture, IntPtr.Zero, ref _singlePlayRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        }

        // Method to handle player input
        public void HandleInput()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the Q key
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

                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
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
                // Check if the player clicked on the Play button
                
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _singlePlayRect.x && x <= _singlePlayRect.x + _singlePlayRect.w &&
                        y >= _singlePlayRect.y && y <= _singlePlayRect.y + _singlePlayRect.h)
                    {
                        // Transition to the main menu state
                        GameState.SetState(new SinglePlay(_window, _renderer));
					    SDL.SDL_Quit();
					    Environment.Exit(0);
				    }
                }
                // Check if the player clicked on the DoublePlay button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _doublePlayRect.x && x <= _doublePlayRect.x + _doublePlayRect.w &&
                        y >= _doublePlayRect.y && y <= _doublePlayRect.y + _doublePlayRect.h)
                    {
                        // Transition to the main menu state
                        GameState.SetState(new DoublePlay(_window, _renderer));
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
    }
