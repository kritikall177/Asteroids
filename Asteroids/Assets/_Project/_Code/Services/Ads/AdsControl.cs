using _Project._Code.Gameplay.GameState;
using Zenject;

namespace _Project._Code.Services.Ads
{
    public class AdsControl : IInitializable, IAdsShow, IOnAdsCompleted, IAdsToggle
    {
        private IAds _ads;
        private IGameStateActionsInvoker _gameStateActionsInvoker;

        private bool _isAdsEnabled = true;

        [Inject]
        public AdsControl(IGameStateActionsInvoker gameStateActionsInvoker)
        {
            _gameStateActionsInvoker = gameStateActionsInvoker;
            _ads = new AdsInitializer(this);
        }


        public void Initialize()
        {
            _ads.Initialize();
        }

        public void EnableAds()
        {
            _isAdsEnabled = true;
        }

        public void DisableAds()
        {
            _isAdsEnabled = false;
        }

        public void ShowDefaultAds()
        {
            if (_isAdsEnabled)
            {
                _ads.ShowBannerAds();
            }
            else
            {
                _gameStateActionsInvoker.ResumeGame();
                _gameStateActionsInvoker.GameOver();
            }
        }

        public void ShowRewardedAds()
        {
            if (_isAdsEnabled)
            {
                _ads.ShowRewardedAds();
            }
            else
            {
                _gameStateActionsInvoker.ResumeGame();
            }
        }

        public void AdComplete(string placementId)
        {
            if (placementId == _ads.AndroidRewardedAdsId)
            {
                _gameStateActionsInvoker.ResumeGame();
            }
            else
            {
                _gameStateActionsInvoker.ResumeGame();
                _gameStateActionsInvoker.GameOver();
            }
        }
    }
}