using System;
using System.Collections;
using _Project._Code.Core.Gameplay.PlayerControl.PlayerShooting;
using _Project._Code.Meta.DataConfig.Configs;
using _Project._Code.Meta.DataConfig.Configs.ClassConfigs;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Effects
{
    public class ShootingEffect : MonoBehaviour
    {
        [SerializeField] private Animation shootingAnimation;

        private const string ShootLaserEffectName = "ShootLaserEffect";
        private const string ShootBulletEffectName = "ShootBulletEffect";
        
        private IOnBulletInvoke _onBulletInvoke;
        private IOnLaserInvoke _onLaserInvoke;
        private LaserSettingsConfig _laserSettingsConfig;

        [Inject]
        public void Construct(IOnBulletInvoke onBulletInvoke, IOnLaserInvoke onLaserInvoke, ILaserSettingsConfig laserSettingsConfig)
        {
            _onBulletInvoke = onBulletInvoke;
            _onLaserInvoke = onLaserInvoke;
            _laserSettingsConfig = laserSettingsConfig.LaserSettingsConfig;
        }

        private void Start()
        {
            _onBulletInvoke.OnBulletInvoke += BulletShoot;
            _onLaserInvoke.OnLaserInvoke += LaserShoot;
        }

        private void OnDestroy()
        {
            _onBulletInvoke.OnBulletInvoke -= BulletShoot;
            _onLaserInvoke.OnLaserInvoke -= LaserShoot;
        }
        

        private void BulletShoot()
        {
            shootingAnimation.Play(ShootBulletEffectName);
        }
        
        private void LaserShoot()
        {
            shootingAnimation.Play(ShootLaserEffectName);
            
            StartCoroutine(StopLaser());
        }

        private IEnumerator StopLaser()
        {
            yield return new WaitForSeconds(_laserSettingsConfig.LaserActiveTime);
            
            shootingAnimation.Stop(ShootLaserEffectName);
            
            //при использовании Stop анимация останавливается на середение по этому пока сбрасываю её запуском ShootBulletEffect
            //потом уточню как сделать получше
            shootingAnimation.Play(ShootBulletEffectName);
        }
    }
}