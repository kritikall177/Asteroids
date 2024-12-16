using _Project._Code.Gameplay.GameState;
using _Project._Code.Gameplay.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project._Code.UI.GameSceneUI
{
    public class UIStartGame : MonoBehaviour
    {
        [SerializeField] private Button _resetButton;
        [SerializeField] private TMP_Text _finalScore;

        private IGameStateActions _gameStateActions;
        private IGetScore _score;

        [Inject]
        public void Construct(IGameStateActions gameStateActions, IGetScore score)
        {
            _gameStateActions = gameStateActions;
            _score = score;
        }

        private void Start()
        {
            _gameStateActions.OnGameStart += HideGameStartUI;
            _gameStateActions.OnGameOver += ShowGameStartUI;

            _resetButton.onClick.AddListener(RestartGame);

            _finalScore.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _gameStateActions.OnGameStart -= HideGameStartUI;
            _gameStateActions.OnGameOver -= ShowGameStartUI;
        }

        private void ShowGameStartUI()
        {
            _resetButton.gameObject.SetActive(true);
            _finalScore.gameObject.SetActive(true);
            _finalScore.SetText($"Score:\n{_score.GetScore()}");
        }

        private void HideGameStartUI()
        {
            _resetButton.gameObject.SetActive(false);
            _finalScore.gameObject.SetActive(false);
        }

        private void RestartGame()
        {
            _gameStateActions.StartGame();
        }
    }
}