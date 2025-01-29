using _Project._Code.Core.Gameplay.PlayerControl.PlayerShooting;
using _Project._Code.Core.MemoryPools;
using UnityEngine;
using Zenject;

namespace _Project._Code.Meta.Sounds
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _effectsSource;
        
        [SerializeField] private AudioClip _bulletShootEffect;
        [SerializeField] private AudioClip _laserShootEffect;
        [SerializeField] private AudioClip _explosionEffect;
        
        private IOnLaserInvoke _onLaserInvoke;
        private IOnExplodeInvoke _onExplodeInvoke;
        private IOnBulletInvoke _onBulletInvoke;
        

        [Inject]
        public void Construct(IOnBulletInvoke onBulletInvoke, IOnExplodeInvoke onExplodeInvoke, IOnLaserInvoke onLaserInvoke)
        {
            _onLaserInvoke = onLaserInvoke;
            _onExplodeInvoke = onExplodeInvoke;
            _onBulletInvoke = onBulletInvoke;
        }

        private void Start()
        {
            _onLaserInvoke.OnLaserInvoke += LaserSound;
            _onExplodeInvoke.OnExplodeInvoke += ExplodeSound;
            _onBulletInvoke.OnBulletInvoke += ShootBulletSound;
        }

        private void OnDestroy()
        {
            _onLaserInvoke.OnLaserInvoke -= LaserSound;
            _onExplodeInvoke.OnExplodeInvoke -= ExplodeSound;
            _onBulletInvoke.OnBulletInvoke += ShootBulletSound;
        }

        private void LaserSound() => _effectsSource.PlayOneShot(_laserShootEffect);

        private void ExplodeSound() => _effectsSource.PlayOneShot(_explosionEffect);
        
        private void ShootBulletSound() => _effectsSource.PlayOneShot(_bulletShootEffect);
    }
}