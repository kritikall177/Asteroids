using System.Collections;
using _Project._Code.System.GameState;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace _Project._Code.System.Ads
{
    public class AdsInitializer : IUnityAdsInitializationListener, IInitializable, IAdsInvoker, IUnityAdsShowListener
    {
        private string _androidGameId = "5742623";
        private string _androidBannerAdsId = "Interstitial_Android";
        private string _androidRewardedAdsId = "Rewarded_Android";
        private bool _testMode = true;
        
        private IGameStateActionsInvoker _gameStateActionsInvoker;
        
        private string _gameId;

        [Inject]
        public AdsInitializer(IGameStateActionsInvoker gameStateActionsInvoker)
        {
            _gameStateActionsInvoker = gameStateActionsInvoker;
        }
        
        public void Initialize()
        {
            InitializeAds();
        }

        private void InitializeAds()
        {
#if UNITY_ANDROID
            _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }

        public void ShowBannerAds()
        {
            Advertisement.Show(_androidBannerAdsId, this);
        }
        
        public void ShowRewardedAds()
        {
            Advertisement.Show(_androidRewardedAdsId, this);
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            
            if (placementId == _androidRewardedAdsId)
            {
                _gameStateActionsInvoker.ResumeGame();
            }
            else
            {
                _gameStateActionsInvoker.ResumeGame();
                _gameStateActionsInvoker.GameOver();
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}