namespace Galaga.GUI;

interface IGameState
{
    #region Methods
    void Update();
    void Draw();
    void HandleInput();
    #endregion
}