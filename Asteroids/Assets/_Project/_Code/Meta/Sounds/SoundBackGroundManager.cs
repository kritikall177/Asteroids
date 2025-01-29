using UnityEngine;

namespace _Project._Code.Meta.Sounds
{
    public class SoundBackGroundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        
        [SerializeField] private AudioClip _backgroundMusic;
        
        public void Awake()
        {
            PlayBackgroundMusic();
        }

        private void PlayBackgroundMusic()
        {
            _musicSource.clip = _backgroundMusic;
            _musicSource.Play();
        }
    }
}