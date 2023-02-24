using PPong.GUI;
using SDL2;

namespace Galaga.GUI;

class SinglePlay : IGameState
    {

    private PPong.GUI.GameplayScreen Verwalten;
        // Constructor
    public SinglePlay(IntPtr window, IntPtr renderer)
    {
		Verwalten = new PPong.GUI.GameplayScreen( window, renderer);
        Update();
	}

    // Method to display the single play screen
    public void Update()
    {
		Verwalten.Setup();

	}

    public void Draw()
    {
           
    }

    // Method to handle player input
    public void HandleInput()
    {
           
    }
}