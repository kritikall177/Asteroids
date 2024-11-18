using System;
using Code.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code
{
    //
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stats;
        [SerializeField] private Button _resetButton;
        [SerializeField] private TMP_Text _finalScore;

        private SignalBus _signalBus;
        
        private int _score = 0;
        private Vector2 _position = Vector2.zero;
        private int _rotation = 0;
        private int _laserCount; //в ресетUI достать количество лазеров с класса

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<GameStartSignal>(ResetUI);
            _signalBus.Subscribe<GameOverSignal>(ShowGameOverUI);
            _signalBus.Subscribe<UpdateTransformSignal>(UpdatePlayerPosition);
            _signalBus.Subscribe<AddScoreSignal>(AddScore);
            _signalBus.Subscribe<UpdateLaserCountSignal>(UpdateLaserCount);
            
            _resetButton.onClick.AddListener(RestartGame);
        }

        private void UpdateLaserCount(UpdateLaserCountSignal laserCount)
        {
            _laserCount = laserCount.LaserCount;
            UpdateText();
        }

        private void AddScore(AddScoreSignal addScoreSignal)
        { 
            _score += addScoreSignal.Score;
            UpdateText();
        }

        private void UpdatePlayerPosition(UpdateTransformSignal transformSignal)
        {
            _position = transformSignal.Position;
            _rotation = (int)transformSignal.Rotation;
            UpdateText();
        }

        private void RestartGame()
        {
            _signalBus.Fire<GameStartSignal>();
        }

        private void ResetUI()
        {
            _score = 0;
            _position = Vector2.zero;
            _rotation = 0;
            
            _resetButton.gameObject.SetActive(false);
            _finalScore.gameObject.SetActive(false);
            UpdateText();
        }

        private void ShowGameOverUI()
        {
            _resetButton.gameObject.SetActive(true);
            _finalScore.gameObject.SetActive(true);
            _finalScore.SetText($"Score:\n{_score}");
        }

        private void UpdateText()
        {
            string str = $"Счёт:\n{_score}\nКординаты:\nx:{_position.x}\ty:{_position.y}\nУгол поворота:\n{_rotation} градусов\nзарядов лазера:{_laserCount}";
            _stats.SetText(str);
        }
    }
}