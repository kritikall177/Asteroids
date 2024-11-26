using System;
using _Project._Code.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project._Code.System
{
    public class UIStartGame : MonoBehaviour
    {
        [SerializeField] private Button _resetButton;
        [SerializeField] private TMP_Text _finalScore;
        
        private SignalBus _signalBus;
        private IScore _score;
        
        [Inject]
        public void Construct(SignalBus signalBus, IScore score)
        {
            _signalBus = signalBus;
            _score = score;
        }

        private void Start()
        {
            _signalBus.Subscribe<GameOverSignal>(ShowGameStartUI);
            _signalBus.Subscribe<GameStartSignal>(HideGameStartUI);
            _resetButton.onClick.AddListener(RestartGame);
            
            _finalScore.gameObject.SetActive(false);
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
            _signalBus.Fire<GameStartSignal>();
        }
    }
}