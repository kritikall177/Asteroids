using System.Collections;
using _Project._Code.Core.MemoryPools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project._Code.Core.Effects
{
    public class ExplodeEffect : MonoBehaviour
    {
        [SerializeField] private Animation _explodeAnimation;
        
        private ExplodeEffectPool _explodeEffectPool;

        [Inject]
        public void Construct(ExplodeEffectPool explodeEffectPool)
        {
            _explodeEffectPool = explodeEffectPool;
        }

        
        private const string ExplodeEffectName = "ExplodeEffect";

        public void OnEnable()
        {
            _explodeAnimation.Play(ExplodeEffectName);
            StartCoroutine(WaitForExplosionEnd(_explodeAnimation.clip.length));
        }

        private IEnumerator WaitForExplosionEnd(float animationLength)
        {
            yield return new WaitForSeconds(animationLength);
            
            _explodeEffectPool.Despawn(this);
        }
    }
}