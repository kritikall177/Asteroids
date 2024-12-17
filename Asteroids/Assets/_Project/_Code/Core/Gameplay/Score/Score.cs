using System;
using System.Collections;
using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Core.Gameplay.Score.ScoreStorage;
using _Project._Code.Meta;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gameplay.Score
{
    public class Score : IScore, IInitializable, IDisposable
    {
        public event Action OnScoreChanged;

        private int _scoreBonusForTime = 1;
        private int _scoreBonusInterval = 3;

        private IGameStateActionsSubscriber _gameStateActions;
        private IScoreStorage _scoreStorage;
        private AsyncProcessor _asyncProcessor;

        private int _score;
        
        public Score(IGameStateActionsSubscriber gameStateActions, IScoreStorage scoreStorage,
            AsyncProcessor asyncProcessor)
        {
            _gameStateActions = gameStateActions;
            _scoreStorage = scoreStorage;
            _asyncProcessor = asyncProcessor;
        }

        public void Initialize()
        {
            _gameStateActions.OnGameStart += OnGameStart;
            _gameStateActions.OnGameOver += OnGameEnd;
        }

        public void Dispose()
        {
            _gameStateActions.OnGameStart -= OnGameStart;
            _gameStateActions.OnGameOver += OnGameEnd;
        }

        public void AddScore(int score)
        {
            if (score > 0)
            {
                _score += score;
                OnScoreChanged?.Invoke();
            }
        }

        public int GetScore()
        {
            return _score;
        }

        private void OnGameEnd()
        {
            _scoreStorage.TryAddInBestScore(_score);
            _asyncProcessor.StopCoroutine(AddTimeScore(_scoreBonusForTime, _scoreBonusInterval));
        }

        private void OnGameStart()
        {
            _score = 0;
            _asyncProcessor.StartCoroutine(AddTimeScore(_scoreBonusForTime, _scoreBonusInterval));
        }

        private IEnumerator AddTimeScore(int scoreBonusForTime, int scoreBonusInterval)
        {
            while (true)
            {
                _score += scoreBonusForTime;
                OnScoreChanged?.Invoke();
                yield return new WaitForSeconds(scoreBonusInterval);
            }
        }
    }
}