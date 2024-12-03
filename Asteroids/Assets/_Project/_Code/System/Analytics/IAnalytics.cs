using System.Collections.Generic;
using _Project._Code.Parameters;
using Zenject;

namespace _Project._Code.System.Analytics
{
    public interface IAnalytics : IInitializable
    {
        public void OnGameStart();
        public void OnGameEnd(GameOverParams gameOverParams);
        public void OnLaserUsed();
    }
}