namespace _Project._Code.System.GameState
{
    public interface IGameStateActionsInvoker
    {
        public void GameOver();
        public void StartGame();
        public void PauseGame();
        public void ResumeGame();
    }
}