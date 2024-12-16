using _Project._Code.Core.Installers;
using _Project._Code.Gameplay.Score.ScoreStorage;
using _Project._Code.Services.Ads;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace _Project._Code.UI.MenuSceneUI
{
    public class UIStartMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private CodelessIAPButton _AdsButton;

        private ISceneLoad _sceneLoad;
        private IAdsToggle _adsToggle;
        private IScoreStorage _scoreStorage;

        [Inject]
        public void Construct(ISceneLoad sceneLoad, IAdsToggle adsToggle, IScoreStorage scoreStorage)
        {
            _sceneLoad = sceneLoad;
            _adsToggle = adsToggle;
            _scoreStorage = scoreStorage;
        }


        private void Start()
        {
            _playButton.onClick.AddListener(LoadGame);
            _quitButton.onClick.AddListener(QuitGame);
            _AdsButton.onPurchaseComplete.AddListener(DisableAds);
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

        private void DisableAds(Product arg0)
        {
            _adsToggle.DisableAds();
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