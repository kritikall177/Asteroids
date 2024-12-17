using System;
using System.Collections;
using _Project._Code.Core.Collision.CollisionObjects.PlayerShip;
using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Meta;
using _Project._Code.Meta.DataConfig.Configs;
using _Project._Code.Meta.DataConfig.Configs.ClassConfigs;
using _Project._Code.Meta.InputSystem;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gameplay.PlayerControl.PlayerShooting
{
    public class LaserShooting : IInitializable, IDisposable, ILaserChargeChange, IOnLaserInvoke
    {
        public event Action<int> OnLaserChargeChanged;
        public event Action OnLaserInvoke;

        private GameObject _laserGameObject;
        private AsyncProcessor _asyncProcessor;
        private IInputSystem _inputSystem;
        private IGameStateActionsSubscriber _gameStateActions;
        private LaserSettingsConfig _laserSettingsConfig;

        private int _laserCharge;
        
        public LaserShooting(AsyncProcessor asyncProcessor, SpaceShip spaceShip, IInputSystem inputSystem,
            IGameStateActionsSubscriber gameStateActions, ILaserSettingsConfig laserSettingsConfig)
        {
            _inputSystem = inputSystem;
            _asyncProcessor = asyncProcessor;
            _laserGameObject = spaceShip.LaserGameObject;
            _gameStateActions = gameStateActions;
            _laserSettingsConfig = laserSettingsConfig.LaserSettingsConfig;
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
            _laserCharge = _laserSettingsConfig.MaxLaserCharge;
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
            OnLaserInvoke?.Invoke();

            yield return new WaitForSeconds(_laserSettingsConfig.LaserActiveTime);

            _laserGameObject.SetActive(false);
        }

        private IEnumerator RestoreLaser()
        {
            yield return new WaitForSeconds(_laserSettingsConfig.LaserRestoreTime);

            if (_laserCharge < _laserSettingsConfig.MaxLaserCharge)
            {
                _laserCharge += 1;
                OnLaserChargeChanged?.Invoke(_laserCharge);
            }
        }
    }
}