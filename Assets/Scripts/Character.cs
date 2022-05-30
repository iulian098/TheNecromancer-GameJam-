using System;
using UnityEngine;
using UnityEngine.Audio;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] protected int characterID;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] AudioClip[] footstepsClips;
    [SerializeField] AudioMixerGroup mixerGroup;

    protected float health;
    protected bool isDead;
    protected AudioSource footstepsSFX;

    public Action OnHealthChanged;
    public Action OnDie;
    public virtual float Health { get => health; 
        set {
            health = value;
            if (health < maxHealth)
                OnHealthChanged?.Invoke();
            if (health <= 0) {
                Die();
                OnDie?.Invoke();
            }
        }
    }
    public float MaxHealth => maxHealth;
    public Animator Anim => anim;
    public bool IsDead => isDead;

    public virtual void Start() {
        health = maxHealth;
        if (footstepsClips.Length == 0)
            footstepsClips = GameManager.Instance.GameData.FootstepClips;

        footstepsSFX = AddAudioSource(_maxDist: 15);
    }

    public AudioSource AddAudioSource(float _volume = 1, float _pitch = 1, float _minDist = 2, float _maxDist = 20, float _spatial = 1) {
        AudioSource audioS = gameObject.AddComponent<AudioSource>();
        audioS.outputAudioMixerGroup = mixerGroup;
        audioS.minDistance = _minDist;
        audioS.maxDistance = _maxDist;
        audioS.volume = _volume;
        audioS.pitch = _pitch;

        audioS.rolloffMode = AudioRolloffMode.Linear;
        audioS.spatialBlend = _spatial;

        return audioS;

    }

    protected void AudioVolumeAndPitch(AudioSource _audioSource, float _minPitch = 0.85f, float _maxPitch = 1.15f, float _minVolume = 0.75f, float _maxVolume = 1f) {
        _audioSource.pitch = UnityEngine.Random.Range(_minPitch, _maxPitch);
        _audioSource.volume = UnityEngine.Random.Range(_minVolume, _maxVolume);
    }

    public virtual void PlayFootstep() {
        if (!footstepsSFX) return;

        AudioVolumeAndPitch(footstepsSFX, 0.85f, 1.15f, 0.5f, 0.75f);
        footstepsSFX.PlayOneShot(footstepsClips[UnityEngine.Random.Range(0, footstepsClips.Length)]);
    }

    public virtual void ReceiveDamage(float damage) {
        Health -= damage;
    }
    public virtual void Die() {
        throw new System.NotImplementedException();
    }
}
