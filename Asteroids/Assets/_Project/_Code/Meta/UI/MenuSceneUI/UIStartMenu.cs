using _Project._Code.Core.Gameplay.Score.ScoreStorage;
using _Project._Code.Meta.Installers;
using _Project._Code.Meta.Services.Ads;
using _Project._Code.Meta.Services.Ads.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Zenject;

namespace _Project._Code.Meta.UI.MenuSceneUI
{
    public class UIStartMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private Button _AdsButton;

        private ISceneLoad _sceneLoad;
        private IScoreStorage _scoreStorage;
        private IIAPService _iapService;

        [Inject]
        public void Construct(ISceneLoad sceneLoad, IScoreStorage scoreStorage, IIAPService iapService)
        {
            _sceneLoad = sceneLoad;
            _scoreStorage = scoreStorage;
            _iapService = iapService;
        }


        private void Start()
        {
            _playButton.onClick.AddListener(LoadGame);
            _quitButton.onClick.AddListener(QuitGame);
            _AdsButton.onClick.AddListener(DisableAds);
            UpdateScore();
        }

        private void UpdateScore()
        {
            var list = _scoreStorage.HighScores;
            var str = "Лучший результат:";
            foreach (var score in list)
            {
                str += "\n" + score;
            }

            _bestScoreText.SetText(str);
        }

        private void LoadGame()
        {
            _sceneLoad.LoadGameScene();
        }

        private void DisableAds()
        {
            _iapService.BuyNoAds();
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}