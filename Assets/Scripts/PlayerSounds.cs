using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Player))]
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip _jumpAudio;
        [SerializeField] private AudioClip _hitAudio;
        [SerializeField] private AudioClip _dieAudio;
        [SerializeField] private AudioClip _pointAudio;

        private AudioSource _audioSource;
        private Player _player;

        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
            _player = GetComponent<Player>();
            _player.OnPipeTrigger += PlayAudioPointReached;
            _player.OnCollisionEnter += PlayAudioHit;
            _player.OnDeath += PlayAudioDeath;
        }

        private void OnDisable()
        {
            _player.OnPipeTrigger -= PlayAudioPointReached;
            _player.OnCollisionEnter -= PlayAudioHit;
            _player.OnDeath -= PlayAudioDeath;
        }

        private void PlayAudioDeath()
        {
            _audioSource.PlayOneShot(_dieAudio);
        }

        private void PlayAudioHit()
        {
            _audioSource.PlayOneShot(_hitAudio);
        }

        private void PlayAudioPointReached()
        {
            _audioSource.PlayOneShot(_pointAudio);
        }

        public void JumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _audioSource.PlayOneShot(_jumpAudio);
            }
        }
    }
}
