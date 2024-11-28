using _Project._Code.System.GameState;
using Zenject;

namespace _Project._Code.CollisionObjects
{
    public class SpaceShipDependencies
    {
        private IGameStateActionsInvoker _gameStateActionsInvoker;
        
        [Inject]
        public SpaceShipDependencies(IGameStateActionsInvoker gameStateActionsInvoker)
        {
            _gameStateActionsInvoker = gameStateActionsInvoker;
        }
        
        public void HandleDestroyed()
        {
            _gameStateActionsInvoker.StopGame();
        }
    }
}