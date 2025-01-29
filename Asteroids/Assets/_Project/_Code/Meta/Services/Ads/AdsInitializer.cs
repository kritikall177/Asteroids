using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project._Code.Meta.Services.Ads
{
    public class AdsInitializer : IUnityAdsInitializationListener, IAds, IUnityAdsShowListener
    {
        public string AndroidBannerAdsId { get; private set; } = "Interstitial_Android";
        public string AndroidRewardedAdsId { get; private set; } = "Rewarded_Android";

        private string _androidGameId = "5742623";
        private bool _testMode = true;

        private IOnAdsCompleted _onAdsCompleted;

        private string _gameId;

        public AdsInitializer(IOnAdsCompleted onAdsCompleted)
        {
            _onAdsCompleted = onAdsCompleted;
        }

        public void Initialize()
        {
            InitializeAds();
        }

        private void InitializeAds()
        {
#if UNITY_ANDROID
            _gameId = _androidGameId;
            _testMode = false;
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
            Advertisement.Show(AndroidBannerAdsId, this);
        }

        public void ShowRewardedAds()
        {
            Advertisement.Show(AndroidRewardedAdsId, this);
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            _onAdsCompleted.AdComplete(placementId);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"Ad failed: {placementId}, Error: {error}, Message: {message}");

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