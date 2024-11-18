using Zenject;

namespace Code
{
    public class GameManager
    {
        private SignalBus _signalBus;

        [Inject]
        public GameManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Fire<GameStartSignal>();
        }
    }
}