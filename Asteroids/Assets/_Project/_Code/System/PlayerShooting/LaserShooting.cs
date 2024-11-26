using System;
using System.Collections;
using _Project._Code.CollisionObjects;
using _Project._Code.MemoryPools;
using _Project._Code.System.GameState;
using _Project._Code.System.InputSystem;
using UnityEngine;
using Zenject;

namespace _Project._Code.System.PlayerShooting
{
    public class LaserShooting : IInitializable, IDisposable, ILaserChargeChange
    {
        public event Action<int> OnLaserChargeChanged;
        
        private GameObject _laserGameObject;
        private AsyncProcessor _asyncProcessor;
        private IInputSystem _inputSystem;
        private IGameStateActionsSubscriber _gameStateActions;
        
        private int _maxLaserCharge = 2;
        private int _laserCharge = 2;
        private float _laserActiveTime = 0.5f;
        private int _laserRestoreTime = 20;

        [Inject]
        public LaserShooting(AsyncProcessor asyncProcessor, SpaceShip spaceShip, IInputSystem inputSystem, 
            IGameStateActionsSubscriber gameStateActions)
        {
            _inputSystem = inputSystem;
            _asyncProcessor = asyncProcessor;
            _laserGameObject = spaceShip.LaserGameObject;
            _gameStateActions = gameStateActions;
        }

        public void Initialize()
        {
            _inputSystem.OnHeavyAttackEvent += LaserAttack;
            _gameStateActions.OnGameOver += ResetCharges;
            
            ResetCharges();
        }

        public void Dispose()
        {
            _inputSystem.OnHeavyAttackEvent -= LaserAttack;
            _gameStateActions.OnGameOver -= ResetCharges;
        }
        
        private void ResetCharges()
        {
            _asyncProcessor.StopAllCoroutines();
            _laserGameObject.SetActive(false);
            _laserCharge = _maxLaserCharge;
            OnLaserChargeChanged?.Invoke(_laserCharge);
        }

        private void LaserAttack(bool isAttack)
        {
            if (isAttack && _laserCharge > 0 && !_laserGameObject.activeSelf)
            {
                _asyncProcessor.StartCoroutine(ActivateLaser());
                _asyncProcessor.StartCoroutine(RestoreLaser());
            }
        }

        private IEnumerator ActivateLaser()
        {
            _laserGameObject.SetActive(true);
            _laserCharge -= 1;
            OnLaserChargeChanged?.Invoke(_laserCharge);

            yield return new WaitForSeconds(_laserActiveTime);

            _laserGameObject.SetActive(false);
        }

        private IEnumerator RestoreLaser()
        {
            yield return new WaitForSeconds(_laserRestoreTime);

            if (_laserCharge < _maxLaserCharge)
            {
                _laserCharge += 1;
                OnLaserChargeChanged?.Invoke(_laserCharge);
            }
        }
    }
}