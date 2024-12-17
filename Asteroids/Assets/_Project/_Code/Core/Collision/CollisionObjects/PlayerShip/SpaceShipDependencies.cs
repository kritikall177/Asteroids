using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Meta.UI.GameSceneUI;

namespace _Project._Code.Core.Collision.CollisionObjects.PlayerShip
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