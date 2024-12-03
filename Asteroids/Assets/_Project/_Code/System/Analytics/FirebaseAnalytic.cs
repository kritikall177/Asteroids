using _Project._Code.Parameters;
using UnityEngine;
using Firebase;
using Firebase.Analytics;

namespace _Project._Code.System.Analytics
{
    public class FirebaseAnalytic : IAnalytics
    {
        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                FirebaseApp app =  FirebaseApp.DefaultInstance;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            });
        }

        public void OnGameStart()
        {
            FirebaseAnalytics.LogEvent("game_start");
        }

        public void OnGameEnd(GameOverParams gameOverParams)
        {
            FirebaseAnalytics.LogEvent("game_end",
                new Parameter("total_shots", gameOverParams.totalShots),
                new Parameter("laser_uses", gameOverParams.laserUses),
                new Parameter("destroyed_asteroids", gameOverParams.destroyedAsteroids),
                new Parameter("destroyed_ufos", gameOverParams.destroyedAsteroids));
        }

        public void OnLaserUsed()
        {
            FirebaseAnalytics.LogEvent("laser_used");
        }
    }
}

