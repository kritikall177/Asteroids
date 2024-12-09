using System;
using System.Threading.Tasks;
using _Project._Code._DataConfig;
using _Project._Code._Installers;
using _Project._Code.Parameters;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Zenject;

namespace _Project._Code.System.Analytics
{
    public class FirebaseAnalytic : IAnalytics, IInitializable, IFirebaseConfigUpdated
    {
        public event Action OnFirebaseConfigUpdated;
        
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
                
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                FetchDataAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void FetchDataAsync()
        {
            Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            if (!fetchTask.IsCompleted)
            {
                Debug.LogError($"Task {fetchTask.IsFaulted}: {fetchTask.Exception}");
            }
            
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"Failed to fetch data from Firebase: {info.LastFetchStatus}");
                return;
            }

            remoteConfig.ActivateAsync().ContinueWithOnMainThread(task =>
            {
                OnFirebaseConfigUpdated?.Invoke();
                Debug.Log($"Successfully fetched data from Firebase: {task.Result}");
            });
        }
    }

    public interface IFirebaseConfigUpdated
    {
        public event Action OnFirebaseConfigUpdated;
    }
}

