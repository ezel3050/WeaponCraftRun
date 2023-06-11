using System.Collections.Generic;
using Statics;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip gateBulletHit;
        [SerializeField] private AudioClip magazineBulletHit;
        [SerializeField] private AudioClip enteringGate;
        [SerializeField] private AudioClip collideWithMagazine;
        [SerializeField] private AudioClip gunUpgrade;
        [SerializeField] private AudioClip clockBulletHit;
        [SerializeField] private AudioClip upgradeButton;
        [SerializeField] private List<AudioClip> woodBulletHits;
        [SerializeField] private AudioClip brickBulletHit;
        [SerializeField] private AudioClip brickGateBroke;
        [SerializeField] private List<AudioClip> chainBulletHits;
        [SerializeField] private AudioClip chainGateBroke;
        [SerializeField] private AudioClip endGame;
        [SerializeField] private AudioSource source;

        private AudioClip _target;
        private bool _isMute;

        public static SoundManager Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public void BulletHitToGate()
        {
            Stop();
            _target = gateBulletHit;
            Play();
        }

        public void BulletHitToMagazine()
        {
            Stop();
            _target = magazineBulletHit;
            Play();
        }
        
        public void BulletHitToClock()
        {
            Stop();
            _target = clockBulletHit;
            Play();
        }

        public void BulletHitToWoodGate()
        {
            Stop();
            _target = woodBulletHits.PickRandom();
            Play();
        }

        public void BulletHitToBrickGate()
        {
            Stop();
            _target = brickBulletHit;
            Play();
        }

        public void BulletHitToChainGate()
        {
            Stop();
            _target = chainBulletHits.PickRandom();
            Play();
        }

        public void CollideWithGate()
        {
            Stop();
            _target = enteringGate;
            Play();
        }

        public void CollideWithMagazine()
        {
            Stop();
            _target = collideWithMagazine;
            Play();
        }

        public void UpgradeGun()
        {
            Stop();
            _target = gunUpgrade;
            Play();
        }

        public void UpgradeButton()
        {
            Stop();
            _target = upgradeButton;
            Play();
        }

        public void BrickGateBroke()
        {
            Stop();
            _target = brickGateBroke;
            Play();
        }

        public void ChainGateBroke()
        {
            Stop();
            _target = chainGateBroke;
            Play();
        }

        public void EndGame()
        {
            Stop();
            _target = endGame;
            Play();
        }
        
        private void Play()
        {
            if (_isMute) return;
            source.clip = _target;
            source.Play();
        }

        private void Stop()
        {
            source.Stop();
        }

        private void MuteHandling(bool isMute)
        {
            _isMute = isMute;
            if (_isMute)
                Stop();
        }
    }
}