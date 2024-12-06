using System;
using System.Threading.Tasks;
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
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(FirebaseCreate);
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

        private void FirebaseCreate(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                {
                    throw new Exception($"Could not resolve all dependencies: {task.Exception}");
                }
                FirebaseApp app = FirebaseApp.DefaultInstance;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}

