using _Project._Code.Signals;
using _Project._Code.System;
using TMPro;
using UnityEngine;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace _Project._Code.UI
{
    public class UIGameHud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stats;
        
        private SignalBus _signalBus;
        private IScoreSystem _scoreSystem;
        
        private int _score = 0;
        private Vector2 _position = Vector2.zero;
        private float _rotation = 0;
        private int _laserCount;
        
        [Inject]
        public void Construct(SignalBus signalBus, IScoreSystem scoreSystem)
        {
            _signalBus = signalBus;
            _scoreSystem = scoreSystem;
        }
        
        private void Start()
        {
            _stats.gameObject.SetActive(false);
            _signalBus.Subscribe<GameOverSignal>(HideHud);
            _signalBus.Subscribe<GameStartSignal>(ShowHud);
            _signalBus.Subscribe<UpdateTransformSignal>(UpdatePlayerPosition);
            _signalBus.Subscribe<UpdateLaserCountSignal>(UpdateLaserCount);
            _signalBus.Subscribe<UpdateScoreUI>(UpdateScore);
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

        private void UpdatePlayerPosition(UpdateTransformSignal transformSignal)
        {
            _position = transformSignal.Position;
            _rotation = transformSignal.Rotation;
            UpdateText();
        }
        
        private void UpdateLaserCount(UpdateLaserCountSignal laserCount)
        {
            _laserCount = laserCount.LaserCount;
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
            string str = $"Счёт:\n{_score}\nКординаты:\nx:{_position.x}\ty:{_position.y}\nУгол поворота:\n{(int)_rotation} градусов\nзарядов лазера:{_laserCount}";
            _stats.SetText(str);
        }
    }
}