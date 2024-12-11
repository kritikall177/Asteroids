using _Project._Code._Installers;
using _Project._Code.System.Ads;
using _Project._Code.System.GameStorage;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace _Project._Code.UI.MenuSceneUI
{
    public class UIStartMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _disableAdsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private TMP_Text _bestScoreText;
        
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
            _disableAdsButton.onClick.AddListener(DisableAds);
            _quitButton.onClick.AddListener(QuitGame);
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
            //уточнить как реализовать покупку пока просто по нажатию реклама будет вылючаться
            _adsToggle.DisableAds();
        }

        private void QuitGame()
        {
            //заменить на нормальный выход перед билдом
            EditorApplication.isPlaying = false;
        }
    }
}