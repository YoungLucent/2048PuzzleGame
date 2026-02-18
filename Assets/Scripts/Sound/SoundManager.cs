using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _mergeSounds;
    [SerializeField] private AudioClip _winSound;     
    [SerializeField] private AudioClip _loseSound;     

    [Header("Settings")]
    [SerializeField] private float _pitchRandomness = 0.1f; 

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameEvents.OnCubeMerged += PlayMergeSound;
        GameEvents.OnGameWon += PlayWinSound;
        GameEvents.OnGameWon += PlayLoseSound;
    }

    private void OnDisable()
    {
        GameEvents.OnCubeMerged -= PlayMergeSound;
        GameEvents.OnGameWon -= PlayWinSound;
        GameEvents.OnGameWon -= PlayLoseSound;
    }

    private void PlayMergeSound(int cubeValue)
    {
        if (_mergeSounds.Length == 0) return;

        AudioClip clip = _mergeSounds[Random.Range(0, _mergeSounds.Length)];

        _audioSource.pitch = 1f + Random.Range(-_pitchRandomness, _pitchRandomness);

        _audioSource.PlayOneShot(clip);
    }

    private void PlayWinSound()
    {
        if (_winSound != null)
        {
            _audioSource.pitch = 1f;
            _audioSource.PlayOneShot(_winSound);
        }
    }

    private void PlayLoseSound()
    {
        if (_winSound != null)
        {
            _audioSource.pitch = 1f;
            _audioSource.PlayOneShot(_loseSound);
        }
    }
}