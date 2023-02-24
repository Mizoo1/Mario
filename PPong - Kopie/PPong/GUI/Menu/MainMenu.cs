using Galaga.GUI;
using PPong.GUI;
using SDL2;

namespace PPong;

    public class MainMenu : IGameState
    {
    #region Variables
        private Font _font;
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the button textures and rectangles
        MenuOption background;
        MenuOption start;
        MenuOption setting;
        MenuOption help;
        int highScore;
        MenuOption exit;
        MenuOption galaga;
        private List<MenuOption> options;

    #endregion

    #region Constructor

    public MainMenu(IntPtr window, IntPtr renderer)
    {
        _window = window;
        _renderer = renderer;
        if (SDL_ttf.TTF_Init() != 0)
        {
            throw new Exception("Failed to initialize SDL_ttf: " + SDL_ttf.TTF_GetError());
        }
        _font = new Font("Assest\\Lato-Italic.ttf", 40);
        if (File.Exists("highscore.txt"))
        {
            String highscoreString = File.ReadAllText("highscore.txt");
            highScore = int.Parse(highscoreString);
        }
        options = new List<MenuOption>();
        SDL.SDL_SetRenderDrawColor(_renderer, 0, 100, 170, 255);
        create();
    }
    #endregion

    #region Methods
    public void create()
    {
        // Create all menu options
        background = new MenuOption("background", _renderer, _window);
        start = new MenuOption("start", _renderer, _window);
        help = new MenuOption("help", _renderer, _window);
        setting = new MenuOption("setting", _renderer, _window);
        galaga = new MenuOption("galaga", _renderer, _window);
        exit = new MenuOption("exit", _renderer, _window);

        // Add all menu options to the list
        options.Add(background);
        options.Add(galaga);
        options.Add(start);
        options.Add(setting);
        options.Add(help);
        options.Add(exit);

        // Call the createMenu method for each menu option
        foreach (MenuOption menuOption in options)
            menuOption.createMenu();
    }
    public void Update()
    {
        // Update the position of each menu option
        start.UpdateButtonPositions();
        help.UpdateButtonPositions();
        setting.UpdateButtonPositions();
        exit.UpdateButtonPositions();

    }

    public void Draw()
    {
        // Clear the renderer
        SDL.SDL_RenderClear(_renderer);
        // Render the button textures
        foreach (MenuOption menuOption in options)
            menuOption.Draw();
        _font.RenderText(_renderer, "Hightscore: " + highScore, new SDL.SDL_Color { r = 255, g = 255, b = 255 }, 100, 420);
    }


    public void HandleInput()
    {
        foreach (MenuOption menuOption in options)
            menuOption.HandleInput();
    }
    #endregion
}
