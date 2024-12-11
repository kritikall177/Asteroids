using _Project._Code.System.Ads;
using _Project._Code.System.GameState;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project._Code.UI.GameSceneUI
{
    public class UIRetryOrQuitPanel : MonoBehaviour
    {
        [SerializeField] private Button _ContinueButton;
        [SerializeField] private Button _LoseButton;
        
        private IGameStateActionsSubscriber _gameStateActions; 
        private IAdsShow _adsShow;
        
        private bool _isFirstDead = true;
        
        [Inject]
        public void Construct(IGameStateActionsSubscriber gameStateActions, IAdsShow adsShow)
        {
            _gameStateActions = gameStateActions;
            _adsShow = adsShow;
        }

        private void Awake()
        {
            _isFirstDead = true;
            HidePanel();
            _ContinueButton.onClick.AddListener(PressContinue);
            _LoseButton.onClick.AddListener(PressLose);
            
            _gameStateActions.OnGameStart += OnGameStart;
        }

        private void OnDestroy()
        {
            _gameStateActions.OnGameStart -= OnGameStart;
        }

        public void RetryOrQuitInvokeUI()
        {
            if (_isFirstDead)
            {
                _isFirstDead = false;
                ShowPanel();
            }
            else
            {
                PressLose();
            }
        }

        private void OnGameStart()
        {
            _isFirstDead = true;
        }

        private void PressLose()
        {
            _adsShow.ShowDefaultAds();
            HidePanel();
        }

        private void PressContinue()
        {
            _adsShow.ShowRewardedAds();
            HidePanel();
        }

        private void ShowPanel()
        {
            _ContinueButton.gameObject.SetActive(true);
            _LoseButton.gameObject.SetActive(true);
        }

        private void HidePanel()
        {
            _ContinueButton.gameObject.SetActive(false);
            _LoseButton.gameObject.SetActive(false);
        }
    }
}