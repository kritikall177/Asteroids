using _Project._Code.Gameplay.GameState;
using _Project._Code.UI.GameSceneUI;
using Zenject;

namespace _Project._Code.Collision.CollisionObjects.PlayerShip
{
    public class SpaceShipDependencies
    {
        private IGameStateActionsInvoker _gameStateActionsInvoker;
        private UIRetryOrQuitPanel _adsUIChoose;
        
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