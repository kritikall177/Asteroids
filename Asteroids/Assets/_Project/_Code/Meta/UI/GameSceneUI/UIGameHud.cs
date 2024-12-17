using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Core.Gameplay.PlayerControl.PlayerMovement;
using _Project._Code.Core.Gameplay.PlayerControl.PlayerShooting;
using _Project._Code.Core.Gameplay.Score;
using _Project._Code.Meta.Parameters;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project._Code.Meta.UI.GameSceneUI
{
    public class UIGameHud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stats;

        private IGameStateActionsSubscriber _gameStateActions;
        private IScore _scoreSystem;
        private ILaserChargeChange _laserCharge;
        private IPlayerPositionChange _playerPosition;

        private int _score = 0;
        private Vector2 _position = Vector2.zero;
        private float _rotation = 0;
        private int _laserCount;

        [Inject]
        public void Construct(IGameStateActionsSubscriber gameStateActions, IScore scoreSystem,
            ILaserChargeChange laserCharge, IPlayerPositionChange playerPosition)
        {
            _gameStateActions = gameStateActions;
            _scoreSystem = scoreSystem;
            _laserCharge = laserCharge;
            _playerPosition = playerPosition;
        }

        private void Start()
        {
            _gameStateActions.OnGameStart += ShowHud;
            _gameStateActions.OnGameOver += HideHud;

            _scoreSystem.OnScoreChanged += UpdateScore;

            _laserCharge.OnLaserChargeChanged += UpdateLaserCount;

            _playerPosition.OnPositionChange += UpdatePlayerPosition;

            _stats.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _gameStateActions.OnGameStart -= ShowHud;
            _gameStateActions.OnGameOver -= HideHud;

            _scoreSystem.OnScoreChanged -= UpdateScore;

            _laserCharge.OnLaserChargeChanged -= UpdateLaserCount;

            _playerPosition.OnPositionChange -= UpdatePlayerPosition;
        }

        private void ShowHud()
        {
            _stats.gameObject.SetActive(true);
            ResetUI();
        }

        private void HideHud()
        {
            _stats.gameObject.SetActive(false);
        }

        private void UpdateScore()
        {
            _score = _scoreSystem.GetScore();
            UpdateText();
        }

        private void UpdatePlayerPosition(PlayerPositionParams transformSignal)
        {
            _position = transformSignal.Position;
            _rotation = transformSignal.Rotation;
            UpdateText();
        }

        private void UpdateLaserCount(int laserCount)
        {
            _laserCount = laserCount;
            UpdateText();
        }

        private void ResetUI()
        {
            _score = 0;
            _position = Vector2.zero;
            _rotation = 0;
            UpdateText();
        }

        private void UpdateText()
        {
            string str =
                $"Счёт:\n{_score}\nКординаты:\nx:{_position.x}\ty:{_position.y}\nУгол поворота:\n{(int)_rotation} градусов\nзарядов лазера:{_laserCount}";
            _stats.SetText(str);
        }
    }
}