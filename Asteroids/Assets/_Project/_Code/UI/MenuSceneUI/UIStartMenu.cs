using _Project._Code._Installers;
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

        [Inject]
        public void Construct(ISceneLoad sceneLoad)
        {
            _sceneLoad = sceneLoad;
        }

        
        private void Start()
        {
            _playButton.onClick.AddListener(LoadGame);
            _disableAdsButton.onClick.AddListener(DisableAds);
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void LoadGame()
        {
            _sceneLoad.LoadGameScene();
        }

        private void DisableAds()
        {
            throw new NotImplementedException();
        }

        private void QuitGame()
        {
            //заменить на нормальный выход перед билдом
            EditorApplication.isPlaying = false;
        }
    }
}