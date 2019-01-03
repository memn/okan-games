using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FPArcher : MonoBehaviour
{
    [SerializeField] private AudioClip _breathSound; // the sound played when character leaves the ground.

    private bool _hold;

    private AudioSource _audioSource;
    private ElvenBow _elvenBow;
    private FirstPersonController _controller;
    private bool _gameStarted;

    // Use this for initialization
    private void Start()
    {
        _controller = GetComponent<FirstPersonController>();
        _audioSource = GetComponent<AudioSource>();
        _elvenBow = FindObjectOfType<ElvenBow>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (!_controller.isActiveAndEnabled)
        {
            return;
        }

        if (!_gameStarted)
        {
            _gameStarted = true; 
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!_hold)
            {
                _elvenBow.Set();
                _hold = true;
                _audioSource.loop = true;
                PlayHoldSound();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_hold)
            {
                _elvenBow.Release();
                _audioSource.loop = false;
                _audioSource.Stop();
                _hold = false;
            }
        }
    }

    private void PlayHoldSound()
    {
        _audioSource.clip = _breathSound;
        _audioSource.Play();
    }
}