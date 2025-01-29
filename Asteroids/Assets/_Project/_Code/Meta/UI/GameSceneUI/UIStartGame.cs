using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Core.Gameplay.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project._Code.Meta.UI.GameSceneUI
{
    public class UIStartGame : MonoBehaviour
    {
        [SerializeField] private Button _resetButton;
        [SerializeField] private TMP_Text _finalScore;
        [SerializeField] private CanvasGroup _canvasGroup;

        private IGameStateActionsSubscriber _gameStateActionsSubscriber;
        private IGameStateActionsInvoker _gameStateActionsInvoker;
        private IGetScore _score;
        private IFadeEffect _fadeEffect;

        [Inject]
        public void Construct(IGameStateActionsSubscriber gameStateActionsSubscriber, IGetScore score, 
            IGameStateActionsInvoker gameStateActionsInvoker, IFadeEffect fadeEffect)
        {
            _score = score;
            _gameStateActionsSubscriber = gameStateActionsSubscriber;
            _gameStateActionsInvoker = gameStateActionsInvoker;
            _fadeEffect = fadeEffect;
        }

        private void Start()
        {
            _gameStateActionsSubscriber.OnGameStart += HideGameStartUI;
            _gameStateActionsSubscriber.OnGameOver += ShowGameStartUI;

            _resetButton.onClick.AddListener(RestartGame);

            _finalScore.gameObject.SetActive(false);
            _fadeEffect.FadeAnimation(_canvasGroup);
        }

        private void OnDestroy()
        {
            _gameStateActionsSubscriber.OnGameStart -= HideGameStartUI;
            _gameStateActionsSubscriber.OnGameOver -= ShowGameStartUI;
        }

        private void ShowGameStartUI()
        {
            _resetButton.gameObject.SetActive(true);
            _finalScore.gameObject.SetActive(true);
            _finalScore.SetText($"Score:\n{_score.GetScore()}");
            _fadeEffect.FadeAnimation(_canvasGroup);
        }

        private void HideGameStartUI()
        {
            _resetButton.gameObject.SetActive(false);
            _finalScore.gameObject.SetActive(false);
        }

        private void RestartGame()
        {
            _gameStateActionsInvoker.StartGame();
        }
    }
}