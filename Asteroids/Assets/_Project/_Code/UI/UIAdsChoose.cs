using _Project._Code.System.Ads;
using _Project._Code.System.GameState;
using _Project._Code.System.Score;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace _Project._Code.UI
{
    public class UIAdsChoose : MonoBehaviour
    {
        [SerializeField] private Button _showAdsAndContinueButton;
        [SerializeField] private Button _showAdsAndLoseButton;
        
        private IAdsInvoker _adsInvoker;
        private IGameStateActionsSubscriber _gameStateActions;
        
        private bool _isFirstDead = true;
        
        [Inject]
        public void Construct(IAdsInvoker adsInvoker, IGameStateActionsSubscriber gameStateActions)
        {
            _adsInvoker = adsInvoker;
            _gameStateActions = gameStateActions;
        }

        private void Awake()
        {
            _isFirstDead = true;
            HideAdsChoose();
            _showAdsAndContinueButton.onClick.AddListener(ShowAdsAndContinue);
            _showAdsAndLoseButton.onClick.AddListener(ShowAdsAndLose);
            
            _gameStateActions.OnGameStart += OnGameStart;
        }

        private void OnDestroy()
        {
            _gameStateActions.OnGameStart -= OnGameStart;
        }

        public void AdsInvokeUI()
        {
            if (_isFirstDead)
            {
                _isFirstDead = false;
                ShowAdsChoose();
            }
            else
            {
                ShowAdsAndLose();
            }
        }

        private void OnGameStart()
        {
            _isFirstDead = true;
        }

        private void ShowAdsAndLose()
        {
            _adsInvoker.ShowBannerAds();
            HideAdsChoose();
        }

        private void ShowAdsAndContinue()
        {
            _adsInvoker.ShowRewardedAds();
            HideAdsChoose();
        }

        private void ShowAdsChoose()
        {
            _showAdsAndContinueButton.gameObject.SetActive(true);
            _showAdsAndLoseButton.gameObject.SetActive(true);
        }

        private void HideAdsChoose()
        {
            _showAdsAndContinueButton.gameObject.SetActive(false);
            _showAdsAndLoseButton.gameObject.SetActive(false);
        }
    }
}