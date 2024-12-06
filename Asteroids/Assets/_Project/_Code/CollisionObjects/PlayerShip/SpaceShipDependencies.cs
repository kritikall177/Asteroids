using _Project._Code.System.GameState;
using _Project._Code.UI;
using Zenject;

namespace _Project._Code.CollisionObjects.PlayerShip
{
    public class SpaceShipDependencies
    {
        private IGameStateActionsInvoker _gameStateActionsInvoker;
        private UIAdsChoose _adsUIChoose;
        
        [Inject]
        public SpaceShipDependencies(IGameStateActionsInvoker gameStateActionsInvoker, UIAdsChoose adsUIChoose)
        {
            _gameStateActionsInvoker = gameStateActionsInvoker;
            _adsUIChoose = adsUIChoose;
        }
        
        public void HandleDestroyed()
        {
            _gameStateActionsInvoker.PauseGame();
            _adsUIChoose.AdsInvokeUI();
        }
    }
}