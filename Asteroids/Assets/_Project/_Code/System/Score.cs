using _Project._Code.Signals;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace _Project._Code.System
{
    public class Score : IInitializable, IScore
    {
        private SignalBus _signalBus;
        
        private int _score;

        [Inject]
        public Score(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<AddScoreSignal>(AddScore);
        }

        private void AddScore(AddScoreSignal signal)
        {
            if (signal.Score > 0)
            {
                _score += signal.Score;
                _signalBus.Fire<UpdateScoreUI>();
            }
        }


        public int GetScore()
        {
            return _score;
        }
    }
}