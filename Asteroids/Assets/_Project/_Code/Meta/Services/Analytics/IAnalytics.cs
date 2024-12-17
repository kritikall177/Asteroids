using _Project._Code.Meta.Parameters;

namespace _Project._Code.Meta.Services.Analytics
{
    public interface IAnalytics
    {
        public void OnGameStart();
        public void OnGameEnd(GameOverParams gameOverParams);
        public void OnLaserUsed();
    }
}