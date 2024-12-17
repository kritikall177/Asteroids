namespace _Project._Code.Core.Gameplay.GameState
{
    public interface IGameStateActionsInvoker
    {
        public void GameOver();
        public void StartGame();
        public void PauseGame();
        public void ResumeGame();
    }
}