using _Project._Code.System.GameState;
using _Project._Code.UI;
using _Project._Code.UI.GameSceneUI;
using Zenject;

namespace _Project._Code.CollisionObjects.PlayerShip
{
    public class SpaceShipDependencies
    {
        private IGameStateActionsInvoker _gameStateActionsInvoker;
        private UIRetryOrQuitPanel _adsUIChoose;
        
        [Inject]
        public SpaceShipDependencies(IGameStateActionsInvoker gameStateActionsInvoker, UIRetryOrQuitPanel adsUIChoose)
        {
            _gameStateActionsInvoker = gameStateActionsInvoker;
            _adsUIChoose = adsUIChoose;
        }
        
        public void HandleDestroyed()
        {
            _gameStateActionsInvoker.PauseGame();
            _adsUIChoose.RetryOrQuitInvokeUI();
        }
    }
}