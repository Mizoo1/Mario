using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPong;
using SDL2;
namespace Galaga.GUI;

class HelpMenu : IGameState
    {
    #region Variable
    // Fields to store the SDL _window and renderer
    private IntPtr _window;
    private IntPtr _renderer;

    // Fields to store the help textures and rectangles
    private IntPtr _helpTexture;
    private SDL.SDL_Rect _helpRect;
    private IntPtr _backTexture;
    private SDL.SDL_Rect _backButtonRect;
    private IntPtr _hintergrungTexture;
    private SDL.SDL_Rect _hintergrungRect;
    private Font _font;
    private Font _font_2;
    #endregion

    #region Constructor
    public HelpMenu(IntPtr window, IntPtr renderer)
    {
        _window = window;
        _renderer = renderer;
        if (SDL_ttf.TTF_Init() != 0)
        {
            throw new Exception("Failed to initialize SDL_ttf: " + SDL_ttf.TTF_GetError());
        }
        _font = new Font("Assest\\Lato-Italic.ttf", 22);
        _font_2 = new Font("Assest\\Lato-Italic.ttf", 30);

        _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
            "Assest\\rr.jpg");

        _hintergrungRect = new SDL.SDL_Rect()
        {
            x = 0,
            y = 0,
            w = DisplaySetting.Display_Width,
            h = DisplaySetting.Display_Height
        };

        // Load the help texture and set the help rectangle
        _backTexture = SDL_image.IMG_LoadTexture(_renderer,
            "Assest\\Back.png");
        _backButtonRect = new SDL.SDL_Rect()
        {
            x = 400,
            y = 340,
            w = 150,
            h = 50
        };
    }
    #endregion

    #region Methods

    // Method to display the help screen
    public void Update()
    {
        UpdateButtonPositions();
        // Poll for events
        SDL.SDL_Event e;
        while (SDL.SDL_PollEvent(out e) != 0)
        {
            // Check if the player clicked the back button
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
            {
                int x = e.button.x;
                int y = e.button.y;
                if (x >= _backButtonRect.x && x <= _backButtonRect.x + _backButtonRect.w &&
                    y >= _backButtonRect.y && y <= _backButtonRect.y + _backButtonRect.h)
                {
                    // Transition back to the main menu

                    GameState.SetState(new MainMenu(_window, _renderer));
                }
            }
        }
    }


    public void Draw()
    {
        // Clear the renderer
        SDL.SDL_RenderClear(_renderer);

        // Render the help texture
        SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergrungRect);
        SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        _font_2.RenderText(_renderer, "MONSTER", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 260, 20);
        _font.RenderText(_renderer, "Monster is a classic arcade game developed by Namco and released ", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 80);
        _font.RenderText(_renderer, " in 1981. The game is developed by Muaaz Bdear and Nour Ahmad", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 120);
        _font.RenderText(_renderer, "and the goal is to destroy enemy spacecraft by shooting them", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 160);
        _font.RenderText(_renderer, "with your own spaceship.Press 'Q' to close the game,", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 200);
        _font.RenderText(_renderer, "'F' for full screen mode, 'W' to move up, 's' to move ", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 240);
        _font.RenderText(_renderer, "down, 'D' to move right, 'A' to move left, ", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 280);
        _font.RenderText(_renderer, "and 'SPACE' to shoot. 'P' to pause", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 320);
        _font.RenderText(_renderer, "and 'ESC' to return to the menu..", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 360);
        _font.RenderText(_renderer, "We hope you have fun playing the game!", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 400);
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

        if (mouseX >= _backButtonRect.x && mouseX <= _backButtonRect.x + _backButtonRect.w &&
            mouseY >= _backButtonRect.y && mouseY <= _backButtonRect.y + _backButtonRect.h && _backButtonRect.x > 370)
        {
            // Mouse is hovering over the sound button
            _backButtonRect.x -= 1;
        }
        else if (_backButtonRect.x < 400)
        {
            _backButtonRect.x += 1; // Move the button back to its original position
        }
    }
    #endregion
}



