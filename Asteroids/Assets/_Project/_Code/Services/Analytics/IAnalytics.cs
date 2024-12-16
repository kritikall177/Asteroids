using _Project._Code.Parameters;

namespace _Project._Code.Services.Analytics
{
    public interface IAnalytics
    {
        public void OnGameStart();
        public void OnGameEnd(GameOverParams gameOverParams);
        public void OnLaserUsed();
    }
}