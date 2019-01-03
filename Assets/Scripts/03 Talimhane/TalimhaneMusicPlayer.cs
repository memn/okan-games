using System;
using UnityEngine;

public class TalimhaneMusicPlayer : MonoBehaviour
{
    // Sound effects
    public AudioClip StringPull;
    public AudioClip StringRelease;
    public AudioClip ArrowSwoosh;
    public AudioClip ArrowImpact;
    public AudioClip ArrowNock;

    private AudioSource _audioSource;

    public enum AudioClips
    {
        ArrowNock,
        StringPull,
        StringRelease,
        ArrowSwoosh,
        ArrowImpact
    }


    // Use this for initialization
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClips audioClip)
    {
        switch (audioClip)
        {
            case AudioClips.ArrowNock:
                _audioSource.PlayOneShot(ArrowNock);
                break;
            case AudioClips.StringPull:
                _audioSource.PlayOneShot(StringPull);
                break;
            case AudioClips.StringRelease:
                _audioSource.PlayOneShot(StringRelease);
                break;
            case AudioClips.ArrowSwoosh:
                _audioSource.PlayOneShot(ArrowSwoosh);
                break;
            case AudioClips.ArrowImpact:
                _audioSource.PlayOneShot(ArrowImpact);
                break;
            default:
                throw new ArgumentOutOfRangeException("audioClip", audioClip, null);
        }
    }
}